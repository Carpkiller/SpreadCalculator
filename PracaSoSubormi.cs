using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using CsvHelper;
using SpreadCalculator.PomocneTriedy;
using System;
using System.Collections.Generic;
using System.IO;
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
                var reader = new StreamReader(file);
                try
                {
                    var csv = new CsvReader(reader);
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
                finally
                {
                    reader.Close();
                }
            }

            return list;
        }


        public List<int> PocetStiahnutychKontraktov(List<SirsiaSpecifikaciaKontraktu> kontrakty)
        {
            var list = new List<int>();

            foreach (var kontrakt in kontrakty)
            {
                var search = "*" + kontrakt.Symbol + "*";
                string[] filePaths =
                    Directory.GetFiles(
                        @System.Environment.CurrentDirectory.Substring(0,
                            System.Environment.CurrentDirectory.LastIndexOf("bin")) + "Download\\", search);
                
                search = kontrakt.Symbol + @"[A-Z]{1}[0-9]{4}";
                list.Add(filePaths.Count(d => Regex.IsMatch(d, search,RegexOptions.Compiled)));
            }

            return list;
        }

        internal static bool SkontrolujSubor(string kontrakt)
        {
            return File.Exists(System.Environment.CurrentDirectory.Substring(0, System.Environment.CurrentDirectory.LastIndexOf("bin", StringComparison.Ordinal)) + "Download\\" + kontrakt + ".xml");
        }

        internal static void UlozAktualnyList(List<ObchodnyDen> list, string komodita)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<ObchodnyDen>));
            TextWriter textWriter = new StreamWriter(System.Environment.CurrentDirectory.Substring(0, System.Environment.CurrentDirectory.LastIndexOf("bin", StringComparison.Ordinal)) + "Download\\"+komodita+".xml");
            serializer.Serialize(textWriter, list);
            textWriter.Close();
        }

        internal static List<ObchodnyDen> NahrajData(string komodita)
        {
            var list = new List<ObchodnyDen>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<ObchodnyDen>));
            TextReader textReader = new StreamReader(System.Environment.CurrentDirectory.Substring(0, System.Environment.CurrentDirectory.LastIndexOf("bin", StringComparison.Ordinal)) + "Download\\" + komodita + ".xml");
            list = (List<ObchodnyDen>) serializer.Deserialize(textReader);
            textReader.Close();
            return list;
        }

        public static void UlozDocasnyAktualnyList(List<ObchodnyDen> list, string komodita)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<ObchodnyDen>));
            TextWriter textWriter = new StreamWriter(System.Environment.CurrentDirectory.Substring(0, System.Environment.CurrentDirectory.LastIndexOf("bin", StringComparison.Ordinal)) + "Docasne subory\\" + komodita + ".xml");
            serializer.Serialize(textWriter, list);
            textWriter.Close();
        }

        public static bool SkontrolujDocasnySubor(string kontrakt)
        {
            return File.Exists(System.Environment.CurrentDirectory.Substring(0, System.Environment.CurrentDirectory.LastIndexOf("bin")) + "Docasne subory\\" + kontrakt + ".xml");
        }

        public static List<ObchodnyDen> NahrajDocasneData(string komodita)
        {
            var list = new List<ObchodnyDen>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<ObchodnyDen>));
            TextReader textReader = new StreamReader(System.Environment.CurrentDirectory.Substring(0, System.Environment.CurrentDirectory.LastIndexOf("bin")) + "Docasne subory\\" + komodita + ".xml");
            list = (List<ObchodnyDen>)serializer.Deserialize(textReader);
            textReader.Close();
            return list;
        }

        public static void OcekujStareSubory()
        {
            try
            {
                var files = Directory.GetFiles(Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.LastIndexOf("bin", StringComparison.Ordinal)) + "Docasne subory");

                foreach (var file in files)
                    if (File.GetCreationTime(file).Date < DateTime.Today)
                        File.Delete(file);
            }
            catch (Exception)
            {
                var path = System.Environment.CurrentDirectory.Substring(0, System.Environment.CurrentDirectory.LastIndexOf("bin")) + "Docasne subory";
                Directory.CreateDirectory(path);
            }
            
        }

        public void UlozZmenySpecifikacii(SirsiaSpecifikaciaKontraktu specikacia, int selectedIndex)
        {
            var file = System.Environment.CurrentDirectory.Substring(0, System.Environment.CurrentDirectory.LastIndexOf("bin")) + "Kontrakty\\Specifikacie.csv";
            var records = GetKontraktyPodrobnejsie();
            File.Delete(file);
            //File.Create(file);

            //if (File.Exists(file)){
            using (StreamWriter sw = new StreamWriter(file, true))
            {
                try
                {
                    var writer = new CsvWriter(sw);
                    writer.Configuration.Delimiter = ";";
                    writer.WriteHeader(typeof (SirsiaSpecifikaciaKontraktu));
                    for (int i = 0; i < selectedIndex; i++)
                    {
                        writer.WriteRecord(records[i]);
                    }
                    writer.WriteRecord(specikacia);
                    for (int i = selectedIndex + 1; i < records.Count; i++)
                    {
                        writer.WriteRecord(records[i]);
                    }
                    sw.Close();
                }
                catch (Exception e)
                {
                    var ee = e.Data["CsvHelper"];
                    Console.WriteLine(e.Data.Values);
                }
                // }
            }
        }

        public static int ZmazDocasneSubory()
        {
            var files = Directory.GetFiles(Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.LastIndexOf("bin", StringComparison.Ordinal)) + "Docasne subory");
            int poc = 0;

            foreach (var file in files)
            {
                File.Delete(file);
                poc++;
            }

            return poc;
        }
    }
}
