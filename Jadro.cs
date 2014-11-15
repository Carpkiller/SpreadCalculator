using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using CsvHelper;
using SpreadCalculator.PomocneTriedy;
using System.Globalization;
using SpreadCalculator.Statistiky;

namespace SpreadCalculator
{
    public class Jadro
    {
        private List<ObchodnyDen> _listKontrakt1;
        private List<ObchodnyDen> _listKontrakt2;
        public List<Spread> ListSpread;
        public List<Spread> PlnyListSpread;
        private List<SpecifikaciaKontraktu> _listSpecifikacii;
        private List<SirsiaSpecifikaciaKontraktu> _listFuturesKontraktov;
        public SledovaneSpreadyData SledovaneSpready;
        public Statistika Statistika;
        public string StavText = "Ready";
        public SpravcaDownloadManager DownloadManager;
        public string HodnotaBodu { get; set; }
        public double Interval { get; set; }

        public delegate void ZmenaPopisuHandler();
        public event ZmenaPopisuHandler ZmenaPopisu;

        public Jadro()
        {
            _listFuturesKontraktov = new List<SirsiaSpecifikaciaKontraktu>();
            SledovaneSpready = new SledovaneSpreadyData();
            PracaSoSubormi.OcekujStareSubory();
        }

        public List<ObchodnyDen> ParsujKontrakt(string cesta, out bool succes)
        {
            succes = false;
            var list = new List<ObchodnyDen>();
            var csv = new CsvReader(new StreamReader(cesta));
            csv.Configuration.HasHeaderRecord = true;
            csv.Configuration.IgnoreHeaderWhiteSpace = true;
            csv.Configuration.Delimiter = ",";
            var myCustomObjects = csv.GetRecords<ObchodnyDen>();

            foreach (var item in myCustomObjects)
                list.Add(item);

            if (list.Count > 0)
                succes = true;

            return list;
        }

        //internal bool ParsujKontrakty(string p1, string p2)
        //{
        //    _listKontrakt1 = new List<ObchodnyDen>();
        //    _listKontrakt2 = new List<ObchodnyDen>();

        //    bool succes1;
        //    bool succes2;

        //    _listKontrakt1 = ParsujKontrakt(p1,out succes1);
        //    _listKontrakt2 = ParsujKontrakt(p2, out succes2);

        //    ListSpread = vypocitajSpread(_listKontrakt1, _listKontrakt2);

        //    if (ListSpread.Count > 0)
        //    {
        //        Statistika = new Statistika(ListSpread);
        //    }

        //    return succes1 && succes2;
        //}


        internal bool ParsujKontrakty(int komodita1, int komodita2, string kontraktnyMesiac1, string rok1, string kontraktnyMesiac2, string rok2, int dlzka)
        {        
            bool succes1;
            bool succes2;
            double hodnotaBodu1;
            double hodnotaBodu2;

            kontraktnyMesiac1 = kontraktnyMesiac1.Contains("  -") ? kontraktnyMesiac1.Substring(0, kontraktnyMesiac1.IndexOf("  -")) : kontraktnyMesiac1;
            kontraktnyMesiac2 = kontraktnyMesiac2.Contains("  -") ? kontraktnyMesiac2.Substring(0, kontraktnyMesiac2.IndexOf("  -")) : kontraktnyMesiac2;

            var listKontrakt1 = NacitajData(komodita1, kontraktnyMesiac1, rok1, out succes1, out hodnotaBodu1);
            var listKontrakt2 = NacitajData(komodita2, kontraktnyMesiac2, rok2, out succes2, out hodnotaBodu2);

            ListSpread = vypocitajSpread(listKontrakt1, listKontrakt2, hodnotaBodu1, hodnotaBodu2);
            PlnyListSpread = ListSpread;

            if (dlzka != 0)
            {
                for (int i = 0; i < ListSpread.Count; i++)
                {
                    if (ListSpread[i].Date < ListSpread.First().Date.AddMonths(-dlzka))
                    {
                        ListSpread = ListSpread.Take(i).ToList();
                        break;
                    }
                }
            }

            if (ListSpread!= null)
            {
                if (ListSpread.Count > 0)
                {
                    StavText = "Nacitanie uspesne dokoncene";
                    Statistika = new Statistika(ListSpread);
                }
                else
                {
                    StavText = "Spread mimo rozsah/ chybajuce data";
                }
            }

            if (ZmenaPopisu != null)
            {
                //vyvolani udalosti
             //   stavText = "Nahravanie dokoncene";
                ZmenaPopisu();
            }

            return succes1 && succes2 && ListSpread.Count>0;
        }

        private List<ObchodnyDen> NacitajData(int p1, string kontraktnyMesiac1, string rok1, out bool succes, out double hodnotaBodu)
        {
            succes = false;
            var listdata = new List<ObchodnyDen>();
            kontraktnyMesiac1 = kontraktnyMesiac1.Contains("  -")
                ? kontraktnyMesiac1.Substring(0, kontraktnyMesiac1.IndexOf("  -"))
                : kontraktnyMesiac1;

            if (ExistujeStiahnutySubor(_listFuturesKontraktov[p1 - 1].Symbol + kontraktnyMesiac1 + rok1))
            {
                if (ZmenaPopisu != null)
                {
                    //vyvolani udalosti
                    StavText = "Nahravam kontrakt " + _listFuturesKontraktov[p1 - 1].Symbol + kontraktnyMesiac1 + rok1;
                    ZmenaPopisu();
                }
                listdata = NahrajUlozeneData(_listFuturesKontraktov[p1 - 1].Symbol + kontraktnyMesiac1 + rok1, out succes);
            }
            if (succes == false && ExistujeDocasneStiahnutySubor(_listFuturesKontraktov[p1 - 1].Symbol + kontraktnyMesiac1 + rok1))
            {
                listdata = NahrajDocasneUlozeneData(_listFuturesKontraktov[p1 - 1].Symbol + kontraktnyMesiac1 + rok1, out succes);
            }
            if (succes == false)
            {
                if (ZmenaPopisu != null)
                {
                    //vyvolani udalosti
                    StavText = "Stahujem kontrakt " + _listFuturesKontraktov[p1 - 1].Symbol + kontraktnyMesiac1 + rok1;
                    ZmenaPopisu();
                }
                listdata = ParsujKontraktXml(out succes, skratene: null, kontrakIndex: p1, kontraktnyMesiac: kontraktnyMesiac1, rok: rok1);
            }

            HodnotaBodu = _listFuturesKontraktov[p1 - 1].HodnotaBod;
            Interval = _listFuturesKontraktov[p1 - 1].VelkostTicku;

            hodnotaBodu = double.Parse(HodnotaBodu);
            return listdata;
        }

        private List<ObchodnyDen> NahrajDocasneUlozeneData(string komodita, out bool succes)
        {
            succes = false;
            var list = PracaSoSubormi.NahrajDocasneData(komodita);
            if (list.Count > 0)
            {
                succes = true;
            }
            return list;
        }

        private bool ExistujeDocasneStiahnutySubor(string komodita)
        {
            return PracaSoSubormi.SkontrolujDocasnySubor(komodita);
        }

        private List<ObchodnyDen> NahrajUlozeneData(string komodita, out bool succes)
        {
            succes = false;
            var list = PracaSoSubormi.NahrajData(komodita);
            if (list.Count > 0)
            {
                succes = true;
            }
            return list;
        }

        private bool ExistujeStiahnutySubor(string komodita)
        {
            return PracaSoSubormi.SkontrolujSubor(komodita);
        }

        private List<Spread> vypocitajSpread(List<ObchodnyDen> listKontrakt1, List<ObchodnyDen> listKontrakt2, double hodnotaBodu1, double hodnotaBodu2)
        {
            try
            {
                var opacne = listKontrakt1[0].Date > listKontrakt2[0].Date;
                int indexZaciatkuDlhsiehoSpreadu;
                int dlzka;

                var list = new List<Spread>();
                if(opacne)
                {
                    indexZaciatkuDlhsiehoSpreadu = listKontrakt1.IndexOf(new ObchodnyDen(listKontrakt2.First().Date));
                    dlzka = listKontrakt1.Count - indexZaciatkuDlhsiehoSpreadu;
                    if (dlzka > listKontrakt2.Count)
                    {
                        dlzka = listKontrakt2.Count;
                    }
                }
                else
                {
                    indexZaciatkuDlhsiehoSpreadu = listKontrakt2.IndexOf(new ObchodnyDen(listKontrakt1.First().Date));
                    dlzka = listKontrakt2.Count - indexZaciatkuDlhsiehoSpreadu;
                    if (dlzka > listKontrakt1.Count)
                    {
                        dlzka = listKontrakt1.Count;
                    }
                }
          //      var indexZaciatkuDlhsiehoSpreadu = listKontrakt2.IndexOf(new ObchodnyDen(listKontrakt1.First().Date));       //  kontrola este z druhej strany
                try
                {
                    for (int i = 0; i < dlzka; i++)
                    {
                        double spread = 0;
                        if (opacne)
                        {
                            if (hodnotaBodu1 != hodnotaBodu2)
                                spread = hodnotaBodu1*listKontrakt1[i + indexZaciatkuDlhsiehoSpreadu].Settle -
                                         hodnotaBodu2*listKontrakt2[i].Settle;
                            else
                                spread = listKontrakt1[i + indexZaciatkuDlhsiehoSpreadu].Settle -
                                         listKontrakt2[i].Settle;

                            list.Add(new Spread(spread, listKontrakt2[i].Date));
                        }
                        else
                        {
                            if (hodnotaBodu1 != hodnotaBodu2)
                                spread = hodnotaBodu1*listKontrakt1[i].Settle -
                                         hodnotaBodu2*listKontrakt2[i + indexZaciatkuDlhsiehoSpreadu].Settle;
                            else
                                spread = listKontrakt1[i].Settle -
                                         listKontrakt2[i + indexZaciatkuDlhsiehoSpreadu].Settle;

                            list.Add(new Spread(spread, listKontrakt1[i].Date));
                        }
                        
                    }
                }
                catch (Exception e)
                {
                    Console.Write(e.ToString());
                }
                if (hodnotaBodu1 != hodnotaBodu2)
                    HodnotaBodu = "1";

                return list;
            }
            catch (Exception)
            {
                if (ZmenaPopisu != null)
                {
                    //vyvolani udalosti
                    StavText = "Nieje mozne stiahnut data, vyberte ine kontrakty";
                    ZmenaPopisu();
                }
                return null;
            }
        }

        public List<ObchodnyDen> ParsujKontraktXml(out bool succes, string skratene = null, int kontrakIndex = 0, string kontraktnyMesiac = null, string rok = null)
        {
            var list = new List<ObchodnyDen>();
            succes = false;

            string komodita = skratene ?? _listFuturesKontraktov[kontrakIndex - 1].Symbol + kontraktnyMesiac + rok;

            //string uri = "http://www.quandl.com/api/v1/datasets/OFDP/FUTURE_" + komodita + ".xml?auth_token=UqHLDQVcxZy5AknRTZX9";
            string uri = String.Format(_listFuturesKontraktov[kontrakIndex - 1].Url, komodita, string.Empty, string.Empty);
            try
            {
                WebRequest req = WebRequest.Create(uri);
                req.Proxy = WebRequest.DefaultWebProxy;
                req.UseDefaultCredentials = true;
                var resp = req.GetResponse();
                var textReader = new StreamReader(resp.GetResponseStream());
                var xmlReader = new XmlTextReader(textReader);
                var doc = new XmlDocument();
                doc.Load(xmlReader);

                int multiplikator = doc.InnerXml.Contains("Change") ? 10 : 8;


                var text = doc.InnerXml.Replace(" type=\"array\"", "").Replace(" type=\"float\"", "");
                var index = text.IndexOf("<data>");
                text = text.Remove(0, index);
                index = text.IndexOf("</dataset>");
                text = text.Substring(0, index);
                text = text.Replace("  <datum>", "  <den>").Replace("  </datum>", "  </den>");
                text = text.Replace("<datum></datum>", "<datum>0</datum>");

                XDocument xdoc = XDocument.Parse(text);

                var authors = xdoc.Descendants("datum");
                var dlzka = authors.Count() / multiplikator;

                if (dlzka > 0)
                {
                    for (int i = 0; i < dlzka; i++)
                    {
                        //var dat = authors.ElementAt(i * multiplikator + 1).Value.ToString();
                        var datum = DateTime.Parse(authors.ElementAt(i * multiplikator + 1).Value.ToString());
                       //var data = authors.ElementAt(i * multiplikator + 2).Value.ToString();
                        var open = double.Parse(authors.ElementAt(i * multiplikator + 2).Value.ToString(), CultureInfo.InvariantCulture);
                        var high = double.Parse(authors.ElementAt(i * multiplikator + 3).Value.ToString(), CultureInfo.InvariantCulture);
                        var low = double.Parse(authors.ElementAt(i * multiplikator + 4).Value.ToString(), CultureInfo.InvariantCulture);
                        var close = double.Parse(authors.ElementAt(i * multiplikator + (multiplikator == 8 ? 5 : 7)).Value.ToString(), CultureInfo.InvariantCulture);
                        var volume = double.Parse(authors.ElementAt(i * multiplikator + (multiplikator == 8 ? 6 : 8)).Value.ToString(), CultureInfo.InvariantCulture);
                        var open_interest = double.Parse(authors.ElementAt(i * multiplikator + (multiplikator == 8 ? 7 : 9)).Value.ToString(), CultureInfo.InvariantCulture);
                        var den = new ObchodnyDen(datum,open,high,low,close,volume,open_interest);
                        list.Add(den);
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (ZmenaPopisu != null)
                {
                    //vyvolani udalosti
                    StavText = "Chyba pri stahovani "+e.Message;
                    ZmenaPopisu();
                }
            }

            if (list.Count>0)
            {
                succes = true;
                if (UlozData(list.First()))
                {
                    PracaSoSubormi.UlozAktualnyList(list, komodita);
                }
                else
                {
                    PracaSoSubormi.UlozDocasnyAktualnyList(list, komodita);
                }
            }

            return list;
        }

        private bool UlozData(ObchodnyDen obchodnyDen)
        {
            var den = (DateTime.Now - obchodnyDen.Date).Days;
            return den > 6;
        }

        public List<string> LoadKontrakty(string komodita)
        {
            var list = new List<string>();

            _listSpecifikacii = new List<SpecifikaciaKontraktu>();
            var file = Environment.CurrentDirectory.Substring(0, System.Environment.CurrentDirectory.LastIndexOf("bin")) + "Kontrakty\\" + komodita + ".csv";

            if (File.Exists(file))
            {
                var csv = new CsvReader(new StreamReader(file));
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.IgnoreHeaderWhiteSpace = true;
                csv.Configuration.Delimiter = ";";
                var myCustomObjects = csv.GetRecords<SpecifikaciaKontraktu>();

                foreach (var item in myCustomObjects)
                {
                    _listSpecifikacii.Add(item);
                    list.Add(item.Symbol);
                }
            }

            return list;
        }


        public List<string> LoadKontrakty()
        {
            _listFuturesKontraktov = new PracaSoSubormi().GetKontraktyPodrobnejsie();
            var list = new List<string> {"-----------"};
            list.AddRange(_listFuturesKontraktov.Select(item => item.Komodita + "  - " + item.Symbol));
            return list;
        }

        public List<string> LoadRokySpecificke(string komodita)
        {
            var list = new List<string>();
            foreach (var item in _listFuturesKontraktov.Where(item => item.Komodita==komodita.Substring(0,komodita.IndexOf("  -"))))
            {
                list.AddRange(PocitajRoky(item.StartRok, item.EndRok));
            }

            return list;
        }

        private IEnumerable<string> PocitajRoky(string start, string koniec)
        {
            var list = new List<string>();
            try
            {
                int startRok = ParsujRok(start);
                int koniecRok = ParsujRok(koniec);           

                for (int i = koniecRok; i >= startRok; i--)
                {
                    list.Add(i.ToString());
                }
            }
            catch (Exception)
            {
                list.Add("Chyba load");
            }
            return list;
        }

        public List<string> LoadMesiaceSpecificke(string kontrakt, string rokKont)
        {
            var list = new List<string>();
            int rok = ParsujRok(rokKont);
            kontrakt = kontrakt.Contains("  -") ? kontrakt.Substring(0, kontrakt.IndexOf("  -")) : kontrakt;

            foreach (var item in _listFuturesKontraktov)
            {
                if (item.Komodita==kontrakt)
                {
                    if (rok == ParsujRok(item.StartRok))
                    {
                        if (item.StartRok.Contains("-"))
                        {
                            var poslednyMesiac = item.StartRok[5].ToString();
                            var listVsetkych = ParsujKontraktyVsetky(item);
                            foreach (var itemVset in listVsetkych)
                            {
                                if (itemVset == poslednyMesiac)
                                {
                                    list.Add(KontraktneMesiace.GetPopis(itemVset));
                                    break;
                                }
                                list.Add(KontraktneMesiace.GetPopis(itemVset));
                            }
                        }
                        else
                        {
                            list.AddRange(ParsujKontraktyVsetky(item).Select(KontraktneMesiace.GetPopis));
                            break;
                        }
                    }
                    else if (rok == ParsujRok(item.EndRok))
                    {
                        if (item.EndRok.Contains("-"))
                        {
                            var poslednyMesiac = item.EndRok[5].ToString(CultureInfo.InvariantCulture);
                            var listVsetkych = ParsujKontraktyVsetky(item);
                            foreach (var itemVset in listVsetkych)
                            {
                                if (itemVset == poslednyMesiac)
                                {
                                    list.Add(KontraktneMesiace.GetPopis(itemVset));
                                    break;
                                }
                                list.Add(KontraktneMesiace.GetPopis(itemVset));
                            }
                        }
                        else
                        {
                            list.AddRange(ParsujKontraktyVsetky(item).Select(KontraktneMesiace.GetPopis));
                            break;
                        }
                    }
                    else if (rok < ParsujRok(item.EndRok) && rok > ParsujRok(item.StartRok))
                    {
                        list.AddRange(ParsujKontraktyVsetky(item).Select(KontraktneMesiace.GetPopis));
                        break;
                    }
                }
            }
            return list;
        }

        private IEnumerable<string> ParsujKontraktyVsetky(SirsiaSpecifikaciaKontraktu item)
        {
            return item.TypyKontraktov.Where((t, i) => i%2 == 0).Select(t => t.ToString(CultureInfo.InvariantCulture)).ToList();
        }

        public int ParsujRok(string rok)
        {
            if (rok.Contains("-"))
            {
                return int.Parse(rok.Substring(0, rok.IndexOf("-")));
            }
            else
            {
                return int.Parse(rok);
            }
        }

        internal bool PocitajSezonnost(int komodita, int komodita2, string kontraktnyMesiac1, string rokKont1, string kontraktnyMesiac2, string rokKont2, string roky)
        {
            try
            {
                var rok1 = int.Parse(rokKont1);
                var rok2 = int.Parse(rokKont2);
                var listMinulychRokoch = new List<Spread>();

                bool succes1;
                bool succes2;
                bool succes3;
                bool succes4;
                double hodnotaBodu1;
                double hodnotaBodu2;
                double hodnotaBodu1Vedl;
                double hodnotaBodu2Vedl;

                var listKontraktHlavny1 = NacitajData(komodita, kontraktnyMesiac1, rokKont1, out succes1, out hodnotaBodu1);
                var listKontraktHlavny2 = NacitajData(komodita2, kontraktnyMesiac2, rokKont2, out succes2, out hodnotaBodu2);
                var spreadHlavny = vypocitajSpread(listKontraktHlavny1, listKontraktHlavny2, hodnotaBodu1, hodnotaBodu2);

                listMinulychRokoch = NacitajMinuleRoky(komodita, komodita2, kontraktnyMesiac1, kontraktnyMesiac2, rok1, rok2,int.Parse(roky), spreadHlavny.Count);
                //var listKontraktVedlajsi1 = NacitajData(komodita, kontraktnyMesiac1, (rok1 - 1).ToString(), out succes3, out hodnotaBodu1Vedl);
                //var listKontraktVedlajsi2 = NacitajData(komodita, kontraktnyMesiac2, (rok2 - 1).ToString(), out succes4, out hodnotaBodu2Vedl);
                //var spreadVedlajsi = vypocitajSpread(listKontraktVedlajsi1, listKontraktVedlajsi2, hodnotaBodu1Vedl, hodnotaBodu2Vedl);

                // var dnesnyDen = DateTime.Now; 

                var index = listMinulychRokoch.IndexOf(new Spread(0, new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, DateTime.Now.Day)));
                var index2 = listMinulychRokoch.IndexOf(new Spread(0, new DateTime(DateTime.Now.Year - 2, DateTime.Now.Month, DateTime.Now.Day)));

                //spreadHlavny = spreadHlavny.GetRange(0, index2 - index);    //zmenene 21.10.2014
                // spreadVedlajsi = spreadVedlajsi.GetRange(0, index2);

                dataGrafTerajsi = Preved100Graf(spreadHlavny, 0).GetRange(0,140);
                dataGrafVedalsi = listMinulychRokoch.Count < 227 ? listMinulychRokoch : listMinulychRokoch.GetRange(0, 227);
                

                //dataGrafVedalsi = Preved110Graf(spreadVedlajsi,index);
                //dataGrafVedalsi = Preved100Graf(dataGrafVedalsi, 0);

                // zistit dlzku presahu minuleho intervalu oproti predoslemu, to pouyit na porovnanie predpovede
                // potom od konca toho presahu odpocitat rok, cize sucasnz interval bude kratsi oproti preodslemu
                // x - dnesny den
                // y - koniec predch. kontraktu
                // y - 365 - zaciatok intervalu , datum v tvare den.mesiac
                // dlzka sucasneho intervalu bude (y-365 : x)
                // dlyka predch. intervalu (y-365 : y)

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private List<Spread> NacitajMinuleRoky(int komodita, int komodita2, string kontraktnyMesiac1, string kontraktnyMesiac2, int rok1, int rok2, int pocetRokov, int count)
        {
            try
            {
                var list = new List<List<Spread>>();
                for (int i = 0; i < pocetRokov; i++)
                {
                    var succes = true;
                    double hodnotaBodu1;
                    double hodnotaBodu2;
                    var listKontraktVedlajsi1 = NacitajData(komodita, kontraktnyMesiac1, (rok1 - (i + 1)).ToString(), out succes, out hodnotaBodu1);
                    var listKontraktVedlajsi2 = NacitajData(komodita2, kontraktnyMesiac2, (rok2 - (i + 1)).ToString(), out succes, out hodnotaBodu2);
                    var spreadVedlajsi = vypocitajSpread(listKontraktVedlajsi1, listKontraktVedlajsi2, hodnotaBodu1, hodnotaBodu2);

                    var index = spreadVedlajsi.IndexOf(new Spread(0, new DateTime(DateTime.Now.Year - (i + 1), DateTime.Now.Month, DateTime.Now.Day)));
                    var dataGrafVedlajsi = Preved110Graf(spreadVedlajsi, count);
                    list.Add(dataGrafVedlajsi);
                }
                dataGrafVsetky = SpriemerujVsetkyData(list);
                return dataGrafVedalsi = SpriemerujPredchadzajuceRoky(list);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private int NajmensiList(List<List<Spread>> list)
        {
            return list.Min(x => x.Count);
        }

        private List<List<Spread>> SpriemerujVsetkyData(List<List<Spread>> list)
        {
            var outputList = new List<List<Spread>>();

            foreach (var rok in list)
            {
                var pomList = new List<Spread>();
                for (int i = 0; i < NajmensiList(list); i++)
                {
                    pomList.Add(new Spread(rok[i].Value,list[0][i].Date));
                }
                outputList.Add(pomList);
            }

            return outputList;
        }

        private List<Spread> SpriemerujPredchadzajuceRoky(List<List<Spread>> list)
        {
            var outputList = new List<Spread>();

            for (int i = 0; i < NajmensiList(list); i++)
            {
                var hodnota = 0.0;
                for (int j = 0; j < list.Count; j++)
                {
                    if (list[j].Count > i)
                    {
                        hodnota += ((Spread)(list[j][i])).Value;
                    }
                }
                hodnota = hodnota/list.Count;
                outputList.Add(new Spread(hodnota, ((Spread) (list[0][i])).Date.AddYears(1)));
            }

            var max = -9999.0;
            var min = 9999.0;

            foreach (var item in outputList)
            {
                if (item.Value > max)
                {
                    max = item.Value;
                } if (item.Value < min)
                {
                    min = item.Value;
                }
            }

            return PrekonvertujList(Preved110Graf(outputList,0));
        }

        private List<Spread> Preved110Graf(List<Spread> spreadVedlajsi, int index)
        {
            var ind = spreadVedlajsi.Count <= index ? spreadVedlajsi.Count : index;

            //var hlavList = spreadVedlajsi.GetRange(0, ind);           
            var hlavList = spreadVedlajsi;

            var max = -9999.0;
            var min = 9999.0;

            foreach (var item in hlavList)
            {
                if (item.Value > max)
                {
                    max = item.Value;
                } if (item.Value < min)
                {
                    min = item.Value;
                }
            }

            var pomer = 100 / (max - min);
            var list = new List<Spread>();

            foreach (var item in hlavList)
            {
                list.Add(new Spread(item.Value * pomer, item.Date));
                //Console.WriteLine(item.Value * pomer);
            }

            return PrekonvertujList(list,index);
        }

        private List<Spread> PrekonvertujList(List<Spread> list, int index)
        {
            //var pomList = list.GetRange(0, index);
            var hlavList = list;

            var max = -9999.0;
            var min = 9999.0;

            foreach (var item in hlavList)
            {
                if (item.Value > max)
                {
                    max = item.Value;
                } if (item.Value < min)
                {
                    min = item.Value;
                }
            }
            var posun = 100 - max;

            for (int i = 0; i < list.Count; i++)
            {
                list[i].Value = list[i].Value + posun;
                //Console.WriteLine(list[i].Value + posun);
            }

            return list;
        }

        private List<Spread> Preved100Graf(List<Spread> spreadHlavny, int posunRokov)
        {
            var max = -9999.0;
            var min = 9999.0;

            //var pomList = spreadHlavny.Count == 253 ? spreadHlavny : spreadHlavny.GetRange(54, 253);
            var pomList = spreadHlavny;

            foreach (var item in pomList)
            {
                if (item.Value > max)
                {
                    max = item.Value;
                } if (item.Value < min)
                {
                    min = item.Value;
                }
            }

            var pomer = 100/(max - min);
            var list = new List<Spread>();

            foreach (var item in spreadHlavny)
            {
                int day = item.Date.Day;
                list.Add(new Spread(item.Value * pomer, new DateTime(item.Date.Year-posunRokov,item.Date.Month,day)));
            //    Console.WriteLine(item.Value * pomer);
            }

            return PrekonvertujList(list);
        }

        private List<Spread> PrekonvertujList(List<Spread> list)
        {
            var max = -9999.0;
            var min = 9999.0;

            //var pomList = list.Count==253 ? list : list.GetRange(54,253);

            foreach (var item in list)
            {
                if (item.Value > max)
                {
                    max = item.Value;
                } if (item.Value < min)
                {
                    min = item.Value;
                }
            }
            var posun = 100 - max;

            for (int i = 0; i < list.Count; i++)
            {
                list[i].Value = list[i].Value + posun;
            }

            return list;
        }

        public List<Spread> dataGrafTerajsi { get; set; }

        public List<Spread> dataGrafVedalsi { get; set; }

        public List<List<Spread>> dataGrafVsetky { get; set; }

        public List<ObchodnyDen> GetDataPreGraf(int p1, string p2, string p3, int dlzka)
        {
            bool succes;
            double hodnotaBodu;
            var mesiac = p2.Contains("  -") ? p2.Substring(0, p2.IndexOf("  -", System.StringComparison.Ordinal)) : p2;
            var listKontrakt = NacitajData(p1, mesiac, p3, out succes, out hodnotaBodu);
            foreach (var obchodnyDen in listKontrakt)
            {
                if (obchodnyDen.High==0)
                {
                    obchodnyDen.High = obchodnyDen.Settle;
                }
                if (obchodnyDen.Low == 0)
                {
                    obchodnyDen.Low = obchodnyDen.Settle;
                }
                if (obchodnyDen.Open == 0)
                {
                    obchodnyDen.Open = obchodnyDen.Settle;
                }
            }

            dlzka = 12;
            var koncDatum = listKontrakt[0].Date.AddMonths(-1*dlzka);

            if (dlzka != 0)
            {
                for (int i = 0; i < listKontrakt.Count; i++)
                {
                    if (listKontrakt[i].Date < koncDatum)
                    {
                        return listKontrakt.Take(i).ToList();
                    }
                }
            }

            return listKontrakt;
        }

        public string PocitajStatistiky(int komodita1, int komodita2, string mesiac1, string rok1, string mesiac2, string rok2, int pocetRokov)
        {
            var list = new List<List<Spread>>();
            mesiac1 = mesiac1.Contains("  -") ? mesiac1.Substring(0, mesiac1.IndexOf("  -", System.StringComparison.Ordinal)) : mesiac1;
            mesiac2 = mesiac2.Contains("  -") ? mesiac2.Substring(0, mesiac2.IndexOf("  -", System.StringComparison.Ordinal)) : mesiac2;

            for (int i = 0; i < pocetRokov; i++)
            {
                bool succes;
                double hodnotaBodu1;
                double hodnotaBodu2;
                var listKontrakt1 = NacitajData(komodita1, mesiac1, (int.Parse(rok1) - (i + 1)).ToString(CultureInfo.InvariantCulture), out succes, out hodnotaBodu1);
                var listKontrakt2 = NacitajData(komodita2, mesiac2, (int.Parse(rok2) - (i + 1)).ToString(CultureInfo.InvariantCulture), out succes, out hodnotaBodu2);
                var spread = vypocitajSpread(listKontrakt1, listKontrakt2, hodnotaBodu1, hodnotaBodu2);

                list.Add(spread);
            }

            return Testy.PocitajStatistiky(list, 10);
        }

        public string PocitajHodnotuVyberu(int komodita1, int komodita2, double pX, double pY)
        {
            return Math.Abs(double.Parse(HodnotaBodu)*(pX - pY)).ToString();
        }

        public List<string> GetMesiace()
        {
            var list = new List<string>();
            int mesiac = 3;
            var poslednyDatum = PlnyListSpread[0].Date;

            foreach (var spread in PlnyListSpread)
            {
                if (spread.Date < poslednyDatum.AddMonths(-mesiac))
                {
                    list.Add(mesiac.ToString());
                    mesiac +=3;
                }
            }
            return list;
        }

        public List<KorelacnySpread> PocitajGrafKorelacie(int komodita1, int komodita2, string mesiac1, string kontrakt1, string mesiac2, string kontrakt2, int dlzka)
        {
            var succes = true;
            double hodnotaBodu1;
            double hodnotaBodu2;
            var list = new List<KorelacnySpread>();

            for (int i = 0; i < dlzka; i++)
            {
                var kontrakt1Upr = (int.Parse(kontrakt1) - i).ToString();
                var kontrakt2Upr = (int.Parse(kontrakt2) - i).ToString();
                var listKontraktHlavny1 = NacitajData(komodita1, mesiac1, kontrakt1Upr, out succes, out hodnotaBodu1);
                var listKontraktHlavny2 = NacitajData(komodita2, mesiac2, kontrakt2Upr, out succes, out hodnotaBodu2);
                var spread = new KorelacnySpread(vypocitajSpread(listKontraktHlavny1, listKontraktHlavny2, hodnotaBodu1, hodnotaBodu2).Take(250).ToList());
                spread.SetRok(spread.Spread[0].Date.Year);

                foreach (var item in spread.Spread)
                {
                    item.Date = item.Date.AddYears(i);
                }
                list.Add(spread);
            }

            return list;
        }

        public void VytvorDownloadManagera()
        {
            DownloadManager = new SpravcaDownloadManager(this);
        }

        public void PridajSledovanySpread(int komodita1, int komodita2, string mesiac1, string rok1, string mesiac2, string rok2)
        {
            SledovaneSpready.PridajZaznam(new SledovanySpread(komodita1,mesiac1,rok1,komodita2,mesiac2,rok2));
        }

        public void Koniec()
        {
            SledovaneSpready.UlozData();
        }

        public ListViewItem[] PocetKontraktov()
        {
            var kontrakty = new PracaSoSubormi().GetKontraktyPodrobnejsie();
            var pocetList = new PracaSoSubormi().PocetStiahnutychKontraktov(kontrakty);
            var list = new List<Tuple<string, int>>();
            
            for (int index = 0; index < kontrakty.Count; index++)
            {
                list.Add(new Tuple<string, int>(kontrakty[index].Komodita, pocetList[index]));
            }

            return list.Select((t) => new ListViewItemHelpClass(t)).ToArray();
        }
    }
}
