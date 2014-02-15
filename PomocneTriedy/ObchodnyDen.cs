using System;

namespace SpreadCalculator.PomocneTriedy
{
    public class ObchodnyDen : IEquatable<ObchodnyDen>
    {
        public DateTime Date { get; set; }
        public Double Open { get; set; }
        public Double High { get; set; }
        public Double Low { get; set; }
        public Double Settle { get; set; }
        public Double Volume { get; set; }
        public Double OpenInterest { get; set; }

        public ObchodnyDen(){}

        public ObchodnyDen(DateTime date)
        {
            Date = date;
        }

        public ObchodnyDen(DateTime date, double open, double high, double low, double settle, double volume, double openInterest)
        {
            Date = date;
            Open = open;
            High = high;
            Low = low;
            Settle = settle;
            Volume = volume;
            OpenInterest = openInterest;
        }

        public bool Equals(ObchodnyDen other)
        {
            if (this.Date == other.Date)
                return true;
            
            return false;
        }
    }
}
