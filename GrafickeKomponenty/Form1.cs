using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using ZedGraph;

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
                if (jadro.parsujKontrakty(comboBoxKomodity.SelectedIndex, comboBoxMesiace1.Text, comboBoxKontrakt1.Text, comboBoxMesiace2.Text, comboBoxKontrakt2.Text))
                {
                //    MessageBox.Show("Done");
                    zg1.Visible = true;
                    zg1 = PracasGrafmi.KresliGraf(NazovGrafu(), jadro.listSpread, zg1);
                    zg1.Refresh();
                    zg1.IsShowPointValues = true;
                    zg1.RestoreScale(zg1.GraphPane);
                 //   zg1.Scale(0);
                }
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
            var predZnak = comboBoxMesiace1.SelectedIndex;
            if (comboBoxKontrakt1.SelectedItem != "----------")
            {
                var listMesiacov = jadro.LoadMesiaceSpecificke(comboBoxKomodity.SelectedItem.ToString(), comboBoxKontrakt1.SelectedItem.ToString());
                if (listMesiacov != null)
                {
                    comboBoxMesiace1.DataSource = listMesiacov;
                    comboBoxKontrakt1.Enabled = true;
                    if (listMesiacov.Count >= predZnak)
                    {
                        comboBoxMesiace1.SelectedIndex = predZnak;
                    }
                }
            }
        }

        private void comboBoxKontrakt2_TextChanged(object sender, EventArgs e)
        {
            var predZnak = comboBoxMesiace2.SelectedIndex;
            if (comboBoxKontrakt2.SelectedItem != "----------")
            {
                var listMesiacov = jadro.LoadMesiaceSpecificke(comboBoxKomodity.SelectedItem.ToString(), comboBoxKontrakt2.SelectedItem.ToString());
                if (listMesiacov != null)
                {
                    comboBoxMesiace2.DataSource = listMesiacov;
                    comboBoxKontrakt1.Enabled = true;
                    if (listMesiacov.Count >= predZnak)
                    {
                        comboBoxMesiace2.SelectedIndex = predZnak;
                    }
                }
            }
        }

        private string NazovGrafu()
        {
            var komodita = comboBoxKomodity.SelectedValue.ToString();
            var kontr1 = comboBoxKontrakt1.SelectedValue.ToString() + comboBoxMesiace1.SelectedValue.ToString();
            var kontr2 = comboBoxKontrakt2.SelectedValue.ToString() + comboBoxMesiace2.SelectedValue.ToString();
            return komodita + "  -  " + kontr1 + " " + kontr2;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBoxKontrakt1.SelectedIndex != 0 && comboBoxKontrakt2.SelectedIndex != 0)
            {
                comboBoxKontrakt1.SelectedIndex = comboBoxKontrakt1.SelectedIndex-1;
                comboBoxKontrakt2.SelectedIndex = comboBoxKontrakt2.SelectedIndex - 1;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBoxKontrakt1.SelectedIndex != comboBoxKontrakt1.Items.Count - 1 && comboBoxKontrakt2.SelectedIndex != comboBoxKontrakt2.Items.Count - 1)
            {
                comboBoxKontrakt1.SelectedIndex = comboBoxKontrakt1.SelectedIndex + 1;
                comboBoxKontrakt2.SelectedIndex = comboBoxKontrakt2.SelectedIndex + 1;
            }
        }
    }
}
