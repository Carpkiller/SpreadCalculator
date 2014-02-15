using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using CsvHelper;
using CsvHelper.Configuration;

namespace SpreadCalculator
{
    public partial class Form1 : Form
    {
        private Jadro jadro;

        public Form1()
        {
            InitializeComponent();
            jadro = new Jadro();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // MessageBox.Show(jadro.parsujKontrakty("C:\\_WA\\WindowsHttpNacuvac\\SpreadCalculator\\bin\\Debug\\FUTURE_WH2001.csv", "C:\\_WA\\WindowsHttpNacuvac\\SpreadCalculator\\bin\\Debug\\FUTURE_WK2001.csv").ToString());
            jadro.parsujKontraktXml();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Graf graf = new Graf(jadro.listSpread, "FUTURE_WH2010");
            graf.Show(this);
        }
    }
}
