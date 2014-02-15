using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpreadCalculator.PomocneTriedy;
using ZedGraph;

namespace SpreadCalculator
{
    public partial class Graf : Form
    {
        public Graf(List<Spread> myCustomObjects, string file)
        {
            InitializeComponent();
            GraphPane myPane = zg1.GraphPane;

            // Set the titles and axis labels
            myPane.Title.Text = file;
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
        }

        private void Graf_Load(object sender, EventArgs e)
        {

        }
    }
}
