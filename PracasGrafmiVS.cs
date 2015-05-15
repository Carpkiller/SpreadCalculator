using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using SpreadCalculator.Obchody;
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


        public void VykresliSpread(string nazovGrafu, List<Spread> listSpread, List<Spread> listSpread1 = null)
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

            if (listSpread1 != null)
            {
                var series2 = new Series
                {
                    Name = "Odhad",
                    LegendText = nazovGrafu,
                    Color = Color.Black,
                    IsVisibleInLegend = true,
                    IsXValueIndexed = false,
                    ChartType = SeriesChartType.Line,
                    BorderWidth = 1
                };

                for (int i = 0; i < listSpread1.Count; i++)
                {
                    series2.Points.AddXY(listSpread1[i].Date, listSpread1[i].Value);
                }

                _graf.Series.Add(series2);
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

        public void VykresliKorelacnySpread(string nazovGrafu, List<KorelacnySpread> listGrafKorelacie)
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
            var startRok = listGrafKorelacie[0].Rok;

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
                    //Name = "Rok " + (listGrafKorelacie[i].Rok - i),
                    Name = "Rok " + (startRok - i),
                    ChartType = SeriesChartType.Line,
                    IsVisibleInLegend = true,
                    Color = listFarieb[i],
                    BorderWidth = 2,
                };
                for (int j = 0; j < listGrafKorelacie[i].Spread.Count; j++)
                {
                    series.Points.AddXY(listGrafKorelacie[i].Spread[j].Date, listGrafKorelacie[i].Spread[j].Value);
                    if (listGrafKorelacie[i].Spread[j].Value > max)
                    {
                        max = listGrafKorelacie[i].Spread[j].Value;
                    }
                    if (listGrafKorelacie[i].Spread[j].Value < min)
                    {
                        min = listGrafKorelacie[i].Spread[j].Value;
                    }
                }

                _graf.Series.Add(series);

            }
            if (listGrafKorelacie[0].Spread[0].Date < DateTime.Today)
            {
                var year = listGrafKorelacie[0].Spread[0].Date.Year;
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

        public void VykresliKorelacnyGraf(string nazovGrafuNormal, List<KorelacnyGraf> listGrafKorelacie)
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
            var startRok = listGrafKorelacie[0].Rok;

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
                    //Name = "Rok " + (listGrafKorelacie[i].Rok - i),
                    Name = "Rok " + (startRok - i),
                    ChartType = SeriesChartType.Line,
                    IsVisibleInLegend = true,
                    Color = listFarieb[i],
                    BorderWidth = 2,
                };
                for (int j = 0; j < listGrafKorelacie[i].Graf.Count; j++)
                {
                    series.Points.AddXY(listGrafKorelacie[i].Graf[j].Date, listGrafKorelacie[i].Graf[j].Settle);
                    if (listGrafKorelacie[i].Graf[j].Settle > max)
                    {
                        max = listGrafKorelacie[i].Graf[j].Settle;
                    }
                    if (listGrafKorelacie[i].Graf[j].Settle < min)
                    {
                        min = listGrafKorelacie[i].Graf[j].Settle;
                    }
                }

                _graf.Series.Add(series);

            }
            if (listGrafKorelacie[0].Graf[0].Date < DateTime.Today)
            {
                var year = listGrafKorelacie[0].Graf[0].Date.Year;
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

        public void VykresliSpreadObchod(string nazovGrafu, List<Spread> listSpread, Obchod obchod)
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

            var series2 = new Series
            {
                Name = "Series2",
                LegendText = nazovGrafu,
                Color = Color.Black,
                IsVisibleInLegend = true,
                IsXValueIndexed = false,
                ChartType = SeriesChartType.Point
            };

            for (int i = 0; i < listSpread.Count; i++)
            {
                series1.Points.AddXY(listSpread[i].Date, listSpread[i].Value);
            }

            series2.Points.AddXY(obchod.ZaciatokObchodu, obchod.VstupnaCena);
            if (obchod.VystupnaCena != null)
            {
                var koniecObchodu = obchod.VystupnaCena == null ? obchod.KoniecObchodu : DateTime.Now;
                series2.Points.AddXY(obchod.KoniecObchodu, obchod.VystupnaCena);
            }

            _graf.Series.Add(series1);
            _graf.Series.Add(series3);
            _graf.Series.Add(series2);
            _graf.Invalidate();
        }
    }
}
