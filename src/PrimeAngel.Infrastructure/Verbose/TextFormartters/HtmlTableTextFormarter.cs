using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PrimeAngel.Infrastructure.Verbose.TextFormartters
{
    public class HtmlTableTextFormarter: IVerboseFormatter
    {
        public string GetString(object obj)
        {
            if (obj == null)
                return @"<h1>No elements found!</h1>";

            var sb = new StringBuilder();

            sb.Append("<table border=\"1\">");
            sb.Append(GetTableHeader(obj));
            sb.Append(GetTableBody(obj));
            sb.Append("</table>");

            return sb.ToString();
        }

        public string GetStringForArray(object[] objList)
        {

            if (!objList.Any())
                return @"<h1>No elements found!</h1>";

            var sb = new StringBuilder();

            sb.Append("<table border=\"1\">");
            sb.Append(GetTableHeader(objList.First()));
            sb.Append(GetTableBody(objList));
            sb.Append("</table>");

            return sb.ToString();
        }

        private static string GetTableBody(IEnumerable<object> objList)
        {
            var sb = new StringBuilder();
            foreach (var obj in objList)
            {
                sb.Append(GetTableBody(obj));
            }
            return sb.ToString();
        }

        private static string GetTableBody(object obj)
        {
            var sb = new StringBuilder();
            sb.Append("<tr>");
            for (var i = 0; i < obj.GetType().GetProperties().Length; i++)
            {
                var property = obj.GetType().GetProperties()[i];

                sb.Append("<td>");
                sb.Append(property.GetValue(obj, null));
                sb.Append("</td>");
            }
            sb.Append("</tr>");
            return sb.ToString();
        }


        private static string GetTableHeader(object obj)
        {
            var sb = new StringBuilder();
            sb.Append("<tr>");

            foreach (var propertie in obj.GetType().GetProperties())
            {
                sb.Append("<td><b>");
                sb.Append(propertie.Name);
                sb.Append("</b></td>");
            }

            sb.Append("</tr>");
            return sb.ToString();
        }
    }
}
