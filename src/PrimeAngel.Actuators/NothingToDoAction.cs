﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeAngel.Actuators
{
    public class NothingToDoAction: IAction
    {
        public ActionResult Act()
        {
            return new ActionResult();
        }
    }
}
