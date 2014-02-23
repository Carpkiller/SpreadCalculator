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

            foreach (var item in myCustomObjects)
            {
                var x = (double)new XDate(DateTime.Parse(item.Date.ToShortDateString()));
                var y = item.Value;
                Console.WriteLine(x + " " + y);
                list.Add(x, y);
            }

            LineItem myCurve = myPane.AddCurve("My Curve", list, Color.Blue, SymbolType.None);
            zg1.AxisChange();

            return zg1;
        }
    }
}
