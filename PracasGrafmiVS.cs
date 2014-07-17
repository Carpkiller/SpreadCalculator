using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using SpreadCalculator.PomocneTriedy;

namespace SpreadCalculator
{
// ReSharper disable once InconsistentNaming
    public class PracasGrafmiVS
    {
        private readonly Chart _graf;

        public PracasGrafmiVS(Chart graf)
        {
            _graf = graf;
        }


        public void VykresliSpread(string nazovGrafu, List<Spread> listSpread)
        {
            _graf.Series.Clear();
            var series1 = new Series
            {
                Name = "Series1",
                Color = Color.Green,
                IsVisibleInLegend = true,
                IsXValueIndexed = false,
                ChartType = SeriesChartType.Line
            };

            var series3 = new Series
            {
                Name = "Series3",
                ChartType = SeriesChartType.Line,
                Color = Color.Blue,
                BorderWidth = 2,

            };

            for (int i = 0; i < listSpread.Count; i++)
            {
                series1.Points.AddXY(listSpread[i].Date, listSpread[i].Value);
            }

            _graf.Series.Add(series1);
            _graf.Series.Add(series3); 
            _graf.Invalidate();
        }
    }
}
