#region License
/*
 * B3SectorClassifcationInfo.cs
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
    /// <summary>
    /// Class that represents the Economic Market Sector of a company listed in
    /// B3 stock exchange (former BM&F Bovespa) Brazil.
    /// </summary>
    public class B3SectorClassifcationInfo
    {
        /// <summary>
        /// Hash of the classification to be used as a key
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 1st level of classification 
        /// Examples
        /// Bens Industriais
        /// Materiais Básicos
        /// </summary>
        public string EconomicSector { get; internal set; }

        /// <summary>
        /// 2nd level of classification 
        /// Examples
        /// Bens Industriais
        ///     Construção e Engenharia
        /// Materiais Básicos
        ///     Mineração
        /// </summary>
        public string EconomicSubSector { get; internal set; }

        /// <summary>
        /// 3rd level of classification 
        /// Examples
        /// Bens Industriais
        ///     Construção e Engenharia
        ///         Produtos para Construção
        ///         Construção Pesada
        ///         Engenharia Consultiva
        /// Materiais Básicos
        ///     Mineração
        ///         Minerais Metálicos
        ///         Minerais Não Metálicos
        /// </summary>
        public string EconomicSegment { get; internal set; }

        /// <summary>
        /// Name of the company classificated
        /// </summary>
        public string CompanyName { get; internal set; }

        /// <summary>
        /// Listing code of the company classificated
        /// </summary>
        public string CompanyListingCode { get; internal set; }

        /// <summary>
        /// Listing segment of the company classificated
        /// </summary>
        public string CompanyListingSegment { get; internal set; }
    }
}
