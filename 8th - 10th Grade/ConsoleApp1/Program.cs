using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            /* Zad 1

            string name = Console.ReadLine();
            int dni = int.Parse(Console.ReadLine());
            int countbileti = int.Parse(Console.ReadLine());
            double pricebilet = double.Parse(Console.ReadLine());   
            int procent = int.Parse(Console.ReadLine());

            double bileti_den = countbileti*pricebilet;
            double prihod = dni * bileti_den;
            double za_kino = prihod * procent / 100;
            double all = prihod - za_kino;

            Console.WriteLine("The profit from the movie "+name  + " is " +  $"{all:f2}" + " lv.");
            



            Zad 2
             
            double budget = double.Parse(Console.ReadLine());
            int nights = int.Parse(Console.ReadLine()); 
            double price_per_night = double.Parse(Console.ReadLine());  
            int razhodi_pro = int.Parse(Console.ReadLine());
            double price_nights=0;
            if (nights>=8)
            {
                price_per_night = price_per_night *0.95;

            }
            price_nights = price_per_night * nights;
            double raz = budget*razhodi_pro/100;
            double all = price_nights + raz;
            if (all<=budget)
            {
                Console.WriteLine("Ivanovi will be left with " + $"{(Math.Abs(budget-all)):f2}" + " leva after vacation.");
            }
            if (all>budget)
            {
                double a = Math.Abs(budget-all);
                Console.WriteLine($"{a:f2}" + " leva needed.");
            }
             


            zad 3 

            
            string country = Console.ReadLine();
            string ured = Console.ReadLine();
            double points_max = 20;
            double a = 0;
            double b = 0;

            if (country == "Russia")
            {
                if (ured == "ribbon")
                {
                    a = 9.1;
                    b = 9.4;
                }
                if (ured == "hoop")
                {
                    a = 9.3;
                    b = 9.8;
                }
                if (ured == "rope")
                {
                    a = 9.6;
                    b = 9;
                }
            }
            else  if (country == "Bulgaria")
            {
                if (ured == "ribbon")
                {
                    a = 9.6;
                    b = 9.4;
                }
                if (ured == "hoop")
                {
                    a = 9.55;
                    b = 9.75;
                }
                if (ured == "rope")
                {
                    a = 9.5;
                    b = 9.4;
                }
            }
            else if (country == "Italy")
            {
                if (ured == "ribbon")
                {
                    a = 9.2;
                    b = 9.5;
                }
                if (ured == "hoop")
                {
                    a = 9.45;
                    b = 9.35;
                }
                if (ured == "rope")
                {
                    a = 9.7;
                    b = 9.15;
                }
            }
            double point = a + b;
            Console.WriteLine($"The team of {country} get {point:f3} on {ured}.");
            point = ((points_max - point) / points_max)*100;
            Console.WriteLine($"{point:f2}%");


             *ZAD 4

             int broi_filmi = int.Parse(Console.ReadLine());
            string name;
            double rating = 0;
            double highest = 0.00;
            string high_name="";
            double lowest=10.00;
            string low_name="";
            double all = 0;
            for (int i = 0; i < broi_filmi; i++)
            {                
                name = Console.ReadLine();
                rating = double.Parse(Console.ReadLine());
                all = all + rating;
                if (highest < rating)
                {
                    highest = rating;
                    high_name = name;
                }
                if (lowest > rating)
                {
                    lowest = rating;
                    low_name = name;
                }
            }


            all = all / broi_filmi;
            Console.WriteLine(high_name+$" is with highest rating: {highest:f1}");
            Console.WriteLine(low_name+$" is with lowest rating: {lowest:f1}");
            Console.WriteLine($"Average rating: {all:f1}");

            */



            int days = int.Parse(Console.ReadLine());
            int hoursPerDay = int.Parse(Console.ReadLine());
            double totalSum = 0;

            for (int day = 1; day <= days; day++)
            {
                double dailySum = 0;
                for (int hour = 1; hour <= hoursPerDay; hour++)
                {
                    if (day % 2 == 0 && hour % 2 != 0)
                    {
                        dailySum += 2.50;
                    }
                    else if (day % 2 != 0 && hour % 2 == 0)
                    {
                        dailySum += 1.25;
                    }
                    else
                    {
                        dailySum += 1.00;
                    }
                }
                Console.WriteLine($"Day: {day} - {dailySum:f2} leva");
                totalSum += dailySum;
            }

            Console.WriteLine($"Total: {totalSum:f2} leva");
        
            

        }
    }
}

