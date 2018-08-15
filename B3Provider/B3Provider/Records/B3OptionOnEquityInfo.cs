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
    /// Class that represents a option on stock (equity)
    /// </summary>
    public class B3OptionOnEquityInfo
    {
        /// <summary>
        /// B3 Internal identification of the instrument
        /// </summary>
        public long? B3ID { get; set; }

        /// <summary>
        /// ISIN world public instrument identification
        /// </summary>
        public string ISIN { get; set; }

        /// <summary>
        /// Description of the instrument
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Ticker of most common refered to symbol of the option
        /// </summary>
        public string Ticker { get; set; }

        /// <summary>
        /// Strike price of the option
        /// </summary>
        public double? Strike { get; set; }

        /// <summary>
        /// Currency in which strike price is expressed
        /// </summary>
        public string StrikeCurrency { get; set; }

        /// <summary>
        /// Style of the option American or European
        /// European option may be exercised only at the expiration date
        /// American option on the other hand may be exercised at any time before the expiration date
        /// </summary>
        public B3OptionOnEquityStyleInfo Style { get; set; }

        /// <summary>
        /// Typw of the option Call or Put
        /// Call provide the holder the right (but not the obligation) to purchase an underlying asset at a specified price (the strike price)
        /// Put give the holder the right to sell an underlying asset at a specified price (the strike price)        
        /// </summary>
        public B3OptionOnEquityTypeInfo Type { get; set; }

        /// <summary>
        /// Expiration date of the option
        /// </summary>
        public DateTime? Expiration { get; set; }

        /// <summary>
        /// Currency in which price of the option is expressed
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// The date of the last instrument file read
        /// </summary>
        public DateTime? LoadDate { get; set; }

        /// <summary>
        /// B3 Internal identification of the underlying instrument
        /// </summary>
        public long? B3IDUnderlying { get; set; }
    }
}
