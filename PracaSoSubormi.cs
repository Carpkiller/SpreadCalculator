using System.Globalization;
using CsvHelper;
using SpreadCalculator.PomocneTriedy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SpreadCalculator
{
    public class PracaSoSubormi
    {

        public List<string> GetKontrakty()
        {
            List<string> list = new List<string>();
            list.Add("----------");
            string path = System.Environment.CurrentDirectory;
            string path2 = path.Substring(0, path.LastIndexOf("bin")) + "Kontrakty";
            var files = Directory.GetFiles(path2);
            foreach (var item in files)
            {
                var komodita = item.Substring(0,item.IndexOf(".csv")).Remove(0, item.LastIndexOf("Kontrakty")+10);
                list.Add(komodita);
            }
            return list;
        }

        public List<SirsiaSpecifikaciaKontraktu> GetKontraktyPodrobnejsie()
        {
            List<SirsiaSpecifikaciaKontraktu> list = new List<SirsiaSpecifikaciaKontraktu>();

            var file = System.Environment.CurrentDirectory.Substring(0, System.Environment.CurrentDirectory.LastIndexOf("bin")) + "Kontrakty\\Specifikacie.csv";

            if (File.Exists(file))
            {
                try
                {
                    var csv = new CsvReader(new StreamReader(file));
                    csv.Configuration.HasHeaderRecord = true;
                    csv.Configuration.IgnoreHeaderWhiteSpace = true;
                    csv.Configuration.Delimiter = ";";
                    csv.Configuration.CultureInfo = CultureInfo.GetCultureInfoByIetfLanguageTag("sk-SK");
                    //csv.Configuration.IgnoreReadingExceptions = true;
                    var myCustomObjects = csv.GetRecords<SirsiaSpecifikaciaKontraktu>();

                    foreach (var item in myCustomObjects)
                    {
                        list.Add(item);
                    }
                }
                catch (Exception e)
                {
                    var ee = e.Data["CsvHelper"];
                     Console.WriteLine(e.Data.Values);
                }

            }

            return list;
        }


        internal static bool SkontrolujSubor(string kontrakt)
        {
            var co = File.Exists(System.Environment.CurrentDirectory.Substring(0, System.Environment.CurrentDirectory.LastIndexOf("bin")) + "Download\\" + kontrakt + ".xml");
            return File.Exists(System.Environment.CurrentDirectory.Substring(0, System.Environment.CurrentDirectory.LastIndexOf("bin")) + "Download\\" + kontrakt + ".xml");
        }

        internal static void UlozAktualnyList(List<ObchodnyDen> list, string komodita)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<ObchodnyDen>));
            TextWriter textWriter = new StreamWriter(System.Environment.CurrentDirectory.Substring(0, System.Environment.CurrentDirectory.LastIndexOf("bin")) + "Download\\"+komodita+".xml");
            serializer.Serialize(textWriter, list);
            textWriter.Close();
        }

        internal static List<ObchodnyDen> NahrajData(string komodita)
        {
            var list = new List<ObchodnyDen>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<ObchodnyDen>));
            TextReader textReader = new StreamReader(System.Environment.CurrentDirectory.Substring(0, System.Environment.CurrentDirectory.LastIndexOf("bin")) + "Download\\" + komodita + ".xml");
            list = (List<ObchodnyDen>) serializer.Deserialize(textReader);
            textReader.Close();
            return list;
        }
    }
}
