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


    }
}