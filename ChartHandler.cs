using System;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lab_1
{
    static class ChartHandler
    {
        public static void BuildCorrelationField(Tuple<List<double>, List<double>> tupleXAndY, Chart chart, string x, string y)
        {
            GetSignsOnAxises(chart, x, y);
            chart.Series[0].Points.Clear();

            for (int i = 0; i < tupleXAndY.Item1.Count; i++)
            {
                chart.Series[0].Points.AddXY(tupleXAndY.Item1[i], tupleXAndY.Item2[i]);
            }

            SetZoomForChartTrue(chart);
        }

        private static void GetSignsOnAxises(Chart chart, string x, string y)
        {
            Axis ax = new Axis();
            ax.Title = x;
            chart.ChartAreas[0].AxisX = ax;
            Axis ay = new Axis();
            ay.Title = y;
            chart.ChartAreas[0].AxisY = ay;
        }

        private static void SetZoomForChartTrue(Chart chart)
        {
            chart.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            chart.ChartAreas[0].AxisX.ScaleView.SmallScrollMinSize = 0.1;
            chart.ChartAreas[0].AxisY.ScaleView.SmallScrollMinSize = 0.1;
            chart.ChartAreas[0].CursorX.Interval = 0.01;
            chart.ChartAreas[0].CursorY.Interval = 0.01;
            chart.ChartAreas[0].CursorY.IsUserEnabled = true;
            chart.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            chart.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            chart.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = true;
        }
    }


}
