using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int p = int.Parse(Console.ReadLine());
            int count = 0;
            for (int a = 1; a < p; a++) 
            {
                for (int b = a; b < p; b++)
                {
                        int c = p - a -b;
                        if (a+b+c==p && a+b>c && a+c>b && b+c>a)
                        {
                            //Console.WriteLine(a +" " + b+ " " + c + " ");
                            count++;
                        }

                }

            }  
            Console.WriteLine(count);
        }

    }

}