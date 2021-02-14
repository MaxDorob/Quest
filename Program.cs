using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ConsoleApp7
{
    class Program
    {
        static void Main(string[] args)
        {
            
            ReRoll(new List<int> { 1, 5, 9, 10, 5 });
            ReRoll(new List<int> { 1, 2, 3 });
            ReRoll(new List<int> { 0, 1, 1, 1, 1, 1, 1, 1, 1, 2 });
            ReRoll(new List<int> { 6, 2, 4, 10, 3 });
            ReRoll(new List<int> { 0, 10, 0, 8, 3, 10, 7, 0, 9, 3 });
            ReRoll(new List<int> { 6, 14, 22, 12, 6, 25, 15, 14, 29, 21, 11, 14, 25, 13, 13 });
            ReRoll(new List<int> { 18, 22, 30, 21, 2, 20, 22, 8, 30, 30, 7, 23, 1, 22, 8, 23, 7, 22, 25, 26, 17, 30, 27, 6, 25, 29, 20, 9, 3, 25, 16, 16, 30, 30, 8, 15, 27, 25, 6, 22, 16, 10, 24, 14, 26, 0, 13, 28, 11, 5 });
            List<int> startStatement = new List<int>();
            string inputStatement = Console.ReadLine();
            inputStatement = inputStatement.Replace(" ", "");
            foreach (var item in inputStatement.Split(","))
            {
                startStatement.Add(int.Parse(item));
            }
            ReRoll(startStatement);
        }
        static void ReRoll(List<int> chipsOnSites)
        {

            int average = (int)chipsOnSites.Average(),
                n = chipsOnSites.Count(x => x > average),
                m = chipsOnSites.Count(x => x < average),
                i = 0,
                j = 0;
            int[] storage = new int[n];
            int[] store = new int[m];
            int[,] ways = new int[n, m];
            for (int z = 0; z < chipsOnSites.Count; z++)
            {
                if (chipsOnSites[z] > average)
                {
                    storage[i] = chipsOnSites[z] - average;
                    j = 0;
                    for (int x = 0; x < chipsOnSites.Count; x++)
                    {
                        if (chipsOnSites[x] < average)
                        {
                            store[j] = average - chipsOnSites[x];
                            ways[i, j] = ShortestWayInCircle(z, x, chipsOnSites.Count);//Приведение к матричной форме
                            j++;
                        }
                    }
                    i++;
                }
            }
            Console.WriteLine( new TransportProblem(ways, store, storage).funcion);
        }
        

        static int ShortestWayInCircle(int pos, int target, int size)//Определение стоимостей для матричной таблицы
        {
            for (int wave = 0; wave <= size / 2; wave++)
            {
                int leftIndex = pos - wave < 0 ? (size - wave + pos) : (pos - wave),
                            rightIndex = pos + wave >= size ? (wave - (size - pos)) : (pos + wave);
                if (rightIndex == target || leftIndex == target)
                {
                    return wave;
                }
            }
            return -10000;
        }

    }
}
