using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace SpreadCalculator.PomocneTriedy
{
    sealed class ObchodnyDenMap : CsvClassMap<ObchodnyDen>
    {
        public override void CreateMap()
        {
            Map(m => m.Date);
            Map(m => m.Open);
            Map(m => m.High);
            Map(m => m.Low);
            Map(m => m.Settle);
            Map(m => m.Volume);
            Map(m => m.OpenInterest).Name("Open Interest");
        }
    }
}
