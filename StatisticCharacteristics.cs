using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab_1
{
    static class StatisticCharacteristics
    {

        public static double GetAverage(List<double> list)
        {
            return list.Average();
        }

        public static double GetMedian(List<double> list)
        {
            var sortedList = list.OrderBy(_ => _);
            int count = sortedList.Count();

            return sortedList.Count() % 2 == 0 ?
                   (sortedList.ElementAt(count / 2) + sortedList.ElementAt(count / 2 - 1)) / 2
                   : sortedList.ElementAt((count - 1) / 2);
        }

        public static double GetStandartDeviation(List<double> list)
        {
            var average = list.Average();
            var sumOfSquares = list.Sum(_ => Math.Pow(_ - average, 2));

            return Math.Sqrt(sumOfSquares / (list.Count - 1));
        }

        public static double GetSkewness(List<double> list)
        {
            double mean = list.Average();
            double standardDeviation = Math.Sqrt(list.Sum(x => Math.Pow(x - mean, 2)) / (list.Count - 1));

            return list.Sum(x => Math.Pow(x - mean, 3)) / ((list.Count - 1) * Math.Pow(standardDeviation, 3));
        }

        public static double GetKurtosis(List<double> list)
        {
            double mean = list.Average();
            double variance = list.Sum(x => Math.Pow(x - mean, 2)) / (list.Count - 1.0);
            double standardDeviation = Math.Sqrt(variance);
            double temp = (list.Sum(x => Math.Pow(x - mean, 4)) / ((list.Count - 1) * Math.Pow(standardDeviation, 4)) - 3.0)
                           + 6.0 / (list.Count + 1.0);

            return ((Math.Pow(list.Count, 2) - 1.0) / ((list.Count - 2.0) * (list.Count - 3.0))) * temp;

        }
    }
}
