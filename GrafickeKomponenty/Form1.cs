using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace SpreadCalculator.GrafickeKomponenty
{
    public partial class Form1 : Form
    {
        private readonly Jadro _jadro;
        private readonly PracasGrafmiVS _pracaSGrafmi;

        private int _poc;
        private double _zacSur;

        public Form1()
        {
            InitializeComponent();
            comboBoxKontrakt1.SelectedIndex = 0;
            comboBoxKontrakt2.SelectedIndex = 0;

            //zg1.Visible = false;
            _jadro = new Jadro();
            _pracaSGrafmi = new PracasGrafmiVS(chart1);
            _jadro.ZmenaPopisu += ZmenPopis;
            labelHodnotaBodu.Text = _jadro.HodnotaBodu;
            comboBoxRokyKorelacie.SelectedIndex = 0;
            listBox1.DataSource = _jadro.SledovaneSpready.PopisSpreadov();
            contextMenuStrip1.Items.Add("Zmazat");
            textBoxPoznamky.Text = _jadro.NacitajPoznamky();
        }

        private void ZmenPopis()
        {
            toolStripStatusLabel1.Text = _jadro.StavText;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBoxKontrakt1.Enabled && comboBoxKontrakt2.Enabled)
            {
                var komodita1 = comboBoxKomodity.SelectedIndex;
                var komodita2 = checkBoxDruhyKontrakt.Checked
                    ? comboBoxKomodity2.SelectedIndex
                    : comboBoxKomodity.SelectedIndex;
                var dlzka = checkBoxVyber.Checked ? 0 : int.Parse(comboBoxMesiace.SelectedValue.ToString());
                if (_jadro.ParsujKontrakty(komodita1, komodita2, comboBoxMesiace1.Text, comboBoxKontrakt1.Text,
                    comboBoxMesiace2.Text, comboBoxKontrakt2.Text, dlzka))
                {
                    textBoxVelky.Visible = false;
                    chart1.Visible = true;
                    _pracaSGrafmi.VykresliSpread(NazovGrafu(), _jadro.ListSpread);
                    labelHodnotaBodu.Text = _jadro.HodnotaBodu + @" $";
                    comboBoxMesiace.DataSource = _jadro.GetMesiace();

                    DialogResult rs = MessageBox.Show(@"Chces pridat spread k sledovanym spreadom?", @"Otazka",
                        MessageBoxButtons.OKCancel);
                    if (rs == DialogResult.OK)
                    {
                        _jadro.PridajSledovanySpread(komodita1, komodita2, comboBoxMesiace1.Text, comboBoxKontrakt1.Text,
                            comboBoxMesiace2.Text, comboBoxKontrakt2.Text);
                        listBox1.DataSource = null;
                        listBox1.DataSource = _jadro.SledovaneSpready.PopisSpreadov();
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var komodita1 = comboBoxKomodity.SelectedIndex;
            var komodita2 = checkBoxDruhyKontrakt.Checked
                ? comboBoxKomodity2.SelectedIndex
                : comboBoxKomodity.SelectedIndex;

            textBoxVelky.Visible = false;
            chart1.Visible = true;
            var dlzka = int.Parse(comboBoxRokyKorelacie.SelectedItem.ToString()) + 1;
            _pracaSGrafmi.VykresliKorelacnySpread(NazovGrafu(),
                _jadro.PocitajGrafKorelacie(komodita1, komodita2, comboBoxMesiace1.Text, comboBoxKontrakt1.Text,
                    comboBoxMesiace2.Text, comboBoxKontrakt2.Text, dlzka));
            labelHodnotaBodu.Text = _jadro.HodnotaBodu + @" $";
            comboBoxMesiace.DataSource = _jadro.GetMesiace();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxKontrakt1.Enabled = false;
            comboBoxKontrakt2.Enabled = false;
            comboBoxKomodity.DataSource = _jadro.LoadKontrakty();
            comboBoxKomodity2.DataSource = _jadro.LoadKontrakty();
        }

        private void comboBoxKomodity_TextChanged(object sender, EventArgs e)
        {
            if (!ReferenceEquals(comboBoxKomodity.SelectedItem, "-----------"))
            {
                var listKontraktov1 = _jadro.LoadRokySpecificke(comboBoxKomodity.SelectedItem.ToString());
                if (listKontraktov1 != null)
                {
                    comboBoxKontrakt1.DataSource = listKontraktov1;
                    comboBoxKontrakt1Sez.DataSource = listKontraktov1;
                    comboBoxKontrakt1Graf.DataSource = listKontraktov1;
                    comboBox1TestyRok.DataSource = listKontraktov1;
                    comboBoxKontrakt1.Enabled = true;
                    if (!checkBoxDruhyKontrakt.Checked)
                    {
                        var listKontraktov2 = new List<string>(listKontraktov1);
                        comboBoxKontrakt2.DataSource = listKontraktov2;
                        comboBoxKontrakt2Sez.DataSource = listKontraktov2;
                        comboBox2TestyRok.DataSource = listKontraktov2;
                        comboBoxKontrakt2.Enabled = true;
                    }
                }
            }
        }

        private void comboBoxKontrakt1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var predZnak = comboBoxMesiace1.SelectedIndex;
                if (!ReferenceEquals(comboBoxKontrakt1.SelectedItem, "----------"))
                {
                    var listMesiacov = _jadro.LoadMesiaceSpecificke(comboBoxKomodity.SelectedItem.ToString(),
                        comboBoxKontrakt1.SelectedItem.ToString());
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
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, @"Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void comboBoxKontrakt2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var predZnak = comboBoxMesiace2.SelectedIndex;
                if (!ReferenceEquals(comboBoxKontrakt2.SelectedItem, "----------"))
                {
                    var listMesiacov =
                        _jadro.LoadMesiaceSpecificke(
                            checkBoxDruhyKontrakt.Checked
                                ? comboBoxKomodity2.SelectedItem.ToString()
                                : comboBoxKomodity.SelectedItem.ToString(), comboBoxKontrakt2.SelectedItem.ToString());
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
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message, @"Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private string NazovGrafu()
        {
            var komodita = comboBoxKomodity.SelectedValue.ToString();
            var komodita2 = comboBoxKomodity2.Visible
                ? comboBoxKomodity2.SelectedValue.ToString()
                : komodita;
            var kontr1 = comboBoxKontrakt1.SelectedValue + comboBoxMesiace1.SelectedValue.ToString();
            var kontr2 = comboBoxKontrakt2.SelectedValue + comboBoxMesiace2.SelectedValue.ToString();
            //return komodita + "  -  " + kontr1 + " " + kontr2;
            return komodita.Substring(komodita.LastIndexOf('-') + 2, 1) + kontr1.Substring(4, 1) +
                   kontr1.Substring(2, 2) + " - " +
                   komodita2.Substring(komodita2.LastIndexOf('-') + 2, 1) + kontr2.Substring(4, 1) +
                   kontr1.Substring(2, 2);
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
            if (comboBoxKontrakt1.SelectedIndex != comboBoxKontrakt1.Items.Count - 1 &&
                comboBoxKontrakt2.SelectedIndex != comboBoxKontrakt2.Items.Count - 1)
            {
                comboBoxKontrakt1.SelectedIndex = comboBoxKontrakt1.SelectedIndex + 1;
                comboBoxKontrakt2.SelectedIndex = comboBoxKontrakt2.SelectedIndex + 1;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text == @"Sezonnost")
            {
                var listKontraktov1 = _jadro.LoadRokySpecificke(comboBoxKomodity.SelectedItem.ToString());
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
            try
            {
                var predZnak = comboBoxMesiace1Sez.SelectedIndex;
                if (!ReferenceEquals(comboBoxKontrakt1Sez.SelectedItem, "----------"))
                {
                    var listMesiacov = _jadro.LoadMesiaceSpecificke(comboBoxKomodity.SelectedItem.ToString(),
                        comboBoxKontrakt1Sez.SelectedItem.ToString());
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
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, @"Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void comboBoxKontrakt2Sez_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var predZnak = comboBoxMesiace2Sez.SelectedIndex;
                if (!ReferenceEquals(comboBoxKontrakt2Sez.SelectedItem, "----------"))
                {
                    var listMesiacov = _jadro.LoadMesiaceSpecificke(comboBoxKomodity.SelectedItem.ToString(),
                        comboBoxKontrakt2Sez.SelectedItem.ToString());
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
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, @"Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBoxKontrakt1Sez.Enabled && comboBoxKontrakt2Sez.Enabled)
            {
                if (_jadro.PocitajSezonnost(comboBoxKomodity.SelectedIndex, comboBoxKomodity2.SelectedIndex, comboBoxMesiace1Sez.Text,
                    comboBoxKontrakt1Sez.Text, comboBoxMesiace2Sez.Text, comboBoxKontrakt2Sez.Text, textBoxRoky.Text))
                {
                    if (checkBoxJednotliveRoky.Checked)
                    {
                        //    MessageBox.Show("Done");
                        textBoxVelky.Visible = false;
                        //zg1.Visible = true;
                        //zg1 = PracasGrafmi.KresliGraf("Forecast graf", _jadro.dataGrafTerajsi, _jadro.dataGrafVsetky,zg1);
                        //zg1.Refresh();
                        //zg1.IsShowPointValues = true;
                        //zg1.RestoreScale(zg1.GraphPane);
                        //textBox1.Text = jadro.statistika.ToString();
                    }
                    else
                    {
                        textBoxVelky.Visible = false;
                        chart1.Visible = true;
                        _pracaSGrafmi.VykresliSpread(NazovGrafu(), _jadro.dataGrafTerajsi, _jadro.dataGrafVedalsi);
                        labelHodnotaBodu.Text = _jadro.HodnotaBodu + @" $";
                        //comboBoxMesiace.DataSource = _jadro.GetMesiace();
                    }
                }
            }
        }

        private void checkBoxDruhyKontrakt_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDruhyKontrakt.Checked)
            {
                labelKomodity2.Visible = true;
                comboBoxKomodity2.Visible = true;
            }
            else
            {
                labelKomodity2.Visible = false;
                comboBoxKomodity2.Visible = false;
            }

        }

        private void comboBoxKomodity2_TextChanged(object sender, EventArgs e)
        {
            if (!ReferenceEquals(comboBoxKomodity2.SelectedItem, "-----------"))
            {
                var listKontraktov1 = _jadro.LoadRokySpecificke(comboBoxKomodity2.SelectedItem.ToString());
                if (listKontraktov1 != null)
                {
                    comboBoxKontrakt2.DataSource = listKontraktov1;
                    comboBoxKontrakt2Sez.DataSource = listKontraktov1;
                    comboBox2TestyRok.DataSource = listKontraktov1;
                    comboBoxKontrakt2.Enabled = true;
                }
            }
        }

        private void comboBoxKontrakt1Graf_TextChanged(object sender, EventArgs e)
        {
            var predZnak = comboBoxMesiace1Graf.SelectedIndex;
            if (!ReferenceEquals(comboBoxKontrakt1Graf.SelectedItem, "-----------"))
            {
                var listMesiacov = _jadro.LoadMesiaceSpecificke(comboBoxKomodity.SelectedItem.ToString(),
                    comboBoxKontrakt1Graf.SelectedItem.ToString());
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
                //zg1.Visible = true;
                //zg1 = PracasGrafmi.KresliJednoduchyGraf("Forecast graf",_jadro.GetDataPreGraf(comboBoxKomodity.SelectedIndex, comboBoxMesiace1Graf.Text,comboBoxKontrakt1Graf.Text), zg1);
                //zg1.Refresh();
                //zg1.IsShowPointValues = true;
                //zg1.RestoreScale(zg1.GraphPane);
                //textBox1.Text = jadro.statistika.ToString();
                var dlzka = checkBoxVyber.Checked ? 0 : int.Parse(comboBoxMesiace.SelectedValue.ToString());
                _pracaSGrafmi.VykreliGraf("Graf komodity",
                    _jadro.GetDataPreGraf(comboBoxKomodity.SelectedIndex, comboBoxMesiace1Graf.Text,
                        comboBoxKontrakt1Graf.Text, dlzka));
            }
        }

        private void buttonTesty_Click(object sender, EventArgs e)
        {
            var komodita1 = comboBoxKomodity.SelectedIndex;
            var komodita2 = checkBoxDruhyKontrakt.Checked
                ? comboBoxKomodity2.SelectedIndex
                : comboBoxKomodity.SelectedIndex;
            chart1.Visible = false;
            textBoxVelky.Visible = true;
            textBoxVelky.Text = _jadro.PocitajStatistiky(komodita1, komodita2, comboBox1TestyMesiac.Text,
                comboBox1TestyRok.Text, comboBox2TestyMesiac.Text, comboBox2TestyRok.Text, int.Parse(textBox2.Text));
        }

        private void comboBox1TestyRok_TextChanged(object sender, EventArgs e)
        {
            var predZnak = comboBox1TestyMesiac.SelectedIndex;
            if (!ReferenceEquals(comboBox1TestyRok.SelectedItem, "----------"))
            {
                var listMesiacov = _jadro.LoadMesiaceSpecificke(comboBoxKomodity.SelectedItem.ToString(),
                    comboBox1TestyRok.SelectedItem.ToString());
                if (listMesiacov != null)
                {
                    comboBox1TestyMesiac.DataSource = listMesiacov;
                    comboBox1TestyRok.Enabled = true;
                    if (listMesiacov.Count >= predZnak)
                    {
                        comboBox1TestyMesiac.SelectedIndex = predZnak;
                    }
                }
            }
        }

        private void comboBox2TestyRok_TextChanged(object sender, EventArgs e)
        {
            var predZnak = comboBox2TestyMesiac.SelectedIndex;
            if (!ReferenceEquals(comboBox2TestyRok.SelectedItem, "----------"))
            {
                var listMesiacov = _jadro.LoadMesiaceSpecificke(comboBoxKomodity.SelectedItem.ToString(),
                    comboBox2TestyRok.SelectedItem.ToString());
                if (listMesiacov != null)
                {
                    comboBox2TestyMesiac.DataSource = listMesiacov;
                    comboBox2TestyRok.Enabled = true;
                    if (listMesiacov.Count >= predZnak)
                    {
                        comboBox2TestyMesiac.SelectedIndex = predZnak;
                    }
                }
            }
        }

        private void chart1_MouseDown(object sender, MouseEventArgs e)
        {
            if (tabPage1.Visible && chart1.Series.Count > 1)
            {
                _poc = 1;
                //Console.WriteLine(_positionDown);

                chart1.ChartAreas[0].CursorY.Interval = _jadro.Interval;

                chart1.ChartAreas[0].CursorX.SetCursorPixelPosition(new PointF(e.X, e.Y), true);
                chart1.ChartAreas[0].CursorY.SetCursorPixelPosition(new PointF(e.X, e.Y), true);

                double pX = chart1.ChartAreas[0].CursorX.Position; //X Axis Coordinate of your mouse cursor
                double pY = chart1.ChartAreas[0].CursorY.Position; //Y Axis Coordinate of your mouse cursor
                _zacSur = pY;

                if (chart1.Series[chart1.Series.Count - 1].Points.Count > 0)
                {
                    chart1.Series[chart1.Series.Count - 1].Points.Clear();
                }

                chart1.Series[chart1.Series.Count - 1].Points.AddXY(pX, pY);
                chart1.Series[chart1.Series.Count - 1].Points.First().Label =
                    _zacSur.ToString(CultureInfo.InvariantCulture);
                chart1.Invalidate();
            }
        }

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (tabPage1.Visible && chart1.Series.Count > 1)
                {
                    chart1.ChartAreas[0].CursorY.Interval = _jadro.Interval;

                    chart1.ChartAreas[0].CursorX.SetCursorPixelPosition(new Point(e.X, e.Y), true);
                    chart1.ChartAreas[0].CursorY.SetCursorPixelPosition(new Point(e.X, e.Y), true);

                    if (_poc == 1)
                    {
                        chart1.PointToScreen(MousePosition);

                        chart1.ChartAreas[0].CursorX.SetCursorPixelPosition(new Point(e.X, e.Y), true);
                        chart1.ChartAreas[0].CursorY.SetCursorPixelPosition(new Point(e.X, e.Y), true);

                        double pX = chart1.ChartAreas[0].CursorX.Position; //X Axis Coordinate of your mouse cursor
                        double pY = chart1.ChartAreas[0].CursorY.Position; //Y Axis Coordinate of your mouse cursor

                        if (chart1.Series[chart1.Series.Count - 1].Points.Count == 2)
                        {
                            chart1.Series[chart1.Series.Count - 1].Points.RemoveAt(1);
                        }
                        chart1.Series[chart1.Series.Count - 1].Points.AddXY(pX, pY);
                        chart1.Series[chart1.Series.Count - 1].Points.Last().Label =
                            pY.ToString(CultureInfo.InvariantCulture);
                        //Console.WriteLine("Poc po " + chart1.Series[1].Points.Count);
                        chart1.Invalidate();

                        var komodita1 = comboBoxKomodity.SelectedIndex;
                        var komodita2 = checkBoxDruhyKontrakt.Checked
                            ? comboBoxKomodity2.SelectedIndex
                            : comboBoxKomodity.SelectedIndex;
                        labelPravitko.Text = _jadro.PocitajHodnotuVyberu(komodita1, komodita2, _zacSur, pY);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, @"Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chart1_MouseUp(object sender, MouseEventArgs e)
        {
            //Console.WriteLine("koncova - " + positionUp);
            _poc = 0;
        }

        private void stiahnutDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var downloadManager = new DownloadManager(_jadro);
            downloadManager.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _jadro.Koniec(textBoxPoznamky.Text);
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            var spread = _jadro.SledovaneSpready.GetZaznam(listBox1.SelectedIndex);
            var komodita1 = spread.komodita1;
            var komodita2 = spread.komodita2;

            comboBoxKomodity.SelectedIndex = komodita1;
            if (komodita1 != komodita2)
            {
                comboBoxKomodity2.SelectedIndex = komodita2;
                checkBoxDruhyKontrakt.Checked = true;
            }
            else
                checkBoxDruhyKontrakt.Checked = false;

            comboBoxKontrakt1.Text = spread.rok1;
            comboBoxMesiace1.Text = spread.kontrakt1;
            comboBoxKontrakt2.Text = spread.rok2;
            comboBoxMesiace2.Text = spread.kontrakt2;

            var dlzka = checkBoxVyber.Checked ? 0 : int.Parse(comboBoxMesiace.SelectedValue.ToString());
            if (_jadro.ParsujKontrakty(komodita1, komodita2, spread.kontrakt1, spread.rok1,
                spread.kontrakt2, spread.rok2, dlzka))
            {
                textBoxVelky.Visible = false;
                chart1.Visible = true;
                _pracaSGrafmi.VykresliSpread(_jadro.SledovaneSpready.PopisSpreadov()[listBox1.SelectedIndex],
                    _jadro.ListSpread);
                labelHodnotaBodu.Text = _jadro.HodnotaBodu + @" $";
                comboBoxMesiace.DataSource = _jadro.GetMesiace();
            }
        }

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                listBox1.SelectedIndex = listBox1.IndexFromPoint(e.X, e.Y);
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void contextMenuStrip1_MouseClick(object sender, MouseEventArgs e)
        {
            contextMenuStrip1.Close();
            _jadro.SledovaneSpready.OdstranZaznam(listBox1.SelectedIndex);
            listBox1.DataSource = null;
            listBox1.DataSource = _jadro.SledovaneSpready.PopisSpreadov();
        }

        private void spravaDatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var spravaDat = new SpravaDat(_jadro);
            spravaDat.Show();
        }

        private void specifikacieKontraktovToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var spravaSpecifikacii = new SpravaSpecifikacii(_jadro);
            spravaSpecifikacii.Show();
        }
    }
}