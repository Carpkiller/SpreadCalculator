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

namespace SpreadCalculator
{
    public class Jadro
    {
        private List<ObchodnyDen> listKontrakt1;
        private List<ObchodnyDen> listKontrakt2;
        public List<Spread> listSpread;
        private List<SpecifikaciaKontraktu> listSpecifikacii;

        public Jadro()
        {
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

        internal bool parsujKontrakty(string p1, string p2)
        {
            listKontrakt1 = new List<ObchodnyDen>();
            listKontrakt2 = new List<ObchodnyDen>();

            var succes1 = false;
            var succes2 = false;

            listKontrakt1 = parsujKontrakt(p1,out succes1);
            listKontrakt2 = parsujKontrakt(p2, out succes2);

            listSpread = vypocitajSpread();

            return succes1 && succes2;
        }


        internal bool parsujKontrakty(int p1, int p2)
        {
            listKontrakt1 = new List<ObchodnyDen>();
            listKontrakt2 = new List<ObchodnyDen>();

            var succes1 = false;
            var succes2 = false;

            listKontrakt1 = parsujKontraktXml(p1, out succes1);
            listKontrakt2 = parsujKontraktXml(p2, out succes2);

            listSpread = vypocitajSpread();

            return succes1 && succes2;
        }

        private List<Spread> vypocitajSpread()
        {
            var list = new List<Spread>();
            var indexZaciatkuDlhsiehoSpreadu = listKontrakt2.IndexOf(new ObchodnyDen(listKontrakt1.First().Date));       //  kontrola este z druhej strany
            var dlzka = listKontrakt2.Count - indexZaciatkuDlhsiehoSpreadu;
            if (dlzka > listKontrakt1.Count)
            {
                dlzka = listKontrakt1.Count;
            }

            for (int i = 0; i < dlzka; i++)
            {
                var spread = listKontrakt1[i].Settle-listKontrakt2[i+indexZaciatkuDlhsiehoSpreadu].Settle;
                list.Add(new Spread(spread,listKontrakt1[i].Date));
            }

            return list;
        }

        public List<ObchodnyDen> parsujKontraktXml(int kontrakIndex, out bool succes)
        {
            var list = new List<ObchodnyDen>();
            succes = false;

            string uri = "http://www.quandl.com/api/v1/datasets/OFDP/FUTURE_"+ listSpecifikacii[kontrakIndex].Symbol+".xml?auth_token=UqHLDQVcxZy5AknRTZX9";

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
            }

            if (list != null)
            {
                succes = true;
            }

            return list;
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

    }
}
