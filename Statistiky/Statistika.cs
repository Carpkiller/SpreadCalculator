using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadCalculator.Statistiky
{
    public class Statistika
    {
        private List<PomocneTriedy.Spread> listSpread;
        private List<DateTime> DatumMaxHodnota;
        private List<DateTime> DatumMinHodnota;
        private List<double> MaxHodnota;
        private List<double> MinHodnota;
        private List<int> ListRokov;
        private double KoncovaHodnota;

        public Statistika()
        {
        }

        public Statistika(List<PomocneTriedy.Spread> listSpread)
        {
            // TODO: Complete member initialization
            this.listSpread = listSpread;
            PocitajStatistiku();
        }

        public override string ToString()
        {
            var ret = "";
            for (int i = 0; i < ListRokov.Count; i++)
            {
                ret += " Rok : " + ListRokov[i] + Environment.NewLine;
                ret += " Max : " + MaxHodnota[i] + "   " + DatumMaxHodnota[i] + Environment.NewLine;
                ret += " Min : " + MinHodnota[i] + "   " + DatumMinHodnota[i] + Environment.NewLine;                
                ret += "-------" + Environment.NewLine;
            }

            ret += " Posl. hodnota : " + KoncovaHodnota + Environment.NewLine;

            return ret;
        }

        private void PocitajStatistiku()
        {
            ListRokov = NajdiRoky();
            PocitajHranicneHodnoty();
        }

        private void PocitajHranicneHodnoty()
        {
            var index = 0;
            InicializujPolia();

            foreach (var item in listSpread)
            {
                if (item.Date.Year == ListRokov[index])
                {
                    if (item.Value > MaxHodnota[index])
                    {
                        MaxHodnota[index] = item.Value;
                        DatumMaxHodnota[index] = item.Date;
                    }
                    if (item.Value < MinHodnota[index])
                    {
                        MinHodnota[index] = item.Value;
                        DatumMinHodnota[index] = item.Date;
                    }
                }
                else
                {
                    index++;
                }
            }
        }

        private void InicializujPolia()
        {
            MaxHodnota = new List<double>();
            MinHodnota = new List<double>();
            DatumMaxHodnota = new List<DateTime>();
            DatumMinHodnota = new List<DateTime>();

            for (int i = 0; i < ListRokov.Count; i++)
            {
                MaxHodnota.Add(-9999);
                MinHodnota.Add(9999);
                DatumMaxHodnota.Add(new DateTime());
                DatumMinHodnota.Add(new DateTime());
            }
        }

        private List<int> NajdiRoky()
        {
            List<int> list = new List<int>();
            DateTime predchadzDatum = listSpread.First().Date;
            KoncovaHodnota = listSpread.First().Value;

            list.Add(predchadzDatum.Year);

            foreach (var item in listSpread)
            {
                if (item.Date.Year != predchadzDatum.Year)
                {
                    Console.WriteLine("Datum  " + item.Date);
                    list.Add(item.Date.Year);
                }
                predchadzDatum = item.Date;
            }

            return list;
        }
    }
}
