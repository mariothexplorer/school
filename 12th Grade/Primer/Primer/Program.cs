using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace Primer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Insert department");
            string d =Console.ReadLine();
            SqlConnection con = new SqlConnection(@"Server=.\\sqlexpress; DataBase=TelerikAcademy12g;
                                                     Integrated Security=true");
            con.Open();
            SqlCommand cmd = new SqlCommand(@"Select *
                                            from Employees e
                                            join Departments d
                                            on e.departmentid=d.departmentid
                                            where d.Name=@s", con);
            cmd.Parameters.Add(new SqlParameter("@s", d));

            /// най-лява клетка от първия ред, първата колона на резултата
            /// int cnt = (int)(cmd.ExecuteScalar()); 
            /// Console.WriteLine(cnt);
            
            /// резултатът е цялата таблица
            SqlDataReader r = cmd.ExecuteReader();
            /// чете ред по ред от резултата   
            while (r.Read())
            {
                string firstname = (String)r["firstname"];
                string lastName = (String)r["lastname"];
                decimal salary = (decimal)r["salary"];
               
                    if (lastName.Length < 8)
                    {
                        Console.WriteLine($"{firstname}\t{lastName}\t\t{salary} лв.");
                    }
                    else
                    {
                        Console.WriteLine($"{firstname}\t{lastName}\t{salary} лв.");
                    }
            }

            r.Close();
            con.Close();
        }
    }
}
