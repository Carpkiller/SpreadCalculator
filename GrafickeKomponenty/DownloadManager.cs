using System.Collections.Generic;
using System.Windows.Forms;

namespace SpreadCalculator.GrafickeKomponenty
{
    public partial class DownloadManager : Form
    {
        private Jadro _jadro;

        public DownloadManager(Jadro jadro)
        {
            _jadro = jadro;
            _jadro.VytvorDownloadManagera();
            InitializeComponent();
            comboBoxPocetRokov.SelectedIndex = 0;
            comboBoxKomodita.DataSource = _jadro.LoadKontrakty();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            _jadro.DownloadManager.StiahniData(
                comboBoxKomodita.SelectedItem.ToString(), 
                int.Parse(comboBoxPocetRokov.SelectedItem.ToString()), 
                comboBoxRok.SelectedItem.ToString(),
                comboBoxMesiac1.SelectedItem.ToString(),
                comboBoxMesiac2.SelectedItem.ToString(),
                checkBoxVsetko.Checked);
        }

        private void comboBoxKomodita_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (!ReferenceEquals(comboBoxKomodita.SelectedItem, "-----------"))
            {
                comboBoxRok.DataSource = _jadro.LoadRokySpecificke(comboBoxKomodita.SelectedItem.ToString());
            }
        }

        private void comboBoxRok_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var predZnak = comboBoxMesiac1.SelectedIndex;
            if (!ReferenceEquals(comboBoxKomodita.SelectedItem, "----------"))
            {
                var listMesiacov = _jadro.LoadMesiaceSpecificke(comboBoxKomodita.SelectedItem.ToString(),
                    comboBoxRok.SelectedItem.ToString());
                if (listMesiacov != null)
                {
                    comboBoxMesiac1.DataSource = listMesiacov;
                    comboBoxMesiac2.DataSource = new List<string>(listMesiacov);
                    comboBoxKomodita.Enabled = true;
                    if (listMesiacov.Count >= predZnak)
                    {
                        comboBoxMesiac1.SelectedIndex = predZnak;
                    }
                }
            }
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
