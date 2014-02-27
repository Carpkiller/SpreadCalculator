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
                    textBox1.Text = jadro.statistika.ToString();
                }
            }
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

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text == "Sezonnost")
            {
                var listKontraktov1 = jadro.LoadRokySpecificke(comboBoxKomodity.SelectedItem.ToString());
                var listKontraktov2 = new List<string>(listKontraktov1);
                if (listKontraktov1 != null)
                {
                    comboBoxKontrakt1Sez.DataSource = listKontraktov1;
                    comboBoxKontrakt2Sez.DataSource = listKontraktov2;
                    comboBoxKontrakt1Sez.Enabled = true;
                    comboBoxKontrakt2Sez.Enabled = true;
                }
            }
        }

        private void comboBoxKontrakt1Sez_TextChanged(object sender, EventArgs e)
        {
            var predZnak = comboBoxMesiace1Sez.SelectedIndex;
            if (comboBoxKontrakt1Sez.SelectedItem != "----------")
            {
                var listMesiacov = jadro.LoadMesiaceSpecificke(comboBoxKomodity.SelectedItem.ToString(), comboBoxKontrakt1Sez.SelectedItem.ToString());
                if (listMesiacov != null)
                {
                    comboBoxMesiace1Sez.DataSource = listMesiacov;
                    comboBoxKontrakt1Sez.Enabled = true;
                    if (listMesiacov.Count >= predZnak)
                    {
                        comboBoxMesiace1Sez.SelectedIndex = predZnak;
                    }
                }
            }
        }

        private void comboBoxKontrakt2Sez_TextChanged(object sender, EventArgs e)
        {
            var predZnak = comboBoxMesiace2Sez.SelectedIndex;
            if (comboBoxKontrakt2Sez.SelectedItem != "----------")
            {
                var listMesiacov = jadro.LoadMesiaceSpecificke(comboBoxKomodity.SelectedItem.ToString(), comboBoxKontrakt2Sez.SelectedItem.ToString());
                if (listMesiacov != null)
                {
                    comboBoxMesiace2Sez.DataSource = listMesiacov;
                    comboBoxKontrakt2Sez.Enabled = true;
                    if (listMesiacov.Count >= predZnak)
                    {
                        comboBoxMesiace2Sez.SelectedIndex = predZnak;
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBoxKontrakt1Sez.Enabled && comboBoxKontrakt2Sez.Enabled)
            {
                if (jadro.pocitajSezonnost(comboBoxKomodity.SelectedIndex, comboBoxMesiace1Sez.Text, comboBoxKontrakt1Sez.Text, comboBoxMesiace2Sez.Text, comboBoxKontrakt2Sez.Text, textBoxRoky.Text))
                {
                    //    MessageBox.Show("Done");
                    //zg1.Visible = true;
                    //zg1 = PracasGrafmi.KresliGraf(NazovGrafu(), jadro.listSpread, zg1);
                    //zg1.Refresh();
                    //zg1.IsShowPointValues = true;
                    //zg1.RestoreScale(zg1.GraphPane);
                    //textBox1.Text = jadro.statistika.ToString();
                }
            }
        }
    }
}
