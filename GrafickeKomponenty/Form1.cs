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

            zg1.Visible = false;
            jadro = new Jadro();

            jadro.ZmenaPopisu += new Jadro.ZmenaPopisuHandler(ZmenPopis);
        }

        private void ZmenPopis()
        {
            toolStripStatusLabel1.Text = jadro.stavText;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBoxKontrakt1.Enabled && comboBoxKontrakt2.Enabled)
            {
                var komodita1 = comboBoxKomodity.SelectedIndex;
                var komodita2 = checkBoxDruhyKontrakt.Checked ? comboBoxKomodity2.SelectedIndex : comboBoxKomodity.SelectedIndex;
                if (jadro.ParsujKontrakty(komodita1, komodita2, comboBoxMesiace1.Text, comboBoxKontrakt1.Text, comboBoxMesiace2.Text, comboBoxKontrakt2.Text))
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
            var graf = new Graf(jadro.listSpread, "FUTURE_WH2010");
            graf.Show(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxKontrakt1.Enabled = false;
            comboBoxKontrakt2.Enabled = false;
            comboBoxKomodity.DataSource = jadro.LoadKontrakty();
            comboBoxKomodity2.DataSource = jadro.LoadKontrakty();
        }

        private void comboBoxKomodity_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxKomodity.SelectedItem != "-----------")
            {
                var listKontraktov1 = jadro.LoadRokySpecificke(comboBoxKomodity.SelectedItem.ToString());
                if (listKontraktov1 != null)
                {
                    comboBoxKontrakt1.DataSource = listKontraktov1;
                    comboBoxKontrakt1Sez.DataSource = listKontraktov1;
                    comboBoxKontrakt1Graf.DataSource = listKontraktov1;
                    comboBoxKontrakt1.Enabled = true;
                    if (!checkBoxDruhyKontrakt.Checked)
                    {
                        var listKontraktov2 = new List<string>(listKontraktov1);
                        comboBoxKontrakt2.DataSource = listKontraktov2;
                        comboBoxKontrakt2Sez.DataSource = listKontraktov2;
                        comboBoxKontrakt2.Enabled = true;
                    }
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
                var listMesiacov = jadro.LoadMesiaceSpecificke(checkBoxDruhyKontrakt.Checked ? comboBoxKomodity2.SelectedItem.ToString() : comboBoxKomodity.SelectedItem.ToString(), comboBoxKontrakt2.SelectedItem.ToString());
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
                comboBoxKontrakt1.SelectedIndex = comboBoxKontrakt1.SelectedIndex - 1;
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
                    zg1.Visible = true;
                    zg1 = PracasGrafmi.KresliGraf("Forecast graf", jadro.dataGrafTerajsi, jadro.dataGrafVedalsi, zg1);
                    zg1.Refresh();
                    zg1.IsShowPointValues = true;
                    zg1.RestoreScale(zg1.GraphPane);
                    //textBox1.Text = jadro.statistika.ToString();
                }
            }
        }

        private void checkBoxDruhyKontrakt_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDruhyKontrakt.Checked)
            {
                labelKomodity2.Visible = true;
                comboBoxKomodity2.Visible = true;
                var ee = comboBoxKontrakt1.DataSource;
            }
            else
            {
                labelKomodity2.Visible = false;
                comboBoxKomodity2.Visible = false;
            }

        }

        private void comboBoxKomodity2_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxKomodity2.SelectedItem != "-----------")
            {
                var listKontraktov1 = jadro.LoadRokySpecificke(comboBoxKomodity2.SelectedItem.ToString());
                if (listKontraktov1 != null)
                {
                    comboBoxKontrakt2.DataSource = listKontraktov1;
                    comboBoxKontrakt2Sez.DataSource = listKontraktov1;
                    comboBoxKontrakt2.Enabled = true;
                }
            }
        }

        private void comboBoxKontrakt1Graf_TextChanged(object sender, EventArgs e)
        {
            var predZnak = comboBoxMesiace1Graf.SelectedIndex;
            if (!ReferenceEquals(comboBoxKontrakt1Graf.SelectedItem, "-----------"))
            {
                var listMesiacov = jadro.LoadMesiaceSpecificke(comboBoxKomodity.SelectedItem.ToString(), comboBoxKontrakt1Graf.SelectedItem.ToString());
                if (listMesiacov != null)
                {
                    comboBoxMesiace1Graf.DataSource = listMesiacov;
                    comboBoxKontrakt1Graf.Enabled = true;
                    if (listMesiacov.Count >= predZnak)
                    {
                        comboBoxMesiace1Graf.SelectedIndex = predZnak;
                    }
                }
            }
        }

        private void buttonJednoduchyGraf_Click(object sender, EventArgs e)
        {
            if (comboBoxMesiace1Graf.Text != null && comboBoxKontrakt1Graf.Text != null)
            {
                //    MessageBox.Show("Done");
                zg1.Visible = true;
                zg1 = PracasGrafmi.KresliJednoduchyGraf("Forecast graf",
                    jadro.GetDataPreGraf(comboBoxKomodity.SelectedIndex, comboBoxMesiace1Graf.Text,
                        comboBoxKontrakt1Graf.Text), zg1);
                zg1.Refresh();
                zg1.IsShowPointValues = true;
                zg1.RestoreScale(zg1.GraphPane);
                //textBox1.Text = jadro.statistika.ToString();
            }
        }
    }
}
