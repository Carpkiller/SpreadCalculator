using System;
using System.Collections.Generic;
using SpreadCalculator.PomocneTriedy;

namespace SpreadCalculator.Statistiky
{
    public static class Testy
    {

        public static string PocitajStatistiky(List<List<Spread>> list, int dlzkaOdKonca)
        {
            var result = "";
            foreach (var rok in list)
            {
                result += "Koniec spreadu " + rok[0].Date + "  na hodnote " + rok[0].Value + Environment.NewLine;
                for (int i = 0; i < 5; i++)
                {
                    result += rok[dlzkaOdKonca + i].Date + "  " + rok[dlzkaOdKonca + i].Value + Environment.NewLine;
                }
                result += "---------------------" + Environment.NewLine + Environment.NewLine;
            }
            return result;
        }
    }
}
