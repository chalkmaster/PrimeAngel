using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeAngel.Infrastructure;

namespace PrimeAngel.Perceptions.System
{
    public class TimePerception: IPerception
    {
        public DateTime RawDateTime { get; private set; }
        public DayOfWeek DayOfWeek { get; private set; }
        public int Month { get; private set; }        
        public int Hour { get; private set; }
        public string DayPart { get; private set; }

        public TimePerception(DateTime timePerception)
        {
            RawDateTime = timePerception;

            Decompose(RawDateTime);
        }

        private void Decompose(DateTime dateToBeDecomposed)
        {
            DayOfWeek = dateToBeDecomposed.DayOfWeek;
            Month = dateToBeDecomposed.Month;
            Hour = dateToBeDecomposed.Hour;
            DayPart = dateToBeDecomposed.ToString("tt");
        }

        public override string ToString()
        {
            const string pattern = "Data e Hora: {0};\nDia da Semana: {1};\nMês {2}; Hora: {3}{4};";
            return string.Format(pattern, RawDateTime.ToShortDateString(), DayOfWeek, Month, Hour, DayPart);
        }

        public string Verbose(IVerboseFormatter formartter)
        {
            return formartter.GetString(this);
        }
    }
}
