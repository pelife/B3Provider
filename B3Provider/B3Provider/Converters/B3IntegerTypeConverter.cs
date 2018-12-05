#region License
/*
 * B3IntegerTypeConverter.cs
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
    /// converts one integer but if string is empty returns 0
    /// </summary>
    public class B3IntegerTypeConverter : ITypeConverter
    {
        public bool CanConvertFrom(Type type)
        {
            bool ret = typeof(String) == type;
            return ret;
        }

        public bool CanConvertTo(Type type)
        {
            bool ret = typeof(System.Int32) == type;
            return ret;
        }

        public object ConvertFromString(string source)
        {
            int intAux = 0;
            if (!string.IsNullOrEmpty(source))
                int.TryParse(source, out intAux);
                
            return intAux;
        }

        public string ConvertToString(object source)
        {
            string intFormated = string.Empty;
            int? oldInt = (int?)source;
            if (oldInt.HasValue)
                intFormated = oldInt.Value.ToString();

            return intFormated;
        }
    }
}
