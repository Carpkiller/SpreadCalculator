using System;
using System.Collections.Generic;
using System.Drawing.Text;
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
        private List<ObchodnyDen> listKontrakt1;
        private List<ObchodnyDen> listKontrakt2;
        public List<Spread> listSpread;
        private List<SpecifikaciaKontraktu> listSpecifikacii;
        private List<SirsiaSpecifikaciaKontraktu> listFuturesKontraktov;
        public Statistika statistika;
        public string stavText = "Ready";

        enum KontraktneMesiace { F, G, H, J, K, M, N, Q, U, V, X, Z };

        public delegate void ZmenaPopisuHandler();
        public event ZmenaPopisuHandler ZmenaPopisu;

        public Jadro()
        {
            listFuturesKontraktov = new List<SirsiaSpecifikaciaKontraktu>();
        }

        public List<ObchodnyDen> parsujKontrakt(string cesta, out bool succes)
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
            listKontrakt1 = new List<ObchodnyDen>();
            listKontrakt2 = new List<ObchodnyDen>();

            var succes1 = false;
            var succes2 = false;

            listKontrakt1 = parsujKontrakt(p1,out succes1);
            listKontrakt2 = parsujKontrakt(p2, out succes2);

            listSpread = vypocitajSpread(listKontrakt1, listKontrakt2);

            if (listSpread.Count > 0)
            {
                statistika = new Statistika(listSpread);
            }

            return succes1 && succes2;
        }


        internal bool ParsujKontrakty(int komodita1, int komodita2, string kontraktnyMesiac1, string rok1, string kontraktnyMesiac2, string rok2)
        {        
            var succes1 = false;
            var succes2 = false;

            var listKontrakt1 = NacitajData(komodita1, kontraktnyMesiac1, rok1, out succes1);
            var listKontrakt2 = NacitajData(komodita2, kontraktnyMesiac2, rok2, out succes2);

            listSpread = vypocitajSpread(listKontrakt1, listKontrakt2);

            if (listSpread!= null)
            {
                if (listSpread.Count > 0)
                {
                    stavText = "Nacitanie uspesne dokoncene";
                    statistika = new Statistika(listSpread);
                }
                else
                {
                    stavText = "Spread mimo rozsah/ chybajuce data";
                }
            }

            if (ZmenaPopisu != null)
            {
                //vyvolani udalosti
             //   stavText = "Nahravanie dokoncene";
                ZmenaPopisu();
            }

            return succes1 && succes2 && listSpread.Count>0;
        }

        private List<ObchodnyDen> NacitajData(int p1, string kontraktnyMesiac1, string rok1, out bool succes)
        {
            succes = false;
            var listdata = new List<ObchodnyDen>();

            if (ExistujeStiahnutySubor(listFuturesKontraktov[p1 - 1].Symbol + kontraktnyMesiac1 + rok1))
            {
                if (ZmenaPopisu != null)
                {
                    //vyvolani udalosti
                    stavText = "Nahravam kontrakt " + listFuturesKontraktov[p1 - 1].Symbol + kontraktnyMesiac1 + rok1;
                    ZmenaPopisu();
                }
                listdata = NahrajUlozeneData(listFuturesKontraktov[p1 - 1].Symbol + kontraktnyMesiac1 + rok1, out succes);
            }
            if (succes == false)
            {
                if (ZmenaPopisu != null)
                {
                    //vyvolani udalosti
                    stavText = "Stahujem kontrakt " + listFuturesKontraktov[p1 - 1].Symbol + kontraktnyMesiac1 + rok1;
                    ZmenaPopisu();
                }
                listdata = parsujKontraktXml(p1, kontraktnyMesiac1, rok1, out succes);
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
                var list = new List<Spread>();
                var indexZaciatkuDlhsiehoSpreadu = listKontrakt2.IndexOf(new ObchodnyDen(listKontrakt1.First().Date));       //  kontrola este z druhej strany
                var dlzka = listKontrakt2.Count - indexZaciatkuDlhsiehoSpreadu;
                if (dlzka > listKontrakt1.Count)
                {
                    dlzka = listKontrakt1.Count;
                }

                try
                {
                    for (int i = 0; i < dlzka; i++)
                    {
                        var spread = listKontrakt1[i].Settle - listKontrakt2[i + indexZaciatkuDlhsiehoSpreadu].Settle;
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
                    stavText = "Nieje mozne stiahnut data, vyberte ine kontrakty";
                    ZmenaPopisu();
                }
                return null;
            }
        }

        public List<ObchodnyDen> parsujKontraktXml(int kontrakIndex, string kontraktnyMesiac, string rok, out bool succes)
        {
            var list = new List<ObchodnyDen>();
            succes = false;
            var komodita = listFuturesKontraktov[kontrakIndex - 1].Symbol + kontraktnyMesiac + rok;

            string uri = "http://www.quandl.com/api/v1/datasets/OFDP/FUTURE_" + komodita + ".xml?auth_token=UqHLDQVcxZy5AknRTZX9";

            try
            {
                WebRequest req = WebRequest.Create(uri);
                req.UseDefaultCredentials = true;
                WebResponse resp = req.GetResponse();
                StreamReader textReader = new StreamReader(resp.GetResponseStream());
                XmlTextReader xmlReader = new XmlTextReader(textReader);
                XmlDocument doc = new XmlDocument();
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
                    stavText = "Chyba pri stahovani "+e.Message;
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

            listSpecifikacii = new List<SpecifikaciaKontraktu>();
            var file = System.Environment.CurrentDirectory.Substring(0, System.Environment.CurrentDirectory.LastIndexOf("bin")) + "Kontrakty\\" + komodita + ".csv";

            if (File.Exists(file))
            {
                var csv = new CsvReader(new StreamReader(file));
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.IgnoreHeaderWhiteSpace = true;
                csv.Configuration.Delimiter = ";";
                var myCustomObjects = csv.GetRecords<SpecifikaciaKontraktu>();

                foreach (var item in myCustomObjects)
                {
                    listSpecifikacii.Add(item);
                    list.Add(item.Symbol);
                }
            }

            return list;
        }


        public List<string> LoadKontrakty()
        {
            listFuturesKontraktov = new PracaSoSubormi().GetKontraktyPodrobnejsie();
            var list = new List<string>();
            list.Add("-----------");
            foreach (var item in listFuturesKontraktov)
            {
                list.Add(item.Komodita);
            }
            return list;
        }

        public List<string> LoadRokySpecificke(string komodita)
        {
            var list = new List<string>();
            foreach (var item in listFuturesKontraktov)
            {
                if (item.Komodita==komodita)
                {
                    list.AddRange(PocitajRoky(item.StartRok, item.EndRok));
                }
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

            foreach (var item in listFuturesKontraktov)
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
                                    list.Add(itemVset);
                                    break;
                                }
                                list.Add(itemVset);
                            }
                        }
                        else
                        {
                            list.AddRange(ParsujKontraktyVsetky(item));
                            break;
                        }
                    }
                    else if (rok == ParsujRok(item.EndRok))
                    {
                        if (item.EndRok.Contains("-"))
                        {
                            var poslednyMesiac = item.EndRok[5].ToString();
                            var listVsetkych = ParsujKontraktyVsetky(item);
                            foreach (var itemVset in listVsetkych)
                            {
                                if (itemVset == poslednyMesiac)
                                {
                                    list.Add(itemVset);
                                    break;
                                }
                                list.Add(itemVset);
                            }
                        }
                        else
                        {
                            list.AddRange(ParsujKontraktyVsetky(item));
                            break;
                        }
                    }
                    else if (rok < ParsujRok(item.EndRok) && rok > ParsujRok(item.StartRok))
                    {
                        list.AddRange(ParsujKontraktyVsetky(item));
                        break;
                    }
                }
            }
            return list;
        }

        private IEnumerable<string> ParsujKontraktyVsetky(SirsiaSpecifikaciaKontraktu item)
        {
            var list = new List<string>();
            for (int i = 0; i < item.TypyKontraktov.Length; i++)
            {
                if (i % 2 == 0)
                {
                    list.Add(item.TypyKontraktov[i].ToString());
                }
            }

            return list;
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

        internal bool pocitajSezonnost(int komodita, string kontraktnyMesiac1, string rokKont1, string kontraktnyMesiac2, string rokKont2, string roky)
        {
            var rok1 = int.Parse(rokKont1);
            var rok2 = int.Parse(rokKont2);
            List<Spread> listMinulychRokov = new List<Spread>();

            bool succes1 = false;
            bool succes2 = false;
            bool succes3 = false;
            bool succes4 = false;
            var listKontraktHlavny1 = NacitajData(komodita, kontraktnyMesiac1, rokKont1, out succes1);
            var listKontraktHlavny2 = NacitajData(komodita, kontraktnyMesiac2, rokKont2, out succes2);
            var spreadHlavny = vypocitajSpread(listKontraktHlavny1, listKontraktHlavny2);

            listMinulychRokov = NacitajMinuleRoky(komodita, kontraktnyMesiac1, kontraktnyMesiac2, rok1, rok2,int.Parse(roky));
            var listKontraktVedlajsi1 = NacitajData(komodita, kontraktnyMesiac1, (rok1-1).ToString(), out succes3);
            var listKontraktVedlajsi2 = NacitajData(komodita, kontraktnyMesiac2, (rok2-1).ToString(), out succes4);
            var spreadVedlajsi = vypocitajSpread(listKontraktVedlajsi1, listKontraktVedlajsi2);

           // var dnesnyDen = DateTime.Now; 

            var index = spreadVedlajsi.IndexOf(new Spread(0, new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, DateTime.Now.Day)));
            var index2 = spreadVedlajsi.IndexOf(new Spread(0, new DateTime(DateTime.Now.Year - 2, DateTime.Now.Month, DateTime.Now.Day)));

            spreadHlavny= spreadHlavny.GetRange(0,index2-index);
           // spreadVedlajsi = spreadVedlajsi.GetRange(0, index2);

            dataGrafTerajsi = Preved100Graf(spreadHlavny,1);
            //dataGrafVedalsi = Preved110Graf(spreadVedlajsi,index);
            dataGrafVedalsi = Preved100Graf(dataGrafVedalsi,0);

            // zistit dlzku presahu minuleho intervalu oproti predoslemu, to pouyit na porovnanie predpovede
            // potom od konca toho presahu odpocitat rok, cize sucasnz interval bude kratsi oproti preodslemu
            // x - dnesny den
            // y - koniec predch. kontraktu
            // y - 365 - zaciatok intervalu , datum v tvare den.mesiac
            // dlzka sucasneho intervalu bude (y-365 : x)
            // dlyka predch. intervalu (y-365 : y)

            return true;
        }

        private List<Spread> NacitajMinuleRoky(int komodita, string kontraktnyMesiac1, string kontraktnyMesiac2, int rok1, int rok2, int pocetRokov)
        {
            List<List<Spread>> list = new List<List<Spread>>();
            for (int i = 0; i < pocetRokov; i++)
            {
                var succes = true;
                var listKontraktVedlajsi1 = NacitajData(komodita, kontraktnyMesiac1, (rok1 - (i+1)).ToString(), out succes);
                var listKontraktVedlajsi2 = NacitajData(komodita, kontraktnyMesiac2, (rok2 - (i + 1)).ToString(), out succes);
                var spreadVedlajsi = vypocitajSpread(listKontraktVedlajsi1, listKontraktVedlajsi2);

                var index = spreadVedlajsi.IndexOf(new Spread(0, new DateTime(DateTime.Now.Year - (i+1), DateTime.Now.Month, DateTime.Now.Day )));
                var dataGrafVedlajsi = Preved110Graf(spreadVedlajsi, 307);
                list.Add(dataGrafVedlajsi);
            }
          return dataGrafVedalsi = SpriemerujPredchadzajuceRoky(list);

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

        public List<ObchodnyDen> GetDataPreGraf(int p1, string p2, string p3)
        {
            bool succes;
            var listKontrakt = NacitajData(p1, p2, p3, out succes);
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
    }
}
