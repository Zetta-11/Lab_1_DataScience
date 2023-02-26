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
    }
}