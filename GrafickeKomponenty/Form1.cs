using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SpreadCalculator
{
    public partial class Form1 : Form
    {
        private Jadro jadro;

        public Form1()
        {
            InitializeComponent();
            comboBoxKontrakt1.SelectedIndex = 0;
            comboBoxKontrakt2.SelectedIndex = 0;

            zg1.Visible =false;
            jadro = new Jadro();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBoxKontrakt1.Enabled && comboBoxKontrakt2.Enabled)
            {
                jadro.parsujKontrakty(comboBoxKontrakt1.SelectedIndex, comboBoxKontrakt2.SelectedIndex);
            }
            //  MessageBox.Show(jadro.parsujKontrakty("C:\\_WA\\WindowsHttpNacuvac\\SpreadCalculator\\bin\\Debug\\FUTURE_WH2001.csv", "C:\\_WA\\WindowsHttpNacuvac\\SpreadCalculator\\bin\\Debug\\FUTURE_WK2001.csv").ToString());
            // jadro.parsujKontraktXml();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Graf graf = new Graf(jadro.listSpread, "FUTURE_WH2010");
            graf.Show(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxKontrakt1.Enabled = false;
            comboBoxKontrakt2.Enabled = false;
            comboBoxKomodity.DataSource = jadro.LoadKontrakty();
        }

        private void comboBoxKomodity_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxKomodity.SelectedItem != "----------")
            {
                var listKontraktov1 = jadro.LoadRokySpecificke(comboBoxKomodity.SelectedItem.ToString());
                var listKontraktov2 = new List<string>(listKontraktov1);
                if (listKontraktov1 != null)
                {
                    comboBoxKontrakt1.DataSource = listKontraktov1;
                    comboBoxKontrakt2.DataSource = listKontraktov2;
                    comboBoxKontrakt1.Enabled = true;
                    comboBoxKontrakt2.Enabled = true;
                }
            }
        }

        private void comboBoxKontrakt1_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxKontrakt1.SelectedItem != "----------")
            {
                var listMesiacov = jadro.LoadMesiaceSpecificke(comboBoxKomodity.SelectedItem.ToString(), comboBoxKontrakt1.SelectedItem.ToString());
                if (listMesiacov != null)
                {
                    comboBoxMesiace1.DataSource = listMesiacov;
                    comboBoxKontrakt1.Enabled = true;
                }
            }
        }

        private void comboBoxKontrakt2_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxKontrakt2.SelectedItem != "----------")
            {
                var listMesiacov = jadro.LoadMesiaceSpecificke(comboBoxKomodity.SelectedItem.ToString(), comboBoxKontrakt2.SelectedItem.ToString());
                if (listMesiacov != null)
                {
                    comboBoxMesiace2.DataSource = listMesiacov;
                    comboBoxKontrakt1.Enabled = true;
                }
            }
        }
    }
}
