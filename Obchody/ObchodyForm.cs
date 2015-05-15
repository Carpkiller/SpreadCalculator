using System;
using System.Windows.Forms;
using SpreadCalculator.GrafickeKomponenty;

namespace SpreadCalculator.Obchody
{
    public partial class ObchodyForm : Form
    {
        private Jadro _jadro;
        private Form1 _form;

        public ObchodyForm(Jadro jadro, Form1 form)
        {
            InitializeComponent();
            _jadro = jadro;
            _form = form;

            dataGridView1.DataSource = _jadro.NacitajObchody();
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void upravitObchodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var index = dataGridView1.CurrentCell.RowIndex;
            var obchod = _jadro.UskutocneneObchody.GetZaznam(index);

            var pridajObchodOkno = new PridajObchod(obchod, index);
            pridajObchodOkno.ShowDialog();
            if (pridajObchodOkno.DialogResult == DialogResult.OK)
            {
                if (pridajObchodOkno.Uprava == -1)
                {
                    _jadro.PridajObchod(pridajObchodOkno.Spread, pridajObchodOkno.DatumVstupu,
                        pridajObchodOkno.VstupnaCena, pridajObchodOkno.DatumVystupu, pridajObchodOkno.VystupnaCena, pridajObchodOkno.Ukonceny, obchod.Spread);
                }
                else
                {
                    _jadro.UpravObchod(pridajObchodOkno.Uprava, pridajObchodOkno.Spread, pridajObchodOkno.DatumVstupu,
                        pridajObchodOkno.VstupnaCena, pridajObchodOkno.DatumVystupu, pridajObchodOkno.VystupnaCena, pridajObchodOkno.Ukonceny, obchod.Spread);
                }
            }
        }

        private void zmazatObchodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var index = dataGridView1.CurrentCell.RowIndex;
            _jadro.OdoberObchod(index);
        }

        private void zobrazitObchodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var index = dataGridView1.CurrentCell.RowIndex;
            var obchod = _jadro.UskutocneneObchody.GetZaznam(index);
            _form.VykresliObchod(obchod);
            Close();
        }
    }
}
