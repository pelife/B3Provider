#region License
/*
 * B3DateTypeConverter.cs
 *
 * The MIT License
 *
 * Copyright (c) 2018 Felipe Bahiana Almeida
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 * 
 * Contributors:
 * - Felipe Bahiana Almeida <felipe.almeida@gmail.com> https://www.linkedin.com/in/felipe-almeida-ba222577
 */
#endregion


namespace B3Provider.Converters
{
    using FlatFile.Core;
    using System;
    using System.Globalization;

    /// <summary>
    /// Date time converter to be used by the class that reads flat files
    /// </summary>
    public class B3DateTypeConverter : ITypeConverter
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
                    "yyyyMMdd",
                    "yyyy-MM-dd",
                    "yyyyMMddTHHmmssZ",
                    "yyyyMMddTHHmmZ",
                    "yyyyMMddTHHmmss",
                    "yyyyMMddTHHmm",
                    "yyyyMMddHHmmss",
                    "yyyyMMddHHmm",                    
                    "yyyy-MM-dd-HH-mm-ss",
                    "yyyy-MM-dd-HH-mm",                    
                    "MM-dd-yyyy",
                    "dd-MM-yyyy"
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
