using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeAngel.Infrastructure;

namespace PrimeAngel.Perceptions.System
{
    public class PerformanceCounterPerception: IPerception
    {

        public Dictionary<string, string> CounterValues { get; private set; }

        public PerformanceCounterPerception(Dictionary<string, string> counterValues)
        {
            CounterValues = counterValues;
        }

        public string Verbose(IVerboseFormatter formartter)
        {
            return formartter.GetString(this);
        }
    }
}
