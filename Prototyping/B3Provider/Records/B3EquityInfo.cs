#region License
/*
 * B3EquityInfo.cs
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
    /// Class that represents a stock (equity)
    /// </summary>
    public class B3EquityInfo
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
        /// Name of issuer company
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Description of the instrument
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Ticker of most common refered to symbol of the stock
        /// </summary>
        public string Ticker { get; set; }

        /// <summary>
        /// Trading currency of the stock
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Market capitalization of the company (size of the company)
        /// </summary>
        public long? MarketCapitalization { get; set; }

        /// <summary>
        /// Last price that was presented into the instrument file (not quote)
        /// </summary>
        public double? LastPrice { get; set; }

        /// <summary>
        /// The date of the last instrument file read
        /// </summary>
        public DateTime? LoadDate { get; set; }

        /// <summary>
        /// Company Market sector classification
        /// </summary>
        public B3SectorClassifcationInfo SectorClassification { get; set; }
    }
}
