using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab_1
{
    static class Correlation
    {
        public static double GetPearsonCoef(Tuple<List<double>, List<double>> tuple)
        {
            double middleX = StatisticCharacteristics.GetAverage(tuple.Item1);
            double middleY = StatisticCharacteristics.GetAverage(tuple.Item2);

            double stdX = Math.Sqrt(tuple.Item1.Select(_ => Math.Pow(_ - middleX, 2)).Average());
            double stdY = Math.Sqrt(tuple.Item2.Select(_ => Math.Pow(_ - middleY, 2)).Average());

            double medianXY = tuple.Item1.Zip(tuple.Item2, (a, b) => (a, b)).Average(_ => _.a * _.b);

            return (medianXY - middleX * middleY) / (stdX * stdY);
        }

        private static double GetSpearmanTies(List<double> list)
        {
            var ties = list.GroupBy(_ => _)
                 .Select(_ => _.Count())
                 .Where(_ => _ > 1);

            return ties.Sum(_ => Math.Pow(_, 3) - _) / 12;
        }

        private static List<double> GetRank(List<double> list)
        {
            var rankDictionary = list.OrderBy(_ => _)
                .Zip(Enumerable.Range(1, list.Count()), (a, b) => (a, b))
                .GroupBy(t => t.a, t => t.b)
                .ToDictionary(_ => _.Key, _ => _.Average());

            return list.Select(_ => rankDictionary[_]).ToList();
        }

        public static double GetSpearmanCoef(Tuple<List<double>, List<double>> tuple)
        {
            var rx = GetRank(tuple.Item1);
            var ry = GetRank(tuple.Item2);

            var aCoef = GetSpearmanTies(rx);
            var bCoef = GetSpearmanTies(ry);

            var n = tuple.Item1.Count;
            var k = n * (Math.Pow(n, 2) - 1) / 6.0;

            var difference = rx.Zip(ry, (a, b) => (a, b))
                .Sum(_ => Math.Pow(_.a - _.b, 2));

            return (k - difference - aCoef - bCoef) / (Math.Sqrt(k - 2 * aCoef) * Math.Sqrt(k - 2 * bCoef));
        }

        public static Tuple<double, double> GetPearsonInterval(Tuple<List<double>, List<double>> tuple)
        {
            double r = GetPearsonCoef(tuple);

            double lowerBound = r + (r * (1.0 - r * r)) / (2.0 * tuple.Item1.Count())
                - StatisticCharacteristics.normalQuantil(1.0 - 0.05 / 2.0) *
                (1.0 - r * r) / Math.Sqrt(tuple.Item1.Count() - 1.0);

            double upperBound = r + (r * (1.0 - r * r)) / (2.0 * tuple.Item1.Count())
                + StatisticCharacteristics.normalQuantil(1.0 - 0.05 / 2.0) *
                (1.0 - r * r) / Math.Sqrt(tuple.Item1.Count() - 1.0);

            return Tuple.Create(lowerBound, upperBound);
        }

        public static Tuple<double, double> GetPearsonStatsAndQuantil(Tuple<List<double>, List<double>> tuple)
        {
            double r = GetPearsonCoef(tuple);

            double t = (r * Math.Sqrt(tuple.Item1.Count() - 2.0)) / Math.Sqrt(1.0 - r * r);
            double p = StatisticCharacteristics.studentQuantil(1.0 - 0.05 / 2.0, (double)tuple.Item1.Count - 2.0);

            return Tuple.Create(t, p);
        }


        public static Tuple<double, double> GetSpearmanStatsAndQuantil(Tuple<List<double>, List<double>> tuple)
        {
            double r = GetSpearmanCoef(tuple);

            double t = (r * Math.Sqrt(tuple.Item1.Count() - 2.0)) / Math.Sqrt(1.0 - r * r);
            double p = StatisticCharacteristics.studentQuantil(1.0 - 0.05 / 2.0, (double)tuple.Item1.Count - 2.0);

            return Tuple.Create(t, p);
        }

        public static string[] comparePearsonStats(Tuple<List<double>, List<double>> tuple)
        {
            Tuple<double, double> statsAndQUantil = GetPearsonStatsAndQuantil(tuple);

            return Math.Abs(statsAndQUantil.Item1) <= statsAndQUantil.Item2 ? new string[] { "Незначуща", "Нема" } : new string[] { "Значуща", "Є" };
        }

        public static string[] compareSpearmanStats(Tuple<List<double>, List<double>> tuple)
        {
            Tuple<double, double> statsAndQUantil = GetSpearmanStatsAndQuantil(tuple);

            return Math.Abs(statsAndQUantil.Item1) <= statsAndQUantil.Item2 ? new string[] { "Незначуща", "Нема" } : new string[] { "Значуща", "Є" };
        }

        //----------------------------------------------------Kendall--------------------------------------------------------

        private static bool CheckToConcondant(Tuple<double, double> p1, Tuple<double, double> p2)
        {
            return p1.Item1 > p2.Item1 && p1.Item2 > p2.Item2 || p1.Item1 < p2.Item1 && p1.Item2 < p2.Item2;
        }

        private static bool CheckToDiscondant(Tuple<double, double> p1, Tuple<double, double> p2)
        {
            return p1.Item1 > p2.Item1 && p1.Item2 < p2.Item2 || p1.Item1 < p2.Item1 && p1.Item2 > p2.Item2;
        }

        private static bool CheckXTies(Tuple<double, double> p1, Tuple<double, double> p2)
        {
            return p1.Item1 == p2.Item1;
        }

        private static bool CheckYTies(Tuple<double, double> p1, Tuple<double, double> p2)
        {
            return p1.Item2 == p2.Item2;
        }

        private static double PointComparator(Tuple<double, double> p1, Tuple<double, double> p2)
        {
            if (CheckToConcondant(p1, p2))
            {
                return 1;
            }
            else if (CheckToDiscondant(p1, p2))
            {
                return -1;
            }
            else
            {
                bool tx = CheckXTies(p1, p2);
                bool ty = CheckYTies(p1, p2);

                if (tx && ty)
                {
                    return 0;
                }
                else if (tx)
                {
                    return 0.5;
                }
                else
                {
                    return -0.5;
                }
            }
        }

        public static double GetKendallCoef(Tuple<List<double>, List<double>> tuple)
        {
            var points = tuple.Item1
                .Zip(tuple.Item2, (x, y) =>
                Tuple.Create(x, y));

            var combinations = points
                .SelectMany((p1, i) => points
                .Skip(i + 1)
                .Select(p2 => (p1, p2)));

            var tiesDictionary = combinations
                .GroupBy(t => PointComparator(t.p1, t.p2))
                .ToDictionary(_ => _.Key, _ => _.Count());

            int nc, nd, nx, ny;
            tiesDictionary.TryGetValue(1, out nc);
            tiesDictionary.TryGetValue(-1, out nd);
            tiesDictionary.TryGetValue(0.5, out nx);
            tiesDictionary.TryGetValue(-0.5, out ny);

            double result = (nc - nd) / (Math.Sqrt(nc + nd + nx) * Math.Sqrt(nc + nd + ny));

            return result;
        }
    }
}