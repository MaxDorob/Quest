using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp7
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<int> startStatement = new List<int>();
            //string inputStatement = Console.ReadLine();
            //inputStatement = inputStatement.Replace(" ", "");
            //foreach (var item in inputStatement.Split(","))
            //{
            //    startStatement.Add(int.Parse(item));
            //}
            //Console.WriteLine("Количество перемещений: "+ReRoll(startStatement));
            ReRoll(new List<int> { 6, 14, 22, 12, 6, 25, 15, 14, 29, 21, 11, 14, 25, 13, 13 });
            Console.WriteLine("Количество перемещений: ");
            Console.ReadKey();
            //Console.WriteLine("Количество перемещений: "+ReRoll(new List<int> {18, 22, 30, 21, 2, 20, 22, 8, 30, 30, 7, 23, 1, 22, 8, 23, 7, 22, 25, 26, 17, 30, 27, 6, 25, 29, 20, 9, 3, 25, 16, 16, 30, 30, 8, 15, 27, 25, 6, 22, 16, 10, 24, 14, 26, 0, 13, 28, 11, 5
 //}));
            Console.ReadKey();
            //Console.WriteLine("Количество перемещений: " + ReRoll(new List<int> { 1, 5, 9, 10, 5 }));
        }
        static void  ReRoll(List<int> chipsOnSites)
        {

            int average = (int)chipsOnSites.Average(),
                n= chipsOnSites.Count(x => x > average),
                m= chipsOnSites.Count(x => x < average),
                i=0,
                j=0;
            int[] storage = new int[n];
            int[] store = new int[m];
            int[,] ways = new int[n, m];
            for (int z = 0; z < chipsOnSites.Count; z++)
            {
                if (chipsOnSites[z]>average)
                {
                    storage[i] = chipsOnSites[z] - average;
                    
                    for (int x = 0; x < chipsOnSites.Count; x++)
                    {
                        if (chipsOnSites[x]<average)
                        {
                            store[j] = average - chipsOnSites[x];
                        }
                    }
                    i++;
                }
            }

           // StateView(chipsOnSites);
          
        }
        int ShortestWayInCircle(int pos,int target,int size)
        {
            for (int wave = 0; wave <= size/2; wave++)
            {

            }
            return;
        }
        static void StateViewModified(List<int> statements,int pos)
        {
            for (int i = 0; i < statements.Count; i++)
            {
                if (i==pos)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(statements[i]);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(", ");
                }
                else Console.Write(statements[i]+", ");
            }
            Console.WriteLine();
        }
        static void StateView(List<int> statements)
        {
            foreach (var item in statements)
            {
                Console.Write($"{item}, ");
            }
            Console.WriteLine();
        }
        
    }
}
