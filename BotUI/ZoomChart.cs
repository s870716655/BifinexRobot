using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Media;
using System.Windows;

using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace BotUI
{
    // Public members
    internal partial class ZoomChart: LiveCharts.WinForms.CartesianChart
    {
        internal ZoomChart()
        {
            InitComponent();
        }

        internal void AddPriceDatas(PriceData[] PriceDataArray, System.Windows.Media.Color Color)
        {
            ChartValues<DateTimePoint> DataList = new ChartValues<DateTimePoint>();
            for (int i = 0; i < PriceDataArray.Length; i++) {
                DataList.Add(new DateTimePoint(PriceDataArray[i].Timestamp, Convert.ToDouble(PriceDataArray[i].Price)));
            }

            LineSeries DataSeries = new LineSeries();
            DataSeries.Values = DataList;
            DataSeries.LineSmoothness = 0;
            DataSeries.Fill = System.Windows.Media.Brushes.Transparent;
            DataSeries.StrokeThickness = 1;
            DataSeries.PointGeometrySize = 0;
            DataSeries.Stroke = new System.Windows.Media.SolidColorBrush(Color);

            Series.Add(DataSeries);
        }

        internal void ClearPriceDatas()
        {
            Series.Clear();
        }
    }

    // Protected members
    internal partial class ZoomChart : LiveCharts.WinForms.CartesianChart
    {
    }

    // Private members
    internal partial class ZoomChart : LiveCharts.WinForms.CartesianChart
    {
        void InitComponent()
        {
            // Set axis
            Axis Axis_X = new Axis();
            Axis_X.LabelFormatter = val => new System.DateTime((long)val).ToString("yyyy/MM/dd/HH ");
            Axis Axis_Y = new Axis();
            Axis_Y.LabelFormatter = val => val.ToString("0");
            AxisX.Add(Axis_X);
            AxisY.Add(Axis_Y);

            // Set zoom properties
            Zoom = ZoomingOptions.X;
        }
    }
}
