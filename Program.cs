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
            Console.WriteLine("Количество перемещений: "+ReRoll(new List<int> { 6, 14, 22, 12, 6, 25, 15, 14, 29, 21, 11, 14, 25, 13, 13 }));
            Console.ReadKey();
            Console.WriteLine("Количество перемещений: "+ReRoll(new List<int> {18, 22, 30, 21, 2, 20, 22, 8, 30, 30, 7, 23, 1, 22, 8, 23, 7, 22, 25, 26, 17, 30, 27, 6, 25, 29, 20, 9, 3, 25, 16, 16, 30, 30, 8, 15, 27, 25, 6, 22, 16, 10, 24, 14, 26, 0, 13, 28, 11, 5
 }));
            Console.ReadKey();
            Console.WriteLine("Количество перемещений: " + ReRoll(new List<int> { 1, 5, 9, 10, 5 }));
        }
        static int ReRoll(List<int> chipsOnSites)
        {
            
           // StateView(chipsOnSites);
            //Console.Clear();
            int countOfChanges = 0;
            int average =(int)chipsOnSites.Average();
            while (!chipsOnSites.All(x=>x==average))
            {
                int indexOfTarget = chipsOnSites.IndexOf(chipsOnSites.Min());
            //    StateViewModified(chipsOnSites, indexOfTarget);
                int leftWeight=0, rightWeight=0;
                var halfOfCycle = chipsOnSites.Count / 2;
                for (int i = 1; i <=halfOfCycle; i++)
                {
                    int leftIndex=indexOfTarget-i<0?(chipsOnSites.Count-i+indexOfTarget):(indexOfTarget-i),
                        rightIndex=indexOfTarget+i>=chipsOnSites.Count?(i-(chipsOnSites.Count-indexOfTarget)):(indexOfTarget+i);
                    leftWeight += chipsOnSites[leftIndex];
                    rightWeight += chipsOnSites[rightIndex];
                }
                for (int wave = 1; wave <=halfOfCycle; wave++)
                {
                    int leftIndex = indexOfTarget - wave < 0 ? (chipsOnSites.Count - wave + indexOfTarget) : (indexOfTarget - wave),
                        rightIndex = indexOfTarget + wave >= chipsOnSites.Count ? (wave - (chipsOnSites.Count - indexOfTarget)) : (indexOfTarget + wave);
                    if (rightWeight>leftWeight)
                    {
                        if (chipsOnSites[rightIndex]>average)
                        {
                            chipsOnSites[rightIndex]--;
                            chipsOnSites[indexOfTarget]++;
                            countOfChanges += wave;
                            break;
                        }
                    }
                    else if (chipsOnSites[leftIndex]>average)
                    {
                        chipsOnSites[leftIndex]--;
                        chipsOnSites[indexOfTarget]++;
                        countOfChanges += wave;
                        break;
                    }

                }
            }
            Console.WriteLine("Итоговое состояние");
            StateView(chipsOnSites);
            return countOfChanges;
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
