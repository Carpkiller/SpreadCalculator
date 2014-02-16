using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
