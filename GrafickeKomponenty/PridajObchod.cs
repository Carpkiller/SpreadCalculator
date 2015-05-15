using System;
using System.Globalization;
using System.Windows.Forms;
using SpreadCalculator.Obchody;

namespace SpreadCalculator.GrafickeKomponenty
{
    public partial class PridajObchod : Form
    {
        public string Spread { get; set; }
        public DateTime DatumVstupu { get; set; }
        public DateTime DatumVystupu { get; set; }
        public double VstupnaCena { get; set; }
        public double? VystupnaCena { get; set; }
        public int Uprava { get; set; }
        public bool Ukonceny { get; set; }

        public PridajObchod(string spread)
        {
            InitializeComponent();
            textBoxSpread.Text = spread;
            Uprava = -1;
        }

        public PridajObchod(Obchod obchod, int row)
        {
            InitializeComponent();
            textBoxSpread.Text = obchod.ZapisSpread;
            dateTimePickerVstup.Value = obchod.ZaciatokObchodu;
            dateTimePickerVystup.Value = obchod.KoniecObchodu;
            textBoxVstupnaCena.Text = obchod.VstupnaCena.ToString(CultureInfo.InvariantCulture);
            textBoxVystupnaCena.Text = obchod.VystupnaCena == null ? string.Empty : obchod.VystupnaCena.ToString();
            checkBox1.Checked = obchod.Ukonceny;
            Uprava = row;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DatumVstupu = dateTimePickerVstup.Value;
            DatumVystupu = dateTimePickerVystup.Value;
            Spread = textBoxSpread.Text;
            VstupnaCena = Convert.ToDouble(textBoxVstupnaCena.Text);
            VystupnaCena = string.IsNullOrEmpty(textBoxVystupnaCena.Text) ? (double?) null : Convert.ToDouble(textBoxVystupnaCena.Text);
            Ukonceny = checkBox1.Checked;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
