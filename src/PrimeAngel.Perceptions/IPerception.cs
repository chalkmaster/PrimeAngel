using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeAngel.Infrastructure;

namespace PrimeAngel.Perceptions
{
    public interface IPerception
    {
        string Verbose(IVerboseFormatter formartter);
    }
}
