using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PrimeAngel.Infrastructure;
using PrimeAngel.Infrastructure.Extensions;

namespace PrimeAngel.Perceptions.PrimeBuilder
{
    public class ActiveInstancesWithoutUsePerception: IPerception
    {
        public IList<InstanceData> Instances { get; private set; }

        public ActiveInstancesWithoutUsePerception()
        {
            Instances = new List<InstanceData>();
        }

        public class InstanceData
        {
            public long InstanceId { get; set; }
            public string InstanceName { get; set; }
            public int WorkingUsers { get; set; }
            public int CreatedTasks { get; set; }
            public int TotalActiveUsers { get; set; }
            public int TotalActiveWebUsers { get; set; }
            public int TotalActiveDeviceUsers { get; set; }
            public DateTime LastUseDate { get; set; }
            public double IdleDays { get { return DateTime.Now.Subtract(LastUseDate).TotalDays; } }       
        }

        public override string ToString()
        {
            return this.Stringify();
        }

        public string Verbose(IVerboseFormatter formartter)
        {
            return formartter.GetStringForArray(Instances.ToArray());
        }

    }

}
