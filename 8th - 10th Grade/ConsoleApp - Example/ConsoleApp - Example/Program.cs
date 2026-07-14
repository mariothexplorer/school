using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp___Example
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int tires = int.Parse(Console.ReadLine());
            int count = 0;
            for (int cars = 0;cars <= tires/4; cars++)
            {
                for (int motor = 0; motor <= tires/2; motor++)
                {
                    
                        int x = tires - cars*4 - motor*2;
                        if (x % 10 == 0 && x >= 0)
                        {
                            count++;
                        }
                }
            }
           Console.WriteLine(count);
        }
    }
}
