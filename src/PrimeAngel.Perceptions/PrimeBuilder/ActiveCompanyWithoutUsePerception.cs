using System;

namespace PrimeAngel.Perceptions.OSMobile
{
    public class ActiveCompanyWithoutUsePerception: IPerception
    {
        public long CompanyId { get; set; }
        public long CompanyName { get; set; }
        public int ActiveUsers { get; set; }
        public DateTime LastUseDate { get; set; }
        public double IdleDays { get { return DateTime.Now.Subtract(LastUseDate).TotalDays; } }
    }

}
