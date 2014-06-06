using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
        private List<SpecifikaciaKontraktu> _listSpecifikacii;
        private List<SirsiaSpecifikaciaKontraktu> _listFuturesKontraktov;
        public Statistika Statistika;
        public string StavText = "Ready";

        public delegate void ZmenaPopisuHandler();
        public event ZmenaPopisuHandler ZmenaPopisu;

        public Jadro()
        {
            _listFuturesKontraktov = new List<SirsiaSpecifikaciaKontraktu>();
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

        internal bool ParsujKontrakty(string p1, string p2)
        {
            _listKontrakt1 = new List<ObchodnyDen>();
            _listKontrakt2 = new List<ObchodnyDen>();

            bool succes1;
            bool succes2;

            _listKontrakt1 = ParsujKontrakt(p1,out succes1);
            _listKontrakt2 = ParsujKontrakt(p2, out succes2);

            ListSpread = vypocitajSpread(_listKontrakt1, _listKontrakt2);

            if (ListSpread.Count > 0)
            {
                Statistika = new Statistika(ListSpread);
            }

            return succes1 && succes2;
        }


        internal bool ParsujKontrakty(int komodita1, int komodita2, string kontraktnyMesiac1, string rok1, string kontraktnyMesiac2, string rok2)
        {        
            bool succes1;
            bool succes2;

            kontraktnyMesiac1 = kontraktnyMesiac1.Contains("  -") ? kontraktnyMesiac1.Substring(0, kontraktnyMesiac1.IndexOf("  -")) : kontraktnyMesiac1;
            kontraktnyMesiac2 = kontraktnyMesiac2.Contains("  -") ? kontraktnyMesiac2.Substring(0, kontraktnyMesiac2.IndexOf("  -")) : kontraktnyMesiac2;

            var listKontrakt1 = NacitajData(komodita1, kontraktnyMesiac1, rok1, out succes1);
            var listKontrakt2 = NacitajData(komodita2, kontraktnyMesiac2, rok2, out succes2);

            ListSpread = vypocitajSpread(listKontrakt1, listKontrakt2);

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

        private List<ObchodnyDen> NacitajData(int p1, string kontraktnyMesiac1, string rok1, out bool succes)
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
            if (succes == false)
            {
                if (ZmenaPopisu != null)
                {
                    //vyvolani udalosti
                    StavText = "Stahujem kontrakt " + _listFuturesKontraktov[p1 - 1].Symbol + kontraktnyMesiac1 + rok1;
                    ZmenaPopisu();
                }
                listdata = ParsujKontraktXml(p1, kontraktnyMesiac1, rok1, out succes);
            }

            return listdata;
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

        private List<Spread> vypocitajSpread(List<ObchodnyDen> listKontrakt1, List<ObchodnyDen> listKontrakt2)
        {
            try
            {
                var opacne = listKontrakt1[0].Date > listKontrakt2[0].Date;
                int indexZaciatkuDlhsiehoSpreadu;
                var list = new List<Spread>();
                if(opacne)
                {
                    indexZaciatkuDlhsiehoSpreadu = listKontrakt1.IndexOf(new ObchodnyDen(listKontrakt2.First().Date)); 
                }
                else
                {
                    indexZaciatkuDlhsiehoSpreadu = listKontrakt2.IndexOf(new ObchodnyDen(listKontrakt1.First().Date)); 
                }
          //      var indexZaciatkuDlhsiehoSpreadu = listKontrakt2.IndexOf(new ObchodnyDen(listKontrakt1.First().Date));       //  kontrola este z druhej strany
                var dlzka = listKontrakt2.Count - indexZaciatkuDlhsiehoSpreadu;
                if (dlzka > listKontrakt1.Count)
                {
                    dlzka = listKontrakt1.Count;
                }

                try
                {                    
                    for (int i = 0; i < dlzka; i++)
                    {
                        double spread;
                        if (opacne)
                        {
                            spread = listKontrakt1[i + indexZaciatkuDlhsiehoSpreadu].Settle - listKontrakt2[i].Settle;
                        }
                        else
                        {
                            spread = listKontrakt1[i].Settle - listKontrakt2[i + indexZaciatkuDlhsiehoSpreadu].Settle;
                        }
                        list.Add(new Spread(spread, listKontrakt1[i].Date));
                    }
                }
                catch (Exception e)
                {
                    Console.Write(e.ToString());
                }


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

        public List<ObchodnyDen> ParsujKontraktXml(int kontrakIndex, string kontraktnyMesiac, string rok, out bool succes)
        {
            var list = new List<ObchodnyDen>();
            succes = false;
            var komodita = _listFuturesKontraktov[kontrakIndex - 1].Symbol + kontraktnyMesiac + rok;

            string uri = "http://www.quandl.com/api/v1/datasets/OFDP/FUTURE_" + komodita + ".xml?auth_token=UqHLDQVcxZy5AknRTZX9";

            try
            {
                WebRequest req = WebRequest.Create(uri);
                req.UseDefaultCredentials = true;
                var resp = req.GetResponse();
                var textReader = new StreamReader(resp.GetResponseStream());
                var xmlReader = new XmlTextReader(textReader);
                var doc = new XmlDocument();
                doc.Load(xmlReader);


                var text = doc.InnerXml.Replace(" type=\"array\"", "").Replace(" type=\"float\"", "");
                var index = text.IndexOf("<data>");
                text = text.Remove(0, index);
                index = text.IndexOf("</dataset>");
                text = text.Substring(0, index);
                text = text.Replace("  <datum>", "  <den>").Replace("  </datum>", "  </den>");

                XDocument xdoc = XDocument.Parse(text);

                var authors = xdoc.Descendants("datum");
                var dlzka = authors.Count() / 8;

                if (dlzka > 0)
                {
                    for (int i = 0; i < dlzka; i++)
                    {
                        var dat = authors.ElementAt(i*8 + 1).Value.ToString();
                        var datum = DateTime.Parse(authors.ElementAt(i*8 + 1).Value.ToString());
                        var open = double.Parse(authors.ElementAt(i * 8 + 2).Value.ToString(), CultureInfo.InvariantCulture);
                        var high = double.Parse(authors.ElementAt(i * 8 + 3).Value.ToString(), CultureInfo.InvariantCulture);
                        var low = double.Parse(authors.ElementAt(i * 8 + 4).Value.ToString(), CultureInfo.InvariantCulture);
                        var close = double.Parse(authors.ElementAt(i * 8 + 5).Value.ToString(), CultureInfo.InvariantCulture);
                        var volume = double.Parse(authors.ElementAt(i * 8 + 6).Value.ToString(), CultureInfo.InvariantCulture);
                        var open_interest = double.Parse(authors.ElementAt(i * 8 + 7).Value.ToString(), CultureInfo.InvariantCulture);
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

        internal bool PocitajSezonnost(int komodita, string kontraktnyMesiac1, string rokKont1, string kontraktnyMesiac2, string rokKont2, string roky)
        {
            try
            {
                var rok1 = int.Parse(rokKont1);
                var rok2 = int.Parse(rokKont2);
                var listMinulychRokov = new List<Spread>();

                bool succes1;
                bool succes2;
                bool succes3;
                bool succes4;
                var listKontraktHlavny1 = NacitajData(komodita, kontraktnyMesiac1, rokKont1, out succes1);
                var listKontraktHlavny2 = NacitajData(komodita, kontraktnyMesiac2, rokKont2, out succes2);
                var spreadHlavny = vypocitajSpread(listKontraktHlavny1, listKontraktHlavny2);

                listMinulychRokov = NacitajMinuleRoky(komodita, kontraktnyMesiac1, kontraktnyMesiac2, rok1, rok2,
                    int.Parse(roky));
                var listKontraktVedlajsi1 = NacitajData(komodita, kontraktnyMesiac1, (rok1 - 1).ToString(), out succes3);
                var listKontraktVedlajsi2 = NacitajData(komodita, kontraktnyMesiac2, (rok2 - 1).ToString(), out succes4);
                var spreadVedlajsi = vypocitajSpread(listKontraktVedlajsi1, listKontraktVedlajsi2);

                // var dnesnyDen = DateTime.Now; 

                var index =
                    spreadVedlajsi.IndexOf(new Spread(0,
                        new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, DateTime.Now.Day)));
                var index2 =
                    spreadVedlajsi.IndexOf(new Spread(0,
                        new DateTime(DateTime.Now.Year - 2, DateTime.Now.Month, DateTime.Now.Day)));

                spreadHlavny = spreadHlavny.GetRange(0, index2 - index);
                // spreadVedlajsi = spreadVedlajsi.GetRange(0, index2);

                dataGrafTerajsi = Preved100Graf(spreadHlavny, 1);
                //dataGrafVedalsi = Preved110Graf(spreadVedlajsi,index);
                dataGrafVedalsi = Preved100Graf(dataGrafVedalsi, 0);

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

        private List<Spread> NacitajMinuleRoky(int komodita, string kontraktnyMesiac1, string kontraktnyMesiac2, int rok1, int rok2, int pocetRokov)
        {
            try
            {
                var list = new List<List<Spread>>();
                for (int i = 0; i < pocetRokov; i++)
                {
                    var succes = true;
                    var listKontraktVedlajsi1 = NacitajData(komodita, kontraktnyMesiac1, (rok1 - (i + 1)).ToString(), out succes);
                    var listKontraktVedlajsi2 = NacitajData(komodita, kontraktnyMesiac2, (rok2 - (i + 1)).ToString(), out succes);
                    var spreadVedlajsi = vypocitajSpread(listKontraktVedlajsi1, listKontraktVedlajsi2);

                    var index = spreadVedlajsi.IndexOf(new Spread(0, new DateTime(DateTime.Now.Year - (i + 1), DateTime.Now.Month, DateTime.Now.Day)));
                    var dataGrafVedlajsi = Preved110Graf(spreadVedlajsi, 307);
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

        private List<List<Spread>> SpriemerujVsetkyData(List<List<Spread>> list)
        {
            var outputList = new List<List<Spread>>();

            foreach (var rok in list)
            {
                var pomList = new List<Spread>();
                for (int i = 0; i < 307; i++)
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

            for (int i = 0; i < 307; i++)
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
                outputList.Add(new Spread(hodnota, ((Spread) (list[0][i])).Date));
            }

            return outputList;
        }

        private List<Spread> Preved110Graf(List<Spread> spreadVedlajsi, int index)
        {
            var ind = spreadVedlajsi.Count <= index ? spreadVedlajsi.Count : index;

            var hlavList = spreadVedlajsi.GetRange(0, ind);           

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
            //    Console.WriteLine(item.Value * pomer);
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
            }

            return list;
        }

        private List<Spread> Preved100Graf(List<Spread> spreadHlavny, int posunRokov)
        {
            var max = -9999.0;
            var min = 9999.0;

            var pomList = spreadHlavny.Count == 253 ? spreadHlavny : spreadHlavny.GetRange(54, 253);

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

            var pomList = list.Count==253 ? list : list.GetRange(54,253);

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

        public List<ObchodnyDen> GetDataPreGraf(int p1, string p2, string p3)
        {
            bool succes;
            var mesiac = p2.Contains("  -") ? p2.Substring(0, p2.IndexOf("  -", System.StringComparison.Ordinal)) : p2;
            var listKontrakt = NacitajData(p1, mesiac, p3, out succes);
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
                var listKontrakt1 = NacitajData(komodita1, mesiac1, (int.Parse(rok1) - (i + 1)).ToString(CultureInfo.InvariantCulture), out succes);
                var listKontrakt2 = NacitajData(komodita2, mesiac2, (int.Parse(rok2) - (i + 1)).ToString(CultureInfo.InvariantCulture), out succes);
                var spread = vypocitajSpread(listKontrakt1, listKontrakt2);

                list.Add(spread);
            }

            return Testy.PocitajStatistiky(list, 10);
        }
    }
}
