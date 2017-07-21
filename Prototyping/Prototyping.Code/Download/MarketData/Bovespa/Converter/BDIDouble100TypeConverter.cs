using FlatFile.Core;
using System;

namespace Prototyping.Code.Download.MarketData.Bovespa.Converter
{
    public class BDIDouble100TypeConverter : ITypeConverter
    {
        public bool CanConvertFrom(Type type)
        {
            bool ret = typeof(String) == type;
            return ret;
        }

        public bool CanConvertTo(Type type)
        {
            bool ret = typeof(System.Double) == type;
            return ret;
        }

        public object ConvertFromString(string source)
        {
            double? newDouble = null;
            double doubleAux = 0.0;
            if (!string.IsNullOrEmpty(source))
                if (double.TryParse(source, out doubleAux))
                {
                    doubleAux = doubleAux / 100;
                    newDouble = doubleAux;
                }
            return newDouble;
        }

        public string ConvertToString(object source)
        {
            string doubleFormated = string.Empty;
            double? oldDouble = (double?)source;
            if (oldDouble.HasValue)
                doubleFormated = oldDouble.Value.ToString("N2");

            return doubleFormated;
        }
    }
}
