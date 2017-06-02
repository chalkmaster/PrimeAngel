using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeAngel.Perceptions;

namespace PrimeAngel.Sensors
{
    public interface ISensor
    {
        IList<IPerception> Percept();
    }
}
