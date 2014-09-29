using System.Collections.Generic;

namespace SpreadCalculator.PomocneTriedy
{
    public class KorelacnySpread
    {
        public List<Spread> Spread;
        public int Rok;

        public KorelacnySpread(List<Spread> spread, int rok)
        {
            Spread = spread;
            Rok = rok;
        }

        public KorelacnySpread(List<Spread> spread)
        {
            Spread = spread;
        }

        public void SetRok(int rok)
        {
            Rok = rok;
        }
    }
}
