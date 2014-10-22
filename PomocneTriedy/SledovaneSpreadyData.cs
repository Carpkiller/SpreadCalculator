using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SpreadCalculator.PomocneTriedy
{
    [Serializable]
    public class SledovaneSpreadyData
    {
        private List<SledovanySpread> listData;

        public SledovaneSpreadyData()
        {
            NacitajData();
        }

        public void NacitajData()
        {
            listData = new List<SledovanySpread>();
            Stream stream = null;
            try
            {
                IFormatter formatter = new BinaryFormatter();
                stream = new FileStream("SledovaneSpready.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
                listData = (List<SledovanySpread>) formatter.Deserialize(stream);
                stream.Close();
            }
            catch (Exception e)
            {
                stream.Close();
            }
        }

        public void UlozData()
        {
            Stream stream = null;
            try
            {
                IFormatter formatter = new BinaryFormatter();
                stream = new FileStream("SledovaneSpready.bin", FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, listData);
                stream.Close();
            }
            catch (Exception e)
            {
                stream.Close();
            }

        }

        public void PridajZaznam(SledovanySpread spread)
        {
            listData.Add(spread);
        }

        public void OdoberZaznam(int index)
        {
            listData.RemoveAt(index);
        }

        public SledovanySpread GetZaznam(int index)
        {
            return listData[index];
        }

        public List<string> PopisSpreadov()
        {
            var listKont = new PracaSoSubormi().GetKontraktyPodrobnejsie();
            List<string> list = new List<string>();

            foreach (var sledovanySpread in listData)
            {
                list.Add(listKont[sledovanySpread.komodita1 - 1].Symbol + sledovanySpread.kontrakt1[0] +
                         sledovanySpread.rok1.Substring(2, 2) + " - " + listKont[sledovanySpread.komodita2 - 1].Symbol +
                         sledovanySpread.kontrakt2[0] + sledovanySpread.rok2.Substring(2, 2));
            }

            return list;
        }

        public void OdstranZaznam(int selectedIndex)
        {
            listData.RemoveAt(selectedIndex);
        }
    }
}
