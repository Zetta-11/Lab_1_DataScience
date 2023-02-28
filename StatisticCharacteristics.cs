using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab_1
{
    static class StatisticCharacteristics
    {
        const double c0 = 2.515517;
        const double c1 = 0.802853;
        const double c2 = 0.010328;
        const double d1 = 1.432788;
        const double d2 = 0.1892659;
        const double d3 = 0.001308;

        public static double GetAverage(List<double> list)
        {
            return list.Average();
        }

        public static Tuple<double, double> GetMeanInterval(List<double> list)
        {
            double lowerBoundForMean = (GetAverage(list) - studentQuantil(1.0 - 0.05 / 2.0, (double)list.Count - 1.0) *
                 (GetStandartDeviation(list) / Math.Sqrt(list.Count)));

            double upperBoundForMean = (GetAverage(list) + studentQuantil(1.0 - 0.05 / 2.0, (double)list.Count - 1.0) *
                 (GetStandartDeviation(list) / Math.Sqrt(list.Count)));

            return Tuple.Create(lowerBoundForMean, upperBoundForMean);
        }

        public static double GetMedian(List<double> list)
        {
            var sortedList = list.OrderBy(_ => _);
            int count = sortedList.Count();

            return sortedList.Count() % 2 == 0 ?
                   (sortedList.ElementAt(count / 2) + sortedList.ElementAt(count / 2 - 1)) / 2
                   : sortedList.ElementAt((count - 1) / 2);
        }

        public static Tuple<double, double> GetMedianInterval(List<double> list)
        {
            List<double> sortedList = list.OrderBy(_ => _).ToList<double>();

            int lowerBoundKoefForMedian = (int)(sortedList.Count() / 2.0 - normalQuantil(1.0 - 0.05 / 2.0) * Math.Sqrt(sortedList.Count()) / 2.0);
            int upperBoundKoefForMedian = (int)(sortedList.Count() / 2.0 + 1.0 + normalQuantil(1.0 - 0.05 / 2.0) * Math.Sqrt(sortedList.Count()) / 2.0);

            return Tuple.Create(sortedList[lowerBoundKoefForMedian], sortedList[upperBoundKoefForMedian]);
        }

        public static double GetStandartDeviation(List<double> list)
        {
            var average = list.Average();
            var sumOfSquares = list.Sum(_ => Math.Pow(_ - average, 2));

            return Math.Sqrt(sumOfSquares / (list.Count - 1));
        }

        private static double GetDevationForRootMeanSquare(List<double> list)
        {
            return Math.Sqrt(GetStandartDeviation(list) / Math.Sqrt(2 * list.Count));
        }

        public static Tuple<double, double> GetStandartDeviationInterval(List<double> list)
        {
            double lowerBoundForRootMeanSquare = GetStandartDeviation(list) - studentQuantil(1.0 - 0.05 / 2.0, (double)list.Count - 1.0) * GetDevationForRootMeanSquare(list);
            double upperBoundForRootMeanSquare = GetStandartDeviation(list) + studentQuantil(1.0 - 0.05 / 2.0, (double)list.Count - 1.0) * GetDevationForRootMeanSquare(list);

            return Tuple.Create(lowerBoundForRootMeanSquare, upperBoundForRootMeanSquare);
        }

        public static double GetSkewness(List<double> list)
        {
            double mean = list.Average();
            double standardDeviation = Math.Sqrt(list.Sum(x => Math.Pow(x - mean, 2)) / (list.Count - 1));

            return list.Sum(x => Math.Pow(x - mean, 3)) / ((list.Count - 1) * Math.Pow(standardDeviation, 3));
        }

        private static double GetSkewnessDevation(List<double> list)
        {
            return Math.Sqrt((6.0 * (list.Count - 2)) / ((list.Count + 1) * (list.Count + 3)));
        }

        public static Tuple<double, double> GetSkewnewssInterval(List<double> list)
        {
            double lowerBoundForSkewness = GetSkewness(list) - studentQuantil(1.0 - 0.05 / 2.0, (double)list.Count - 1.0) * GetSkewnessDevation(list);
            double upperBoundForSkewness = GetSkewness(list) + studentQuantil(1.0 - 0.05 / 2.0, (double)list.Count - 1.0) * GetSkewnessDevation(list);

            return Tuple.Create(lowerBoundForSkewness, upperBoundForSkewness);
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

        public static Tuple<double, double> GetKurtosisInterval(List<double> list)
        {
            double standartDevationForKurtosis = Math.Sqrt((24.0 * list.Count * (list.Count - 2) * (list.Count - 3)) /
                 ((Math.Pow(list.Count + 1, 2)) * (list.Count + 3) * (list.Count + 5)));

            double lowerBoundForKurtosis = GetKurtosis(list) - studentQuantil(1.0 - 0.05 / 2.0, (double)list.Count - 1.0) * standartDevationForKurtosis;
            double upperBoundForKurtosis = GetKurtosis(list) + studentQuantil(1.0 - 0.05 / 2.0, (double)list.Count - 1.0) * standartDevationForKurtosis;

            return Tuple.Create(lowerBoundForKurtosis, upperBoundForKurtosis);
        }

        public static double studentQuantil(double alpha, double v)
        {
            return normalQuantil(alpha) + 1.0 / v * g1(alpha) + 1.0 / (v * v) * g2(alpha) + 1.0 / Math.Pow(v, 3) * g3(alpha) + 1.0 / Math.Pow(v, 4) * g4(alpha);
        }

        private static double g1(double alpha)
        {
            return 0.25 * (Math.Pow(normalQuantil(alpha), 3)
                + normalQuantil(alpha));
        }

        private static double g2(double alpha)
        {
            return 1.0 / 96.0 * (5 * Math.Pow(normalQuantil(alpha), 5)
                + 16.0 * Math.Pow(normalQuantil(alpha), 3)
                + 3.0 * normalQuantil(alpha));
        }

        private static double g3(double alpha)
        {
            return 1.0 / 384.0 * (3.0 * Math.Pow(normalQuantil(alpha), 7)
                + 19.0 * Math.Pow(normalQuantil(alpha), 5)
                + 17.0 * Math.Pow(normalQuantil(alpha), 3)
                - 15.0 * normalQuantil(alpha));
        }

        private static double g4(double alpha)
        {
            return 1.0 / 92160.0 * (79.0 * Math.Pow(normalQuantil(alpha), 9)
                + 779.0 * Math.Pow(normalQuantil(alpha), 7)
                + 1482.0 * Math.Pow(normalQuantil(alpha), 5)
                - 1920.0 * Math.Pow(normalQuantil(alpha), 3)
                - 945.0 * normalQuantil(alpha));
        }

        public static double normalQuantil(double alpha)
        {
            double t;
            if (alpha <= 0.5)
            {
                t = Math.Sqrt(-2.0 * Math.Log(alpha));

                return -1.0 * (t - ((c0 + (c1 * t) + (c2 * t * t))
                / (1.0 + (d1 * t) + (d2 * t * t) + (d3 * t * t * t))));
            }
            else
            {
                alpha = 1 - alpha;
                t = Math.Sqrt(-2.0 * Math.Log(alpha));

                return t - ((c0 + (c1 * t) + (c2 * t * t))
                / (1.0 + (d1 * t) + (d2 * t * t) + (d3 * t * t * t)));
            }
        }
    }
}
