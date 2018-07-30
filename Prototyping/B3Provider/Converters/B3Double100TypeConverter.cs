#region License
/*
 * B3Double100TypeConverter.cs
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

    /// <summary>
    /// double base100 double converter to be used by the class that reads flat files
    /// </summary>
    public class B3Double100TypeConverter : ITypeConverter
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
