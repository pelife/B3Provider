using FlatFile.Core;
using System;

namespace Prototyping.Code.Download.MarketData.Bovespa.Converter
{
    public class BDIStringTypeConverter : ITypeConverter
    {
        public bool CanConvertFrom(Type type)
        {
            bool ret = typeof(String) == type;
            return ret;
        }

        public bool CanConvertTo(Type type)
        {
            bool ret = typeof(System.String) == type;
            return ret;
        }

        public object ConvertFromString(string source)
        {
            string newDate = string.Empty;
            if (!string.IsNullOrEmpty(source))
                newDate = source.Trim();
            return newDate;
        }

        public string ConvertToString(object source)
        {
            string dateFormated = string.Empty;
            string oldDate = (string)source;

            if (!string.IsNullOrEmpty(oldDate))
                dateFormated = oldDate.Trim();

            return dateFormated;
        }
    }
}
