using System.Windows.Forms;

namespace SpreadCalculator.GrafickeKomponenty
{
    public partial class SpravaDat : Form
    {
        public SpravaDat(Jadro jadro)
        {
            InitializeComponent();
            listView1.Columns.AddRange(new[]
            {
                new ColumnHeader
                {
                    Text = @"Komodita"
                },
                new ColumnHeader
                {
                    Text = @"Pocet stiahnutych kontraktov"
                }
            });

            listView1.BeginUpdate();
            listView1.Items.AddRange(jadro.PocetKontraktov());
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listView1.EndUpdate();

        }
    }
}
