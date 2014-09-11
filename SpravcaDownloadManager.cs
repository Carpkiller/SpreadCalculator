using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadCalculator
{
    public class SpravcaDownloadManager
    {
        private Jadro _jadro;

        public SpravcaDownloadManager(Jadro jadro)
        {
            _jadro = jadro;
        }
        public void StiahniData(string komodita, int pocetRokov, string startRok, string mesiac1, string mesiac2, bool vsetko)
        {
            //komodita = komodita.Contains("  -") ? komodita.Substring(komodita.IndexOf("  -")+4) : komodita;
            List<string> kontraktyNaStiahnutie;
            if (vsetko)
            {
                kontraktyNaStiahnutie = SkontrolujSuboryNaStiahnutieVsetky(komodita, pocetRokov, startRok);
            }
            else
            {
                mesiac1 = mesiac1.Substring(0, 1);
                mesiac2 = mesiac2.Substring(0, 1);
                kontraktyNaStiahnutie = SkontrolujSuboryNaStiahnutieSpecificke(komodita, pocetRokov, startRok, mesiac1, mesiac2);
            }

            bool succes;
            foreach (var kontraktnyMesiac in kontraktyNaStiahnutie)
            {
                _jadro.ParsujKontraktXml(out succes, kontraktnyMesiac);
            }
        }

        private List<string> SkontrolujSuboryNaStiahnutieSpecificke(string komodita, int pocetRokov, string startRok, string mesiac1, string mesiac2)
        {
            int neexistuje = 0;
            int pocet = 0;
            var list = new List<string>();

            var kontraktneMesiace = new List<string>{mesiac1, mesiac2};
            komodita = komodita.Contains("  -") ? komodita.Substring(komodita.IndexOf("  -") + 4) : komodita;
            foreach (var mesiac in kontraktneMesiace)
            {
                for (int i = 0; i < pocetRokov; i++)
                {
                    pocet++;
                    var aktMesiac = mesiac.Substring(0, 1);
                    var rok = (int.Parse(startRok) - i).ToString();
                    if (!PracaSoSubormi.SkontrolujSubor(komodita + aktMesiac + rok))
                    {
                        list.Add(komodita + aktMesiac + rok);
                        neexistuje++;
                    }
                }
            }

            return list;
        }

        private List<string> SkontrolujSuboryNaStiahnutieVsetky(string komodita, int pocetRokov, string startRok)
        {
            int neexistuje = 0;
            int pocet = 0;
            var list = new List<string>();

            var kontraktneMesiace = _jadro.LoadMesiaceSpecificke(komodita, startRok);
            komodita = komodita.Contains("  -") ? komodita.Substring(komodita.IndexOf("  -") + 4) : komodita;
            foreach (var mesiac in kontraktneMesiace)
            {
                for (int i = 0; i < pocetRokov; i++)
                {
                    pocet++;
                    var aktMesiac = mesiac.Substring(0, 1);
                    var rok = (int.Parse(startRok)-i).ToString();
                    if (!PracaSoSubormi.SkontrolujSubor(komodita + aktMesiac + rok))
                    {
                        list.Add(komodita + aktMesiac + rok);
                        neexistuje ++;
                    }
                }
            }
            
            return list;
        }
    }
}
