using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Hotel
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int students = int.Parse(Console.ReadLine());
            int count = 0;
            for (int br2 = 0; br2 <= students/2; br2++)
            {
                for (int br3 = 0; br3 <=students/3; br3++)
                {
                    int x = students - br2 * 2 - br3 * 3;
                        if (x%4==0 && x>=0)
                        {
                            count++;
                           // Console.WriteLine(br2+ " " + br3 + " " +x);

                        }   
                }
            }

            Console.WriteLine(count);
        }
    }
}
