using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
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
                LegendText = nazovGrafu,
                Color = Color.Green,
                IsVisibleInLegend = true,
                IsXValueIndexed = false,
                ChartType = SeriesChartType.Line
            };

            var series3 = new Series
            {
                Name = "Series3",
                ChartType = SeriesChartType.Line,
                IsVisibleInLegend = false,
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

        public void VykreliGraf(string nazovGrafu, List<ObchodnyDen> listData)
        {
            _graf.Series.Clear();
            var series1 = new Series
            {
                Name = "Series1",
                Color = Color.Green,
                //Legend = nazovGrafu,
                IsVisibleInLegend = false,
                IsXValueIndexed = false,
                ChartType = SeriesChartType.Candlestick
            };

            _graf.Series.Add(series1);

            for (int i = 0; i < listData.Count; i++)
            {
                series1.Points.AddXY(listData[i].Date, listData[i].High,listData[i].Low,listData[i].Open,listData[i].Settle);
            }
            
            _graf.Invalidate();
        }

        public void VykresliKorelacnySpread(string nazovGrafu, List<List<Spread>> listGrafKorelacie)
        {
            var listFarieb = new List<Color>
            {
                Color.Blue,
                Color.Brown,
                Color.BurlyWood,
                Color.CadetBlue,
                Color.Chartreuse,
                Color.Chocolate,
                Color.DarkCyan,
                Color.DarkMagenta,
                Color.Firebrick,
                Color.Black,
                Color.LightSeaGreen,
                Color.Green
            };

            double min = 99999;
            double max = -99999;

            _graf.Series.Clear();

            var series3 = new Series
            {
                //Name = "Series3",
                ChartType = SeriesChartType.Line,
                IsVisibleInLegend = false,
                Color = Color.Black,
                BorderWidth = 3,

            };

            for (int i = 0; i < listGrafKorelacie.Count; i++)
            {
                var series = new Series
                {
                    Name = "Rok " + (int.Parse(listGrafKorelacie[i][0].Date.Year.ToString(CultureInfo.InvariantCulture)) - i),
                    ChartType = SeriesChartType.Line,
                    IsVisibleInLegend = true,
                    Color = listFarieb[i],
                    BorderWidth = 2,
                };
                for (int j = 0; j < listGrafKorelacie[i].Count; j++)
                {
                    series.Points.AddXY(listGrafKorelacie[i][j].Date, listGrafKorelacie[i][j].Value);
                    if (listGrafKorelacie[i][j].Value > max)
                    {
                        max = listGrafKorelacie[i][j].Value;
                    }
                    if (listGrafKorelacie[i][j].Value < min)
                    {
                        min = listGrafKorelacie[i][j].Value;
                    }
                }

                _graf.Series.Add(series);

            }
            if (listGrafKorelacie[0][0].Date < DateTime.Today)
            {
                var year = listGrafKorelacie[0][0].Date.Year;
                var date = DateTime.Today;
                series3.Points.AddXY(date.AddYears(year - date.Year), max);
                series3.Points.AddXY(date.AddYears(year - date.Year), min);    
            }
            else
            {
                series3.Points.AddXY(DateTime.Today.Date, max);
                series3.Points.AddXY(DateTime.Today.Date, min);    
            }

            _graf.Series.Add(series3);
            _graf.Series.Add(new Series
            {
                IsVisibleInLegend = false,
                ChartType = SeriesChartType.Line,
                BorderWidth = 2
            });
            _graf.Invalidate();
        }
    }
}
