using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lab_1
{
    static class TableCreator
    {
        private static void CreateColumsForMainTableData(DataGridView dataGrid)
        {
            ClearRowsAndColums(dataGrid);

            dataGrid.Columns.Add("priority", "i");
            dataGrid.Columns.Add("x", "X");
            dataGrid.Columns.Add("y", "Y");
            dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private static void CreateColumsForStaticCharacteristics(DataGridView dataGrid)
        {
            ClearRowsAndColums(dataGrid);
            dataGrid.Columns.Add("name", "Назва");
            dataGrid.Columns.Add("meaning", "Значення");
            dataGrid.Columns.Add("interval", "95% Довірчий інтервал");
            dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private static void CreateColumnsForCorrelationTable(DataGridView dataGrid)
        {
            ClearRowsAndColums(dataGrid);
            dataGrid.Columns.Add("name", "Коефіцієнт кореляції");
            dataGrid.Columns.Add("grade", "Оцінка");
            dataGrid.Columns.Add("interval", "Довірчий інтервал");
            dataGrid.Columns.Add("interval", "Статистика");
            dataGrid.Columns.Add("quantil", "Квантиль");
            dataGrid.Columns.Add("conclusion", "Висновок");
            dataGrid.Columns.Add("generalConclusion", "Висновок щодо наявності взаємозв'язку");
            dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private static void CreateColumnsForAdditionalTable(DataGridView dataGrid)
        {
            ClearRowsAndColums(dataGrid);
            dataGrid.Columns.Add("name", "Оцінка коеф. Пірсона");
            dataGrid.Columns.Add("grade", "Оцінка кореляційного відношення");
            dataGrid.Columns.Add("interval", "Статистика");
            dataGrid.Columns.Add("quantil", "Квантиль");
            dataGrid.Columns.Add("conclusion", "Висновок щодо рівності");
            dataGrid.Columns.Add("generalConclusion", "Висновок щодо виду залежності");
            dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        public static void AddInfoToStatisticCharacteristicsTable(DataGridView dataGrid, List<double> list)
        {
            CreateColumsForStaticCharacteristics(dataGrid);

            dataGrid.Rows.Add("Середнє арифметичне", Math.Round(StatisticCharacteristics.GetAverage(list), 4),
                "[ " + Math.Round(StatisticCharacteristics.GetMeanInterval(list).Item1, 4) + "; "
                + Math.Round(StatisticCharacteristics.GetMeanInterval(list).Item2, 4) + " ]");

            dataGrid.Rows.Add("Медіана", Math.Round(StatisticCharacteristics.GetMedian(list), 4),
                "[ " + Math.Round(StatisticCharacteristics.GetMedianInterval(list).Item1, 4) + "; "
                + Math.Round(StatisticCharacteristics.GetMedianInterval(list).Item2, 4) + " ]");

            dataGrid.Rows.Add("Середньоквадритчне відхилення", Math.Round(StatisticCharacteristics.GetStandartDeviation(list), 4),
                "[ " + Math.Round(StatisticCharacteristics.GetStandartDeviationInterval(list).Item1, 4) + "; "
                + Math.Round(StatisticCharacteristics.GetStandartDeviationInterval(list).Item2, 4) + " ]");

            dataGrid.Rows.Add("Коефіцієнт асиметрії", Math.Round(StatisticCharacteristics.GetSkewness(list), 4),
                "[ " + Math.Round(StatisticCharacteristics.GetSkewnewssInterval(list).Item1, 4) + "; "
                + Math.Round(StatisticCharacteristics.GetSkewnewssInterval(list).Item2, 4) + " ]");

            dataGrid.Rows.Add("Коефіцієнт ексцесу", Math.Round(StatisticCharacteristics.GetKurtosis(list), 4),
                "[ " + Math.Round(StatisticCharacteristics.GetKurtosisInterval(list).Item1, 4) + "; "
                + Math.Round(StatisticCharacteristics.GetKurtosisInterval(list).Item2, 4) + " ]");
        }


        public static void AddInfoToMainTable(Tuple<List<double>, List<double>> tupleXAndYRaw, DataGridView dataGrid)
        {
            CreateColumsForMainTableData(dataGrid);

            for (int i = 0; i < tupleXAndYRaw.Item1.Count; i++)
            {
                dataGrid.Rows.Add();
                dataGrid.Rows[i].Cells[0].Value = i + 1;
                dataGrid.Rows[i].Cells[1].Value = tupleXAndYRaw.Item1[i];
                dataGrid.Rows[i].Cells[2].Value = tupleXAndYRaw.Item2[i];
            }
        }

        public static void AddInfoToTable(Tuple<List<double>, List<double>> tupleXAndYRaw, DataGridView dataGrid)
        {
            CreateColumsForMainTableData(dataGrid);

            for (int i = 0; i < tupleXAndYRaw.Item1.Count; i++)
            {
                dataGrid.Rows.Add();
                dataGrid.Rows[i].Cells[0].Value = i + 1;
                dataGrid.Rows[i].Cells[1].Value = tupleXAndYRaw.Item1[i];
                dataGrid.Rows[i].Cells[2].Value = tupleXAndYRaw.Item2[i];
            }
        }

        public static void AddInfoToCorrelationTable(Tuple<List<double>, List<double>> tuple, DataGridView dataGrid)
        {
            Tuple<double, double> boundsPearsonInterval = Correlation.GetPearsonInterval(tuple);
            Tuple<double, double> pearsonStatsAndQuantil = Correlation.GetPearsonStatsAndQuantil(tuple);
            Tuple<double, double> spearmanStatsAndQuantil = Correlation.GetSpearmanStatsAndQuantil(tuple);
            Tuple<double, double> kendallStatsAndQuantil = Correlation.GetKendallStatsAndQuantil(tuple);
            Tuple<double, double> correlationRatioStatsAndQuantil = Correlation.GetCorrelationRatioStatsAndQuantil(tuple);

            CreateColumnsForCorrelationTable(dataGrid);
            dataGrid.Rows.Add("Пірсона", Math.Round(Correlation.GetPearsonCoef(tuple), 4),
                "[ " + Math.Round(boundsPearsonInterval.Item1, 4) + "; " +
                Math.Round(boundsPearsonInterval.Item2, 4) + " ]",
                Math.Round(pearsonStatsAndQuantil.Item1, 4),
                Math.Round(pearsonStatsAndQuantil.Item2, 4),
                Correlation.comparePearsonStats(tuple)[0],
                Correlation.comparePearsonStats(tuple)[1]);

            dataGrid.Rows.Add("Спірмена", Math.Round(Correlation.GetSpearmanCoef(tuple), 4), "-",
                Math.Round(spearmanStatsAndQuantil.Item1, 4),
                Math.Round(spearmanStatsAndQuantil.Item2, 4),
                Correlation.compareSpearmanStats(tuple)[0],
                Correlation.compareSpearmanStats(tuple)[1]);

            dataGrid.Rows.Add("Кендела", Math.Round(Correlation.GetKendallCoef(tuple), 4), "-",
                Math.Round(kendallStatsAndQuantil.Item1, 4),
                Math.Round(kendallStatsAndQuantil.Item2, 4),
                Correlation.compareKendallStats(tuple)[0],
                Correlation.compareKendallStats(tuple)[1]);

            dataGrid.Rows.Add("Кореляційне відношення",
                Math.Round(Correlation.GetCorrelationRatio(Tuple.Create(Correlation.TransformList(tuple.Item1), tuple.Item2)), 4),
                "-",
                Math.Round(correlationRatioStatsAndQuantil.Item1, 4),
                Math.Round(correlationRatioStatsAndQuantil.Item2, 4),
                Correlation.compareCorrelationRatioStats(tuple)[0],
                Correlation.compareCorrelationRatioStats(tuple)[1]);
        }

        public static void AddInfoToAdditionalTable(Tuple<List<double>, List<double>> tuple, DataGridView dataGrid)
        {
            CreateColumnsForAdditionalTable(dataGrid);

            if (Correlation.compareCorrelationRatioStats(tuple)[0] == "Значуща")
            {
                Tuple<double, double> addTuple = Correlation.GetAdditionalStatsAndQuantil(tuple);
                dataGrid.Rows.Add(Math.Round(Correlation.GetPearsonCoef(Tuple.Create(Correlation.TransformList(tuple.Item1), tuple.Item2)), 4),
                    Math.Round(Correlation.GetCorrelationRatio(Tuple.Create(Correlation.TransformList(tuple.Item1), tuple.Item2)), 4),
                    Math.Round(addTuple.Item1, 4),
                    Math.Round(addTuple.Item2, 4),
                    Correlation.compareAdditionalStats(tuple)[0],
                    Correlation.compareAdditionalStats(tuple)[1]);
            }
        }

        private static void ClearRowsAndColums(DataGridView dataGrid)
        {
            dataGrid.Columns.Clear();
            dataGrid.Rows.Clear();
            dataGrid.RowHeadersVisible = false;
        }
    }
}
