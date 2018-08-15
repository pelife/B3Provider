#region License
/*
 * ConverterUtil.cs
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

namespace B3Provider.Utils
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using System.Threading;

    public static class ConverterUtil
    {
        //reference implementation from: Joel Coehoorn @jcoehoorn
        //https://stackoverflow.com/questions/773078/convert-string-to-nullable-type-int-double-etc

        public static Nullable<T> ToNullable<T>(this string s) where T : struct
        {
            Nullable<T> result = new Nullable<T>();
            try
            {
                if (!string.IsNullOrEmpty(s) && s.Trim().Length > 0)
                {
                    TypeConverter conv = TypeDescriptor.GetConverter(typeof(T));
                    result = (T)conv.ConvertFrom(s);
                }
            }
            catch { }
            return result;
        }

        public static Nullable<T> ToNullable<T>(this string s, CultureInfo cultureToSet) where T : struct
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = cultureToSet;
            var result = s.ToNullable<T>();
            Thread.CurrentThread.CurrentCulture = currentCulture;
            return result;
        }

        public static string RemoveMultipleSpaces(this string s)
        {
            //Sheridan - Patrick Desjardins
            //https://stackoverflow.com/users/249281/sheridan
            //https://stackoverflow.com/users/13913/patrick-desjardins
            //https://stackoverflow.com/questions/206717/how-do-i-replace-multiple-spaces-with-a-single-space-in-c
            if (!string.IsNullOrEmpty(s))
            {
                RegexOptions options = RegexOptions.None;
                Regex regex = new Regex("[ ]{2,}", options);
                s = regex.Replace(s, " ");
            }

            return s;
            
        }

        
    }
}
