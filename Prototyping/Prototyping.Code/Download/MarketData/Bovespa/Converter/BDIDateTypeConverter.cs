using FlatFile.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototyping.Code.Download.MarketData.Bovespa.Converter
{
    public class BDIDateTypeConverter : ITypeConverter
    {
        public bool CanConvertFrom(Type type)
        {
            bool ret = typeof(String) == type;
            return ret;
        }

        public bool CanConvertTo(Type type)
        {
            bool ret = typeof(System.DateTime) == type;
            return ret;
        }

        public object ConvertFromString(string source)
        {
            DateTime? newDate = null;
            newDate = ParseDateTime(source);
            return newDate;
        }

        public string ConvertToString(object source)
        {
            string dateFormated = string.Empty;
            DateTime? oldDate = (System.DateTime?)source;

            if (oldDate.HasValue)
                dateFormated = oldDate.Value.ToString("yyyyMMdd");

            return dateFormated;
        }

        private DateTime? ParseDateTime(
            string dateToParse,
            string[] formats = null,
            IFormatProvider provider = null,
            DateTimeStyles styles = DateTimeStyles.AssumeLocal)
        {
            string[] CUSTOM_DATE_FORMATS = new string[]
                {
                    "yyyyMMddTHHmmssZ",
                    "yyyyMMddTHHmmZ",
                    "yyyyMMddTHHmmss",
                    "yyyyMMddTHHmm",
                    "yyyyMMddHHmmss",
                    "yyyyMMddHHmm",
                    "yyyyMMdd",
                    "yyyy-MM-dd-HH-mm-ss",
                    "yyyy-MM-dd-HH-mm",
                    "yyyy-MM-dd",
                    "MM-dd-yyyy"
                };

            if (formats == null)
            {
                formats = CUSTOM_DATE_FORMATS;
            }

            DateTime validDate;

            foreach (var format in formats)
            {
                if (format.EndsWith("Z"))
                {
                    if (DateTime.TryParseExact(dateToParse, format,
                             provider,
                             DateTimeStyles.AssumeUniversal,
                             out validDate))
                    {
                        return validDate;
                    }
                }
                else
                {
                    if (DateTime.TryParseExact(dateToParse, format,
                             provider, styles, out validDate))
                    {
                        return validDate;
                    }
                }
            }

            return null;
        }
    }
}
