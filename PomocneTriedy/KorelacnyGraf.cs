using System.Collections.Generic;

namespace SpreadCalculator.PomocneTriedy
{
    public class KorelacnyGraf
    {
        public List<ObchodnyDen> Graf;
        public int Rok;

        public KorelacnyGraf(List<ObchodnyDen> graf, int rok)
        {
            Graf = graf;
            Rok = rok;
        }

        public KorelacnyGraf(List<ObchodnyDen> graf)
        {
            Graf = graf;
        }

        public void SetRok(int rok)
        {
            Rok = rok;
        }
    }
}
