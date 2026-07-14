using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TelerikAcademy12gEntities t = new TelerikAcademy12gEntities();


            //string name = Console.ReadLine();
            //var q = from x in t.Employees
            //        where x.Department.Name == name
            //        select x.FirstName + " " + x.LastName;
            //foreach (var x in q) 
            //{
            //    Console.WriteLine(x);
            //}

            // select Firstname from Employees
            //var p = t.Employees;
            //foreach (var e in p)
            //{
            //    Console.WriteLine(e.FirstName);
            //}
        }
    }
}
