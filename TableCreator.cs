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

        public static void AddInfoToStatisticCharacteristicsTable(DataGridView dataGrid, List<double> list)
        {
            CreateColumsForStaticCharacteristics(dataGrid);

            dataGrid.Rows.Add("Середнє арифметичне", StatisticCharacteristics.GetAverage(list));
            dataGrid.Rows.Add("Медіана", StatisticCharacteristics.GetMedian(list));
            dataGrid.Rows.Add("Середньоквадритчне відхилення", StatisticCharacteristics.GetStandartDeviation(list));
            dataGrid.Rows.Add("Коефіцієнт асиметрії");
            dataGrid.Rows.Add("Коефіцієнт ексцесу");
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

        private static void ClearRowsAndColums(DataGridView dataGrid)
        {
            dataGrid.Columns.Clear();
            dataGrid.Rows.Clear();
        }
    }
}
