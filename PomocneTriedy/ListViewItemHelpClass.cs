using System;
using System.Globalization;
using System.Windows.Forms;

namespace SpreadCalculator.PomocneTriedy
{
    public class ListViewItemHelpClass : ListViewItem
    {
        public ListViewItemHelpClass(Tuple<string, int> kontrakt)
            : base(new[] { "", ""})
        {
            SubItems[0].Text = kontrakt.Item1;
            SubItems[1].Text = kontrakt.Item2.ToString(CultureInfo.InvariantCulture);
        }
    }
}
