using System;
using PrimeAngel.Perceptions;
using PrimeAngel.Perceptions.System;
using System.Collections.Generic;

namespace PrimeAngel.Sensors.System
{
    public class TimeSensor: ISensor
    {
        public IList<IPerception> Percept()
        {
            return new List<IPerception> {new TimePerception(DateTime.Now)};
        }
    }
}
