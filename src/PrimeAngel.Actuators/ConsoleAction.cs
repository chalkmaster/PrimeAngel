using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeAngel.Infrastructure.Extensions;

namespace PrimeAngel.Actuators
{
    public class ConsoleAction: IAction
    {
        private IList<Perceptions.IPerception> _perceptions;

        public ConsoleAction(IList<Perceptions.IPerception> perceptions)
        {
            _perceptions = perceptions;
        }
        public ActionResult Act()
        {
            _perceptions.ToList().ForEach(p => Console.WriteLine(p.Stringify()));
            return new ActionResult {Action = this, Message = "ok", Score = 0, Success = true};
        }
    }
}
