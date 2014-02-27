using System;

namespace SpreadCalculator.PomocneTriedy
{
    public class Spread
    {
        public double Value { get; set; }
        public DateTime Date { get; set; }

        public Spread(double value, DateTime date)
        {
            Value = value;
            Date = date;
        }

        public override bool Equals(object obj)
        {
            return this.Date == ((Spread) (obj)).Date;
        }
    }
}
