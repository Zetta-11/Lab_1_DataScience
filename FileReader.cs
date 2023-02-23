using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Lab_1
{
    static class FileReader
    {

        public static Tuple<List<double>, List<double>> GetAndReadFile()
        {
            List<double> listOfRawDataX = new List<double>();
            List<double> listOfRawDataY = new List<double>();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return null;
            }
            string filename = openFileDialog.FileName;
            using (StreamReader fs = new StreamReader(filename, Encoding.UTF8))
            {
                string line;
                while ((line = fs.ReadLine()) != null)
                {
                    string[] splitLine = line.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);

                    listOfRawDataX.Add(Double.Parse(splitLine[0].Replace(".", ",").Trim()));
                    listOfRawDataY.Add(Double.Parse(splitLine[1].Replace(".", ",").Trim()));
                }
            }
            return Tuple.Create(listOfRawDataX, listOfRawDataY);
        }
    }
}