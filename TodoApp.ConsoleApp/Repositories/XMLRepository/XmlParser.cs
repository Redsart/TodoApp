using System;
using System.Globalization;
using System.Xml.Linq;

namespace TodoApp.ConsoleApp.Repositories.XMLRepository
{
    internal static class XmlParser
    {
        public static string GetString(XElement element, string propName)
        {
            return element.Element(propName).Value;
        }

        public static DateTime GetDateTime(XElement element, string propName)
        {
            var strValue = GetString(element, propName);
            var date = DateTime.Parse(strValue, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);

            return date;
        }

        public static void SetString(XElement element, string propName, string value)
        {
            var propElement = new XElement(propName, value);
            element.Add(propElement);
        }

        public static void SetDateTime(XElement element, string propName, DateTime value)
        {
            var strValue = value.ToUniversalTime().ToString("o", CultureInfo.InvariantCulture);
            SetString(element, propName, strValue);
        }

        public static Guid GetGuid(XElement element, string propName)
        {
            Guid guid = Guid.Parse(element.Attribute(propName).Value);
            return guid;
        }

        public static void SetGuid(XElement element, string propName, Guid value)
        {
            var strValue = value.ToString();
            var id = new XAttribute(propName, strValue);
            element.Add(id);
        }
    }
}
