using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SpreadCalculator.Obchody
{
    [Serializable]
    public class UskutocneneObchody
    {
        public List<Obchod> listObchody { get; set; }

        public UskutocneneObchody()
        {
            NacitajObchody();
        }

        public void NacitajObchody()
        {
            listObchody = new List<Obchod>();
            Stream stream = null;
            try
            {
                if (File.Exists("Obchody.bin"))
                {
                    IFormatter formatter = new BinaryFormatter();
                    stream = new FileStream("Obchody.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
                    listObchody = (List<Obchod>)formatter.Deserialize(stream);
                    stream.Close();
                }
            }
            catch (Exception e)
            {
                stream.Close();
            }
        }

        public void UlozObchody()
        {
            Stream stream = null;
            try
            {
                if (!File.Exists("Obchody.bin"))
                {
                    File.Create("Obchody.bin").Close();
                }
                IFormatter formatter = new BinaryFormatter();
                stream = new FileStream("Obchody.bin", FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, listObchody);
                stream.Close();
            }
            catch (Exception e)
            {
                stream.Close();
            }
        }

        public void PridajZaznam(Obchod obchod)
        {
            listObchody.Add(obchod);
        }

        public void OdoberZaznam(int index)
        {
            listObchody.RemoveAt(index);
        }

        public Obchod GetZaznam(int index)
        {
            return listObchody[index];
        }
    }
}
