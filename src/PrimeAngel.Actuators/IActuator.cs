using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeAngel.Perceptions;
using PrimeAngel.PersistentMemory;

namespace PrimeAngel.Actuators
{
    public interface IActuator
    {
        void Do(IList<IPerception> perceptions);
    }
}
