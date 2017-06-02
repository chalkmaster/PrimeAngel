using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using PrimeAngel.Perceptions;
using PrimeAngel.Perceptions.System;

namespace PrimeAngel.Sensors.System
{
    public class PerformanceCounterSensor: ISensor
    {
        public List<PerformanceCounter> PerformanceCounters { get; private set; }

        public PerformanceCounterSensor(List<string> countersName, string category)
        {
            PerformanceCounters = new List<PerformanceCounter>();
            countersName.ForEach(s => PerformanceCounters.Add(new PerformanceCounter(category, s, true)));
        }

        public IPerception Percept()
        {
            var counters = new Dictionary<string, string>();
            PerformanceCounters.ForEach(pc =>  counters.Add(pc.CounterName, pc.NextValue().ToString("F")));

            return new PerformanceCounterPerception(counters);
        }

        IList<IPerception> ISensor.Percept()
        {
            throw new NotImplementedException();
        }
    }
}
