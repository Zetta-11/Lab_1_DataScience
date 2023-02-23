using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lab_1
{
    public partial class Form1 : Form
    {
        readonly Tuple<List<double>, List<double>> tupleXAndYRaw;

        public Form1()
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

        }
    }
}
