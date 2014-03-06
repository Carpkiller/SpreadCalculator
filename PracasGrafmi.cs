using SpreadCalculator.PomocneTriedy;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedGraph;

namespace SpreadCalculator
{
    public static class PracasGrafmi
    {

        public static ZedGraphControl KresliGraf(string nazovGrafu,List<Spread> myCustomObjects, ZedGraphControl zg1)
        {
            zg1.GraphPane.CurveList.Clear();
            zg1.GraphPane.GraphObjList.Clear();
            
            GraphPane myPane = zg1.GraphPane;

            // Set the titles and axis labels
            myPane.Title.Text = nazovGrafu;
            myPane.XAxis.Title.Text = "Date";
            myPane.YAxis.Title.Text = "$";
            myPane.XAxis.Type = AxisType.Date;

            PointPairList list = new PointPairList();
            int rok = myCustomObjects.First().Date.Year;
            DateTime predchadzDatum = myCustomObjects.First().Date;

            foreach (var item in myCustomObjects)
            {
                var x = (double)new XDate(DateTime.Parse(item.Date.ToShortDateString()));
                var y = item.Value;
                //Console.WriteLine(x + " " + y);
                list.Add(x, y);
                //Console.WriteLine(rok + " > " + item.Date.Year.ToString() + "  " + (item.Date + " > " + new DateTime(item.Date.Year, 1, 1)).ToString() + " " + (predchadzDatum + " < " + new DateTime(item.Date.Year, 1, 1)).ToString());
                //Console.WriteLine((rok > item.Date.Year).ToString() + "  " + (item.Date > new DateTime(item.Date.Year, 1, 1)).ToString() + " " + (predchadzDatum < new DateTime(item.Date.Year, 1, 1)).ToString());
                if (item.Date.Year != predchadzDatum.Year)
                {
                    LineItem line = new LineItem(String.Empty, new[] { x, x },    new[] { myPane.YAxis.Scale.Min, myPane.YAxis.Scale.Max },    Color.Black, SymbolType.None);
                    line.Line.Style = System.Drawing.Drawing2D.DashStyle.Dash;
                    line.Line.Width = 1f;
                    myPane.CurveList.Add(line);
                    Console.WriteLine("Datum  " + item.Date);
                }
                predchadzDatum = item.Date;
             }
         
            myPane.CurveList.Add(new LineItem("My Curve", list, Color.Blue, SymbolType.None));
            zg1.Refresh();
          //  LineItem myCurve = myPane.AddCurve("My Curve", list, Color.Blue, SymbolType.None);
            zg1.AxisChange();

            return zg1;
        }

        internal static ZedGraphControl KresliGraf(string nazovGrafu, List<Spread> myCustomObjects, List<Spread> myCustomObjects2, ZedGraphControl zg1)
        {
            zg1.GraphPane.CurveList.Clear();
            zg1.GraphPane.GraphObjList.Clear();

            GraphPane myPane = zg1.GraphPane;

            // Set the titles and axis labels
            myPane.Title.Text = nazovGrafu;
            myPane.XAxis.Title.Text = "Date";
            myPane.YAxis.Title.Text = "$";
            myPane.XAxis.Type = AxisType.Date;

            PointPairList list = new PointPairList();
            int rok = myCustomObjects.First().Date.Year;
            DateTime predchadzDatum = myCustomObjects.First().Date;

            foreach (var item in myCustomObjects)
            {
                var x = (double)new XDate(DateTime.Parse(item.Date.ToShortDateString()));
                var y = item.Value;
                //Console.WriteLine(x + " " + y);
                list.Add(x, y);
                //Console.WriteLine(rok + " > " + item.Date.Year.ToString() + "  " + (item.Date + " > " + new DateTime(item.Date.Year, 1, 1)).ToString() + " " + (predchadzDatum + " < " + new DateTime(item.Date.Year, 1, 1)).ToString());
                //Console.WriteLine((rok > item.Date.Year).ToString() + "  " + (item.Date > new DateTime(item.Date.Year, 1, 1)).ToString() + " " + (predchadzDatum < new DateTime(item.Date.Year, 1, 1)).ToString());
                if (item.Date.Year != predchadzDatum.Year)
                {
                    LineItem line = new LineItem(String.Empty, new[] { x, x }, new[] { myPane.YAxis.Scale.Min, myPane.YAxis.Scale.Max }, Color.Black, SymbolType.None);
                    line.Line.Style = System.Drawing.Drawing2D.DashStyle.Dash;
                    line.Line.Width = 1f;
                    myPane.CurveList.Add(line);
                    Console.WriteLine("Datum  " + item.Date);
                }
                predchadzDatum = item.Date;
            }

            myPane.CurveList.Add(new LineItem("Priebeh tento rok", list, Color.Blue, SymbolType.None));
            zg1.Invalidate();
            list = new PointPairList();
            rok = myCustomObjects2.First().Date.Year;
            predchadzDatum = myCustomObjects2.First().Date;

            foreach (var item in myCustomObjects2)
            {
                var x = (double)new XDate(DateTime.Parse(item.Date.ToShortDateString()));
                var y = item.Value;
                //Console.WriteLine(x + " " + y);
                list.Add(x, y);
                //Console.WriteLine(rok + " > " + item.Date.Year.ToString() + "  " + (item.Date + " > " + new DateTime(item.Date.Year, 1, 1)).ToString() + " " + (predchadzDatum + " < " + new DateTime(item.Date.Year, 1, 1)).ToString());
                //Console.WriteLine((rok > item.Date.Year).ToString() + "  " + (item.Date > new DateTime(item.Date.Year, 1, 1)).ToString() + " " + (predchadzDatum < new DateTime(item.Date.Year, 1, 1)).ToString());
            }

            myPane.CurveList.Add(new LineItem("Priebeh v minulosti", list, Color.Red, SymbolType.None));
            
            zg1.Refresh();
            //  LineItem myCurve = myPane.AddCurve("My Curve", list, Color.Blue, SymbolType.None);
            zg1.AxisChange();

            return zg1;
        }

        public static ZedGraphControl KresliJednoduchyGraf(string forecastGraf, List<ObchodnyDen> getDataPreGraf, ZedGraphControl zg1)
        {
            zg1.GraphPane.CurveList.Clear();
            zg1.GraphPane.GraphObjList.Clear();

            GraphPane myPane = zg1.GraphPane;

            // Set the titles and axis labels
            myPane.Title.Text = forecastGraf;
            myPane.XAxis.Title.Text = "Date";
            myPane.YAxis.Title.Text = "$";
            myPane.XAxis.Type = AxisType.Date;

            var spl = new StockPointList();
            int rok = getDataPreGraf.First().Date.Year;
            DateTime predchadzDatum = getDataPreGraf.First().Date;

            foreach (var den in getDataPreGraf)
            {
                var x = (double)new XDate(DateTime.Parse(den.Date.ToShortDateString()));

                double open = den.Open;
                double close = den.Settle;
                double hi = den.High;
                double low = den.Low;

                var pt = new StockPt(x, hi, low, open, close, 100000);
                spl.Add(pt);

                if (den.Date.Year != predchadzDatum.Year)
                {
                    LineItem line = new LineItem(String.Empty, new[] { x, x },    new[] { myPane.YAxis.Scale.Min, myPane.YAxis.Scale.Max },    Color.Black, SymbolType.None);
                    line.Line.Style = System.Drawing.Drawing2D.DashStyle.Dash;
                    line.Line.Width = 1f;
                    //myPane.CurveList.Add(line);
                    Console.WriteLine("Datum  " + den.Date);
                }
                predchadzDatum = den.Date;

            }

            JapaneseCandleStickItem myCurve = myPane.AddJapaneseCandleStick("trades", spl);
            myCurve.Stick.IsAutoSize = true;
            myCurve.Stick.Color = Color.Blue;

            // Use DateAsOrdinal to skip weekend gaps
            myPane.XAxis.Type = AxisType.DateAsOrdinal;

            // pretty it up a little
            //myPane.Chart.Fill = new Fill(Color.White, Color.LightGoldenrodYellow, 45.0f);
            //myPane.Fill = new Fill(Color.White, Color.FromArgb(220, 220, 255), 45.0f);

            // Tell ZedGraph to calculate the axis ranges
            zg1.AxisChange();
            zg1.Invalidate();

            return zg1;
        }
    }
}
