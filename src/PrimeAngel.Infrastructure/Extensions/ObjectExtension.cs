using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PrimeAngel.Infrastructure.Extensions
{
    public static class ObjectExtension
    {

        public static string Stringify(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static string GetObjectPropertiesAsString(this object obj)
        {
            if (obj == null)
                return string.Empty;

            if (obj.GetType().IsArray)
            {
                var info = new StringBuilder();
                foreach (var item in (Array) obj)
                    info.AppendLine(GetPropertiesAsString(item));

                return info.ToString();
            }

            return GetPropertiesAsString(obj);
        }

        private static string GetPropertiesAsString(object obj)
        {
            var info = new StringBuilder();
            obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty)
                        .Where(p => p.CanRead && (p.PropertyType.IsPrimitive || p.PropertyType == typeof(Decimal)
                            || p.PropertyType == typeof(String) || p.PropertyType.IsArray))
                        .ToList().ForEach(p => info.Append(p.PropertyType.IsArray
                                                      ? p.GetValue(obj, null).GetObjectPropertiesAsString()
                                                      : string.Format("{0}: {1}; ", p.Name, p.GetValue(obj, null) != null
                                                      ? p.GetValue(obj, null).ToString() : string.Empty))
                                );
            return info.ToString();
        }
    }
}
