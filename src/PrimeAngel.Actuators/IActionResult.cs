using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeAngel.Actuators
{
    public class ActionResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int Score { get; set; }
        public IAction Action { get; set; }
    }
}
