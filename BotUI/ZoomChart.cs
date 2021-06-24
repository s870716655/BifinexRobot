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
            InitData();
            InitComponent();
        }

        internal void AddData(DateTime Time, double Value)
        {
            m_Data.Add(new DateTimePoint(Time, Value));
        }
    }

    // Protected members
    internal partial class ZoomChart : LiveCharts.WinForms.CartesianChart
    {
    }

    // Private members
    internal partial class ZoomChart : LiveCharts.WinForms.CartesianChart
    {
        ChartValues<DateTimePoint> m_Data;

        void InitData()
        {
            m_Data = new ChartValues<DateTimePoint>();

            LineSeries DataSeries = new LineSeries();
            DataSeries.Values = m_Data;
            DataSeries.LineSmoothness = 0;
            DataSeries.Fill = System.Windows.Media.Brushes.Transparent;
            DataSeries.StrokeThickness = 1;
            DataSeries.PointGeometrySize = 0;
            DataSeries.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
            Series.Add(DataSeries);
        }

        void InitComponent()
        {
            // Set axis
            Axis Axis_X = new Axis();
            Axis_X.LabelFormatter = val => new System.DateTime((long)val).ToString("dd MM yyyy");
            Axis Axis_Y = new Axis();
            Axis_Y.LabelFormatter = val => val.ToString("0");
            AxisX.Add(Axis_X);
            AxisY.Add(Axis_Y);

            // Set zoom properties
            Zoom = ZoomingOptions.X;
        }
    }
}
