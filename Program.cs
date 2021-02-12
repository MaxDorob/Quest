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
                int indexOfTarget = chipsOnSites.IndexOf(chipsOnSites.Where(x=>x>average).Min());
                for (int wave = 1; wave <= chipsOnSites.Count/2&&chipsOnSites[indexOfTarget]>average; wave++)
                {
                    int leftIndexOfSubTarget, rightIndexOfSubTarget;
                    if (indexOfTarget - wave < 0)
                        leftIndexOfSubTarget = chipsOnSites.Count - indexOfTarget - wave;
                    else
                        leftIndexOfSubTarget = indexOfTarget - wave;
                    if (indexOfTarget + wave >= chipsOnSites.Count)
                        rightIndexOfSubTarget = 0 + wave - (chipsOnSites.Count - indexOfTarget);
                    else
                        rightIndexOfSubTarget = indexOfTarget + wave;
                    while (chipsOnSites[indexOfTarget]>average&&(chipsOnSites[rightIndexOfSubTarget]<average||chipsOnSites[leftIndexOfSubTarget]<average))
                    {
                        if (chipsOnSites[leftIndexOfSubTarget]<=chipsOnSites[rightIndexOfSubTarget])
                            chipsOnSites[leftIndexOfSubTarget]++;
                        else
                            chipsOnSites[rightIndexOfSubTarget]++;
                        chipsOnSites[indexOfTarget]--;
                        countOfChanges += wave;
                        StateView(chipsOnSites);
                    }
                }


            }
            Console.WriteLine("Итоговое состояние");
            StateView(chipsOnSites);
            return countOfChanges;
        }
        static void StateView(List<int> statements)
        {
            foreach (var item in statements)
            {
                Console.Write($"{item},");
            }
            Console.WriteLine();
        }
        
    }
}
