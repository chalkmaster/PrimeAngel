using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeAngel.Perceptions;
using PrimeAngel.PersistentMemory;

namespace PrimeAngel.Actuators
{
    public abstract class Actuator: IActuator
    {
        public void Do(IList<IPerception> perceptions)
        {
            var actionList = FindActionList(perceptions);
            var actionsResult = Act(actionList);
            Learn(actionsResult, new MainMemory());
        }

        protected abstract IList<IAction> FindActionList(IList<IPerception> perceptions);
        protected abstract IList<ActionResult> Act(IList<IAction> actions);
        protected abstract void Learn(IList<ActionResult> toEvaluate, IMemory mainMemory);
    }
}
