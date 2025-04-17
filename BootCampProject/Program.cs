// See https://aka.ms/new-console-template for more information

// Using the classic Hello World example to demonstrate the use of C# in a console application.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootCampProject
{
    class Program
    {
        private static void Main(string[] args)
        {
            int[] primerArreglo = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            int[] array = new int[5];
            string[] weekDays = ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];
            Console.WriteLine(weekDays[0]);
            Console.WriteLine(weekDays[1]);
            Console.WriteLine(weekDays[2]);
            Console.WriteLine(weekDays[3]);
            Console.WriteLine(weekDays[4]);
            Console.WriteLine(weekDays[5]);
            Console.WriteLine(weekDays[6]);

            weekDays[0] = "Otro Valor";
            
            Console.WriteLine(weekDays[0]);
            
            for (int i = 0; i < weekDays.Length; i++)
            {
                Console.WriteLine(weekDays[i]);
            }

            // foreach (var numero in primerArreglo)
            // {
            //     Console.WriteLine(numero);
            // }
            
            // Two-dimensional array.
            int[,] array2DInitialization =  { { 1, 2 }, { 3, 4 }, { 5, 6 }, { 7, 8 } };
            
            // Accessing array elements.
            System.Console.WriteLine(array2DInitialization[0, 0]);
            System.Console.WriteLine(array2DInitialization[0, 1]);
            System.Console.WriteLine(array2DInitialization[1, 0]);
            System.Console.WriteLine(array2DInitialization[1, 1]);
            
            // Create a list of strings by using a
            // collection initializer.
            List<string> salmons = ["chinook", "coho", "pink", "sockeye"];
            
            // Iterate through the list.
            foreach (var salmon in salmons)
            {
                Console.WriteLine(salmon + " ");
            }
            
            // Remove an element from the list by specifying
            // the object.
            salmons.Remove("coho");
            salmons.RemoveAt(1);

            foreach (var salmon in salmons)
            {
                Console.WriteLine(salmon + " ");
            }
            
            salmons.Add("Nuevo Salmon");
            
            foreach (var salmon in salmons)
            {
                Console.WriteLine(salmon + " ");
            }
            
            // Create a dictionary and iterate through it.
            Dictionary<string, string> fish = new Dictionary<string, string>();
            fish.Add("chinook", "salmon");
            fish.Add("coho", "salmon");
            fish.Add("pink", "salmon");
            
            foreach (KeyValuePair<string, string> kvp in fish)
            {
                Console.WriteLine(kvp.Key + " " + kvp.Value);
            }

            Console.WriteLine("Hello, World!");
        }
    }
}