namespace SpreadCalculator.PomocneTriedy
{
    public class SpecifikaciaKontraktu
    {
        public string Symbol { get; set; }
        public string Popis { get; set; }
        public string Burza { get; set; }
        public string Url { get; set; }
        public string UrlConn { get; set; }
        public string StartRok { get; set; }
        public string EndRok { get; set; }
        public string KontraktneMesiace { get; set; }
        public string HodnotaBodu { get; set; }
        public string VelkostTicku { get; set; }
        public string Typ { get; set; }

        public SpecifikaciaKontraktu(){}

        public SpecifikaciaKontraktu(string symbol, string popis, string burza, string url)
        {
            Symbol = symbol;
            Popis = popis;
            Burza = burza;
            Url = url;
        }

        public SpecifikaciaKontraktu(string symbol, string popis, string burza, string url, string urlConn, string startRok, string endRok, string kontraktneMesiace, string hodnotaBodu, string velkostTicku, string typ)
        {
            Symbol = symbol;
            Popis = popis;
            Burza = burza;
            Url = url;
            UrlConn = urlConn;
            StartRok = startRok;
            EndRok = endRok;
            KontraktneMesiace = kontraktneMesiace;
            HodnotaBodu = hodnotaBodu;
            VelkostTicku = velkostTicku;
            Typ = typ;
        }
    }
}
