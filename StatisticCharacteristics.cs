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
    }
}
