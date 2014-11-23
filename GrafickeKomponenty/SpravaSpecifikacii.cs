using System.Globalization;
using System.Windows.Forms;
using SpreadCalculator.PomocneTriedy;

namespace SpreadCalculator.GrafickeKomponenty
{
    public partial class SpravaSpecifikacii : Form
    {
        private Jadro _jadro;
        public SpravaSpecifikacii(Jadro jadro)
        {
            _jadro = jadro;
            InitializeComponent();
            var kontrakty = _jadro.LoadKontrakty();
            listBox1.DataSource = kontrakty.GetRange(1, kontrakty.Count - 1);
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var specikacie = _jadro.LoadSpecifikacie(listBox1.SelectedIndex);

            textBoxBurza.Text = specikacie.Burza;
            textBoxEnd.Text = specikacie.EndRok;
            textBoxHodnotaBodu.Text = specikacie.HodnotaBod;
            textBoxHodnotaTicku.Text = specikacie.VelkostTicku.ToString(CultureInfo.CurrentCulture);
            textBoxKategoria.Text = specikacie.Kategoria;
            textBoxMesiace.Text = specikacie.TypyKontraktov;
            textBoxNazov.Text = specikacie.Komodita;
            textBoxStart.Text = specikacie.StartRok;
            textBoxSymbol.Text = specikacie.Symbol;
            textBoxUrl.Text = specikacie.Url;
            textBoxUrlCon.Text = specikacie.UrlCon;
        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            var specikacia = new SirsiaSpecifikaciaKontraktu
            {
                Burza = textBoxBurza.Text,
                EndRok = textBoxEnd.Text,
                HodnotaBod = textBoxHodnotaBodu.Text,
                Kategoria = textBoxKategoria.Text,
                Komodita = textBoxNazov.Text,
                StartRok = textBoxStart.Text,
                Symbol = textBoxSymbol.Text,
                TypyKontraktov = textBoxMesiace.Text,
                Url = textBoxUrl.Text,
                UrlCon = textBoxUrlCon.Text,
                VelkostTicku = double.Parse(textBoxHodnotaTicku.Text,CultureInfo.GetCultureInfoByIetfLanguageTag("sk-SK"))
            };

            _jadro.UlozZmenySpecifikacii(specikacia, listBox1.SelectedIndex);
        }
    }
}
