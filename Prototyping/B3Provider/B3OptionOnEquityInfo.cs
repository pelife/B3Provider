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

using System;

namespace B3Provider
{
    /// <summary>
    /// Enumeration that indicates the type of Option Call or Put
    /// </summary>
    public enum B3OptionOnEquityTypeInfo : short
    {
        /// <summary>
        /// Call option is an obligation to sell at a price to call issuer.
        /// </summary>
        Call = 1

       /// <summary>
       /// Put option is an obligation to buy at a price to call issuer.
       /// </summary>
       , Put = -1
    }

    /// <summary>
    /// Enumeration that indicates the Exercise Style of Option American or European
    /// </summary>
    public enum B3OptionOnEquityStyleInfo : short
    {
        /// <summary>
        /// Can be exercised any time prior to the expiration date
        /// </summary>
        American = 1

       /// <summary>
       /// Can be exercised only in the expiration date
       /// </summary>
       , European = 2
    }

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
