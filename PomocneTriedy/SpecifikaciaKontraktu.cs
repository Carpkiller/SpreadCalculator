using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadCalculator.PomocneTriedy
{
    public class SpecifikaciaKontraktu
    {
        public string Symbol { get; set; }
        public string Popis { get; set; }
        public string Burza { get; set; }
        public string Url { get; set; }

        public SpecifikaciaKontraktu(){}

        public SpecifikaciaKontraktu(string symbol, string popis, string burza, string url)
        {
            Symbol = symbol;
            Popis = popis;
            Burza = burza;
            Url = url;
        }
    }
}
