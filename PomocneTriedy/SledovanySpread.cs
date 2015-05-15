using System;

namespace SpreadCalculator.PomocneTriedy
{
    [Serializable]
    public class SledovanySpread
    {
        public int komodita1;
        public string kontrakt1;
        public string rok1;
        public int komodita2;
        public string kontrakt2;
        public string rok2;

        public SledovanySpread(int komodita1, string kontrakt1, string rok1, int komodita2, string kontrakt2, string rok2)
        {
            this.komodita1 = komodita1;
            this.kontrakt1 = kontrakt1;
            this.rok1 = rok1;
            this.komodita2 = komodita2;
            this.kontrakt2 = kontrakt2;
            this.rok2 = rok2;
        }

        public override string ToString()
        {
            return "";
        }
    }
}
