using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Spirki
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            var lines = File.ReadAllLines("spirki.txt");
            foreach (var line in lines)
            {
                string stop = line.Trim();
                if (string.IsNullOrEmpty(stop))
                    continue;
                stops.Add(stop);
                comboBox1.Items.Add(stop);
                comboBox2.Items.Add(stop);
                graph[stop] = new List<string>();
            }
            var lines_ = File.ReadAllLines("rebra.txt");
            foreach (var l in lines_)
            {
                string line = l.Trim();
                if (string.IsNullOrEmpty(line))
                    continue;
                var parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 2)
                    continue;
                int a = int.Parse(parts[0]) - 1;
                int b = int.Parse(parts[1]) - 1;
                string s1 = stops[a].Trim();
                string s2 = stops[b].Trim();
                graph[s1].Add(s2);
                graph[s2].Add(s1);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string start = comboBox1.Text.Trim();
            string end = comboBox2.Text.Trim();
            if (string.IsNullOrEmpty(start) && string.IsNullOrEmpty(end))
            {
                MessageBox.Show("Изберете начална и крайна спирка!");
                return;
            }
            if (string.IsNullOrEmpty(start))
            {
                MessageBox.Show("Изберете начална спирка!");
                return;
            }
            if (string.IsNullOrEmpty(end))
            {
                MessageBox.Show("Изберете крайна спирка!");
                return;
            }
            if (comboBox1.Text == comboBox2.Text)
            {
                MessageBox.Show("Началната и крайната спирка трябва да бъдат различни!");
                comboBox1.Text = "";
                comboBox2.Text = "";
                return;
            }
            var path = FindShortestPathBFS(start, end);
            if (path == null)
            {
                listBox1.Items.Add("Няма връзка между избраните спирки.");
            }
            else
            {
                foreach (var stop in path)
                listBox1.Items.Add(stop);
            }
        }
        Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>();
        List<string> stops = new List<string>();
        private List<string> FindShortestPathBFS(string start, string end)
        {
            Queue<string> queue = new Queue<string>();
            Dictionary<string, string> parent = new Dictionary<string, string>();
            HashSet<string> visited = new HashSet<string>();
            queue.Enqueue(start);
            visited.Add(start);
            parent[start] = null;
            while (queue.Count > 0)
            {
                string current = queue.Dequeue();
                if (current == end)
                return BuildPath(parent, end);
                foreach (var neighbor in graph[current])
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        parent[neighbor] = current;
                        queue.Enqueue(neighbor);
                    }
                }
            }
            return null;
        }

        private List<string> BuildPath(Dictionary<string, string> parent, string goal)
        {
            List<string> path = new List<string>();
            string current = goal;
            while (current != null)
            {
                path.Add(current);
                current = parent[current];
            }
            path.Reverse();
            return path;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;   
            comboBox2.SelectedIndex = -1;
            listBox1.Items.Clear();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
