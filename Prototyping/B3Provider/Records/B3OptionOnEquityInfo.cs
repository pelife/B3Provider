#region License
/*
 * B3OptionOnEquityInfo.cs
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

namespace B3Provider.Records
{
    using System;
     

    /// <summary>
    /// 
    /// </summary>
    public class B3OptionOnEquityInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public long? B3ID { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public string ISIN { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public string Ticker { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public double? Strike { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public string StrikeCurrency { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public B3OptionOnEquityStyleInfo Style { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public B3OptionOnEquityTypeInfo Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? Expiration { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Currency { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LoadDate { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public long? B3IDUnderlying { get; internal set; }
    }
}
