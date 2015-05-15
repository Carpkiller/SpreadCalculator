using System;
using SpreadCalculator.PomocneTriedy;

namespace SpreadCalculator.Obchody
{
    [Serializable]
    public class Obchod
    {
        public DateTime ZaciatokObchodu { get; set; }
        public DateTime KoniecObchodu { get; set; }
        public double VstupnaCena { get; set; }
        public double? VystupnaCena { get; set; }
        public string ZapisSpread { get; set; }
        public bool Ukonceny { get; set; }
        [System.ComponentModel.Browsable(false)] 
        public SledovanySpread Spread;

        public Obchod(DateTime zaciatokObchodu, DateTime koniecObchodu, double vstupnaCena, double? vystupnaCena, string zapis, bool ukonceny, SledovanySpread spread)
        {
            ZaciatokObchodu = zaciatokObchodu;
            KoniecObchodu = koniecObchodu;
            VstupnaCena = vstupnaCena;
            VystupnaCena = vystupnaCena;
            ZapisSpread = zapis;
            Ukonceny = ukonceny;
            Spread = spread;
        }
    }
}
