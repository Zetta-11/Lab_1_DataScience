using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lab_1
{
    public partial class Lab_1 : Form
    {
        Tuple<List<double>, List<double>> tupleXAndYRawAdd;

        public Lab_1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
        }

        private void closeProgramToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            DialogResult res = MessageBox.Show("Are you sure you want to EXIT?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void openFileToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Tuple<List<double>, List<double>> tupleXAndYRaw = FileReader.GetAndReadFile();


            TableCreator.AddInfoToMainTable(tupleXAndYRaw, dataGridView1);



            ChartHandler.BuildCorrelationField(tupleXAndYRaw, chart1, "X", "Y");

            TableCreator.AddInfoToStatisticCharacteristicsTable(dataGridView2, tupleXAndYRaw.Item1);
            TableCreator.AddInfoToStatisticCharacteristicsTable(dataGridView3, tupleXAndYRaw.Item2);

            TableCreator.AddInfoToCorrelationTable(tupleXAndYRaw, dataGridView4);
            TableCreator.AddInfoToAdditionalTable(tupleXAndYRaw, dataGridView5);

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem == null)
            {
                return;
            }
            tupleXAndYRawAdd = FileReader.ReadFile(new int[] { int.Parse(comboBox1.SelectedItem.ToString()), int.Parse(comboBox2.SelectedItem.ToString()) });

            TableCreator.AddInfoToTable(tupleXAndYRawAdd, dataGridView1);

            ChartHandler.BuildCorrelationField(tupleXAndYRawAdd, chart1, "X", "Y");
            TableCreator.AddInfoToStatisticCharacteristicsTable(dataGridView2, tupleXAndYRawAdd.Item1);
            TableCreator.AddInfoToStatisticCharacteristicsTable(dataGridView3, tupleXAndYRawAdd.Item2);

            TableCreator.AddInfoToCorrelationTable(tupleXAndYRawAdd, dataGridView4);
            TableCreator.AddInfoToAdditionalTable(tupleXAndYRawAdd, dataGridView5);
        }

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                return;
            }
            tupleXAndYRawAdd = FileReader.ReadFile(new int[] { int.Parse(comboBox1.SelectedItem.ToString()) - 1, int.Parse(comboBox2.SelectedItem.ToString()) - 1 });

            TableCreator.AddInfoToTable(tupleXAndYRawAdd, dataGridView1);
            ChartHandler.BuildCorrelationField(tupleXAndYRawAdd, chart1, "X", "Y");
            TableCreator.AddInfoToStatisticCharacteristicsTable(dataGridView2, tupleXAndYRawAdd.Item1);
            TableCreator.AddInfoToStatisticCharacteristicsTable(dataGridView3, tupleXAndYRawAdd.Item2);

            TableCreator.AddInfoToCorrelationTable(tupleXAndYRawAdd, dataGridView4);
            TableCreator.AddInfoToAdditionalTable(tupleXAndYRawAdd, dataGridView5);
        }
    }
}
