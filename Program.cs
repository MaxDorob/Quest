using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp7
{
    class Program
    {
        static void Main(string[] args)
        {            
            List<int> startStatement = new List<int>();
            string inputStatement = Console.ReadLine();
            inputStatement = inputStatement.Replace(" ", "");
            foreach (var item in inputStatement.Split(","))
            {
                startStatement.Add(int.Parse(item));
            }
            Console.WriteLine("Количество перемещений: "+ReRoll(startStatement));
        }
        static int ReRoll(List<int> chipsOnSites)
        {
            StateView(chipsOnSites);
            int countOfChanges = 0;
            int average =(int)chipsOnSites.Average();
            while (!chipsOnSites.All(x=>x==average))
            {
                int indexOfTarget = chipsOnSites.IndexOf(chipsOnSites.Where(x=>x<average).Max());
                StateViewModified(chipsOnSites, indexOfTarget);
                int leftWeight=0, rightWeight=0;
                for (int i = 1; i <= chipsOnSites.Count/2; i++)
                {
                    int leftIndex=indexOfTarget-i<0?(chipsOnSites.Count-i+indexOfTarget):(indexOfTarget-i),
                        rightIndex=indexOfTarget+i>=chipsOnSites.Count?(i-(chipsOnSites.Count-indexOfTarget)):(indexOfTarget+i);
                    leftWeight += chipsOnSites[leftIndex];
                    rightWeight += chipsOnSites[rightIndex];
                }
                for (int wave = 1; wave <= chipsOnSites.Count/2; wave++)
                {
                    int leftIndex = indexOfTarget - wave < 0 ? (chipsOnSites.Count - wave + indexOfTarget) : (indexOfTarget - wave),
                        rightIndex = indexOfTarget + wave >= chipsOnSites.Count ? (wave - (chipsOnSites.Count - indexOfTarget)) : (indexOfTarget + wave);
                    if (rightWeight>=leftWeight)
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
