#region License
/*
 * B3EquityInfoReader.cs
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
 
namespace B3Provider.Readers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Xml;
    using B3Provider.Utils;

    /// <summary>
    /// Method responsible for reading all the equity info from B3 instrument file
    /// </summary>
    public class B3EquityInfoReader : AbstractReader<B3EquityInfo>
    {

        #region "public methods"
        /// <summary>
        /// Method responsible to read records from file
        /// </summary>
        /// <param name="filePath">File to read records from</param>
        /// <returns>
        /// List of all equity instruments found
        /// </returns>
        public override IList<B3EquityInfo> ReadRecords(string filePath)
        {
            var listOfEquity = new List<B3EquityInfo>();

            var temporaryPath = GetRandomTemporaryDirectory();
            var filesToRead = UnzipFile(filePath, temporaryPath);
            if (filesToRead != null && filesToRead.Length > 0)
            {
                foreach (string oneFileToRead in filesToRead)
                {
                    var equityInfo = ReadFile(oneFileToRead);
                    listOfEquity.AddRange(equityInfo);
                }
            }
            
            DeleteDirectory(temporaryPath);
            return listOfEquity;
        }
        #endregion

        #region "private methods"
        /// <summary>
        /// Internal private file responsible to read records of equities from file
        /// </summary>
        /// <param name="filePath">File path to read equity records from</param>
        /// <returns>
        /// All equities found in file
        /// </returns>
        private IList<B3EquityInfo> ReadFile(string filePath)
        {
            IList<B3EquityInfo> equityInfo = null;

            var cultureNumericAmerica = new CultureInfo("en-US");

            var equityInfoDocument = new XmlDocument();
            var relevantEquityInfoQuery = "//instrument:FinInstrmAttrCmon[instrument:Mkt=10 and instrument:Sgmt=1]/..//instrument:EqtyInf[instrument:LastPric>0.00 and not(normalize-space(instrument:CrpnNm)='TAXA DE FINANCIAMENTO')]";

            equityInfoDocument.Load(filePath);

            var rootNode = equityInfoDocument.DocumentElement;
            var nameSpaceManager = new XmlNamespaceManager(equityInfoDocument.NameTable);

            nameSpaceManager.AddNamespace("report", "urn:bvmf.052.01.xsd");
            nameSpaceManager.AddNamespace("instrument", "urn:bvmf.100.02.xsd");

            var dateNode = rootNode.SelectSingleNode("//report:CreDtAndTm[1]", nameSpaceManager);
            var equityNodes = rootNode.SelectNodes(relevantEquityInfoQuery, nameSpaceManager);

            if (equityNodes != null)
            {
                equityInfo = new List<B3EquityInfo>();

                foreach (XmlNode oneEquityNode in equityNodes)
                {
                    var oneEquityInfo = new B3EquityInfo();
                    var noAvo = oneEquityNode.ParentNode?.ParentNode;
                    var noIdInterno = noAvo?.SelectSingleNode("./instrument:FinInstrmId/instrument:OthrId/instrument:Id", nameSpaceManager);
                    var noDescricao = noAvo?.SelectSingleNode("./instrument:FinInstrmAttrCmon/instrument:Desc", nameSpaceManager);

                    oneEquityInfo.B3ID = noIdInterno.InnerText.ToNullable<long>();
                    oneEquityInfo.ISIN = oneEquityNode["ISIN"].InnerText;
                    oneEquityInfo.Description = oneEquityNode["CrpnNm"].InnerText.RemoveMultipleSpaces();
                    oneEquityInfo.CompanyName = noDescricao.InnerText.RemoveMultipleSpaces().Trim();
                    oneEquityInfo.Ticker = oneEquityNode["TckrSymb"].InnerText;
                    oneEquityInfo.Currency = oneEquityNode["TradgCcy"].InnerText;
                    oneEquityInfo.MarketCapitalization = oneEquityNode["MktCptlstn"].InnerText.ToNullable<long>();
                    oneEquityInfo.LastPrice = oneEquityNode["LastPric"].InnerText.ToNullable<double>(cultureNumericAmerica);
                    oneEquityInfo.LoadDate = dateNode.InnerText.ToNullable<DateTime>();

                    equityInfo.Add(oneEquityInfo);
                }
            }

            return equityInfo;
        }
        #endregion
    }
}
