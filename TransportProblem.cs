using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ConsoleApp7
{
    public class TransportProblem
    {
        public int funcion;
        Dictionary<Point, int> Costs;
        int[] Users, Storages;
        Dictionary<Point, int?> LessCostTable;
        public TransportProblem(int[,] Costs, int[] Stores, int[] Storages)
        {
            this.Costs = new Dictionary<Point, int>();
            LessCostTable = new Dictionary<Point, int?>();
            for (int i = 0; i < Storages.Length; i++)
            {
                for (int j = 0; j < Stores.Length; j++)
                {
                    this.Costs.Add(new Point(i, j), Costs[i, j]);
                    LessCostTable.Add(new Point(i, j), null);
                }
            }
            this.Users = Stores;
            this.Storages = Storages;
            NorthWestMethod();
            Potential();
            funcion = 0;
            foreach (var item in LessCostTable.Where(x => x.Value != null))
            {
                funcion += item.Value.Value * this.Costs[item.Key];
            }

            }
        void NorthWestMethod()//метод Север-западного угла
        {
            int i = 0, j = 0;
            while (j != Users.Length)
            {
                var min = Math.Min(Users[j], Storages[i]);
                LessCostTable[new Point(i, j)] = min;
                Storages[i] -= min;
                Users[j] -= min;
                if (Storages[i] == 0) i++;
                if (Users[j] == 0) j++;
            }
            while (LessCostTable.Count(x => x.Value != null) < Users.Length + Storages.Length - 1)
            {
                for (int x = 1; x < Storages.Length; x++)
                {
                    bool br = false;
                    for (int z = 1; z < Users.Length; z++)
                    {
                        if (LessCostTable[new Point(x, z - 1)] == null && LessCostTable[new Point(x - 1, z)] == null && LessCostTable[new Point(x, z)] != null)
                        {
                            LessCostTable[new Point(x, z - 1)] = 0;
                            br = true;
                            break;
                        }
                    }
                    if (br) break;


                }
            }
        }
        int?[] u, v;
        void Potential()//Метод потенциалов
        {

            u = new int?[Storages.Length];
            v = new int?[Users.Length];
            u[0] = 0;
            while (!u.All(x => x != null) || !v.All(x => x != null))
            {
                for (int i = 0; i < Storages.Length; i++)
                {
                    for (int j = 0; j < Users.Length; j++)
                    {


                        if (LessCostTable[new Point(i, j)] == null) continue;
                        if (u[i] != null) v[j] = Costs[new Point(i, j)] - u[i];
                        if (v[j] != null) u[i] = Costs[new Point(i, j)] - v[j];
                    }
                }
            }
            if (Costs.Where(x => LessCostTable[x.Key] == null).Any(x => u[x.Key.X] + v[x.Key.Y] > x.Value))
            {
                int max = Costs.Where(x => LessCostTable[x.Key] == null).Max(x => u[x.Key.X].Value + v[x.Key.Y].Value - x.Value);
                CreateNewStandPlan(Costs.Where(x => LessCostTable[x.Key] == null).First(x => u[x.Key.X].Value + v[x.Key.Y].Value - x.Value == max).Key);
            }
            else return;
        }

        void CreateNewStandPlan(Point perspectivePos)//создание нового опорного плана
        {
            //funcion = 0;
            //foreach (var item in LessCostTable.Where(x => x.Value != null))
            //{
            //    funcion += item.Value.Value * this.Costs[item.Key];
            //}
            var listOfVerticles = LessCostTable.Keys.Where(x => LessCostTable[x] != null).ToList();
            listOfVerticles.Add(perspectivePos);
            var path = FindPath(new List<Point> { perspectivePos }, listOfVerticles);
            OptimizePath(path);
            Dictionary<Point, int> vert = new Dictionary<Point, int>();
            int mp = -1;
            foreach (var item in path)
            {
                vert.Add(item, -mp);
                mp = -mp;
            }
            Point toNull = new Point(-1, -1);
            int multipy = int.MaxValue;
            foreach (var item in LessCostTable.Where(x=>vert.ContainsKey(x.Key)&& vert[x.Key]<0&&x.Value!=null).ToDictionary(x=>x.Key,x=>x.Value))
            {
                if (multipy>=item.Value)
                {
                    multipy = item.Value.Value;
                    toNull = item.Key;
                }
            }
            LessCostTable[perspectivePos] = 0;
            LessCostTable[toNull] = null;
            
            foreach (var item in vert)
            {
                if (LessCostTable[item.Key] != null)
                    LessCostTable[item.Key] += item.Value * multipy;
            }
            
            Potential();

        }
        void OptimizePath(List<Point> toOpmize)//отсечение лишних точек на линиях(3 и более точки)
        {
            bool optimization = true;
            while (optimization)
            {
                int countOfPointsInLine = 1;
                bool horizontalMovement = true;
                for (int i = 1; i < toOpmize.Count; i++)
                {
                    if (horizontalMovement&&toOpmize[i].X!=toOpmize[i-1].X)
                    {
                        horizontalMovement = false;
                        countOfPointsInLine = 1;
                    }
                    else if (!horizontalMovement&&toOpmize[i].Y!=toOpmize[i-1].Y)
                    {
                        horizontalMovement = true;
                        countOfPointsInLine = 1;
                    }
                    countOfPointsInLine++;
                    if (countOfPointsInLine>2)
                    {
                        toOpmize.RemoveAt(i - 1);
                        break;
                    }
                    if (i==toOpmize.Count-1)
                    {
                        optimization=false;
                    }
                }
                while (toOpmize[0].X == toOpmize[toOpmize.Count - 2].X || toOpmize[0].Y == toOpmize[toOpmize.Count-2].Y)
                    toOpmize.RemoveAt(toOpmize.Count - 1);

            }
            return;
        }
        List<Point> FindPath(List<Point> Scaned, List<Point> verticles)//поиск кругового пути
        {
            var arr = verticles.Where(x => x.Y == Scaned.Last().Y && x.X < Scaned.Last().X);
            Point nearesTopPoint = arr.Count() == 0 ? new Point(-1, -1) : arr.First(x => Scaned.Last().X - x.X == arr.Min(z => Scaned.Last().X - z.X));

            arr = verticles.Where(x => x.Y == Scaned.Last().Y && x.X > Scaned.Last().X);
            Point nearestBottomPoint = arr.Count() == 0 ? new Point(-1, -1) : arr.First(x => x.X - Scaned.Last().X == arr.Min(z => z.X - Scaned.Last().X));

            arr = verticles.Where(x => x.X == Scaned.Last().X && x.Y < Scaned.Last().Y);
            Point nearestLeftPoint = arr.Count() == 0 ? new Point(-1, -1) : arr.First(x => Scaned.Last().Y - x.Y == arr.Min(z => Scaned.Last().Y - z.Y));

            arr = verticles.Where(x => x.X == Scaned.Last().X && x.Y > Scaned.Last().Y);
            Point nearestRightPoint = arr.Count() == 0 ? new Point(-1, -1) : arr.First(x => x.Y - Scaned.Last().Y == arr.Min(z => z.Y - Scaned.Last().Y));

            List<Point> returned = null;

            if (Scaned.Count > 3 && (nearestBottomPoint == Scaned[0] || nearestLeftPoint == Scaned[0] || nearesTopPoint == Scaned[0] || nearestRightPoint == Scaned[0]))
            {
                return Scaned;
            }
            if (nearesTopPoint != new Point(-1, -1) && !Scaned.Contains(nearesTopPoint))
                returned = FindPath(new List<Point>(Scaned) { nearesTopPoint }, verticles);
            if (returned != null) return returned;
            if (nearestBottomPoint != new Point(-1, -1) && !Scaned.Contains(nearestBottomPoint))
                returned = FindPath(new List<Point>(Scaned) { nearestBottomPoint }, verticles);
            if (returned != null) return returned;
            if (nearestLeftPoint != new Point(-1, -1) && !Scaned.Contains(nearestLeftPoint))
                returned = FindPath(new List<Point>(Scaned) { nearestLeftPoint }, verticles);
            if (returned != null) return returned;

            
            if (nearestRightPoint != new Point(-1, -1) && !Scaned.Contains(nearestRightPoint))
                returned = FindPath(new List<Point>(Scaned) { nearestRightPoint }, verticles);
            if (returned != null) return returned;

            return null;
        }
    }
}
