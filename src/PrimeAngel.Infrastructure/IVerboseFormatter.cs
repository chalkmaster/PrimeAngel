using System;
using System.Collections.Generic;
using System.Security;

namespace PrimeAngel.Infrastructure
{
    public interface IVerboseFormatter
    {
        string GetString(object obj);
        string GetStringForArray(object[] obj);
    }
}
