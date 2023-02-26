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

        public static void AddInfoToCorrelationTable(Tuple<List<double>, List<double>> tuple, DataGridView dataGrid)
        {
            CreateColumnsForCorrelationTable(dataGrid);
        }

        private static void ClearRowsAndColums(DataGridView dataGrid)
        {
            dataGrid.Columns.Clear();
            dataGrid.Rows.Clear();
            dataGrid.RowHeadersVisible = false;
        }
    }
}
