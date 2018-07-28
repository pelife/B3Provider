#region License
/*
 * B3OptionOnEquityInfoReader.cs
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
    using B3Provider.Util;

    /// <summary>
    /// Method responsible for reading all the options on equity info from B3 instrument file
    /// </summary>
    public class B3OptionOnEquityInfoReader : AbstractReader<B3OptionOnEquityInfo>
    {
        #region "public methods"
        /// <summary>
        /// Method responsible to read records from file
        /// </summary>
        /// <param name="filePath">File to read records from</param>
        /// <returns>
        /// List of all options on equity instruments found
        /// </returns>
        public override IList<B3OptionOnEquityInfo> ReadRecords(string filePath)
        {
            var listOfOptionsOnEquity = new List<B3OptionOnEquityInfo>();

            var temporaryPath = GetRandomTemporaryDirectory();
            var filesToRead = UnzipFile(filePath, temporaryPath);
            if (filesToRead != null && filesToRead.Length > 0)
            {
                foreach (string oneFileToRead in filesToRead)
                {
                    var optionsOnEquityInfo = ReadFile(oneFileToRead);
                    listOfOptionsOnEquity.AddRange(optionsOnEquityInfo);
                }
            }

            DeleteDirectory(temporaryPath);
            return listOfOptionsOnEquity;
        }
        #endregion

        #region "private methods"
        /// <summary>
        /// Internal private file responsible to read records of options on equities from file
        /// </summary>
        /// <param name="filePath">File path to read equity records from</param>
        /// <returns>
        /// All options on equities found in file
        /// </returns>
        private IList<B3OptionOnEquityInfo> ReadFile(string filePath)
        {
            IList<B3OptionOnEquityInfo> optionsInfo = null;
            var cultureNumericAmerica = new CultureInfo("en-US");

            var optionsInfoDocument = new XmlDocument();
            var relevantOptionsInfoQuery = "//instrument:FinInstrmAttrCmon[(instrument:Mkt=70 or instrument:Mkt=80) and instrument:Sgmt=2]/..//instrument:OptnOnEqtsInf";

            optionsInfoDocument.Load(filePath);

            var rootNode = optionsInfoDocument.DocumentElement;
            var nameSpaceManager = new XmlNamespaceManager(optionsInfoDocument.NameTable);

            nameSpaceManager.AddNamespace("report", "urn:bvmf.052.01.xsd");
            nameSpaceManager.AddNamespace("instrument", "urn:bvmf.100.02.xsd");

            var dateNode = rootNode.SelectSingleNode("//report:CreDtAndTm[1]", nameSpaceManager);
            var optionOnEquityNodes = rootNode.SelectNodes(relevantOptionsInfoQuery, nameSpaceManager);

            if (optionOnEquityNodes != null)
            {
                optionsInfo = new List<B3OptionOnEquityInfo>();

                foreach (XmlNode oneOptionOnEquityNode in optionOnEquityNodes)
                {
                    var oneOptionOnEquityInfo = new B3OptionOnEquityInfo();
                    var noAvo = oneOptionOnEquityNode.ParentNode?.ParentNode;
                    var noIdInterno = noAvo?.SelectSingleNode("./instrument:FinInstrmId/instrument:OthrId/instrument:Id", nameSpaceManager);
                    var noDescricao = noAvo?.SelectSingleNode("./instrument:FinInstrmAttrCmon/instrument:Desc", nameSpaceManager);
                    var underlyingID = oneOptionOnEquityNode.SelectSingleNode("./instrument:UndrlygInstrmId/instrument:OthrId/instrument:Id", nameSpaceManager);

                    oneOptionOnEquityInfo.B3ID = noIdInterno.InnerText.ToNullable<long>();
                    oneOptionOnEquityInfo.ISIN = oneOptionOnEquityNode["ISIN"].InnerText;
                    oneOptionOnEquityInfo.Description = noDescricao.InnerText.RemoveMultipleSpaces().Trim();
                    oneOptionOnEquityInfo.Ticker = oneOptionOnEquityNode["TckrSymb"].InnerText;
                    oneOptionOnEquityInfo.Strike = oneOptionOnEquityNode["ExrcPric"].InnerText.ToNullable<double>(cultureNumericAmerica);
                    oneOptionOnEquityInfo.StrikeCurrency = oneOptionOnEquityNode["ExrcPric"].Attributes["Ccy"].Value;

                    var stringHelperStyle = oneOptionOnEquityNode["ExrcPric"].InnerText;
                    oneOptionOnEquityInfo.Style =
                        (!string.IsNullOrEmpty(stringHelperStyle) && stringHelperStyle.Equals("EURO", StringComparison.InvariantCultureIgnoreCase)) ?
                            B3OptionOnEquityStyleInfo.European : B3OptionOnEquityStyleInfo.American;
                    oneOptionOnEquityInfo.Expiration = oneOptionOnEquityNode["XprtnDt"].InnerText.ToNullable<DateTime>();

                    var stringHelperType = oneOptionOnEquityNode["OptnTp"].InnerText;
                    oneOptionOnEquityInfo.Type =
                        (!string.IsNullOrEmpty(stringHelperType) && stringHelperType.Equals("PUTT", StringComparison.InvariantCultureIgnoreCase)) ?
                            B3OptionOnEquityTypeInfo.Put : B3OptionOnEquityTypeInfo.Call;

                    oneOptionOnEquityInfo.Currency = oneOptionOnEquityNode["TradgCcy"].InnerText;
                    oneOptionOnEquityInfo.B3IDUnderlying = underlyingID.InnerText.ToNullable<long>();
                    oneOptionOnEquityInfo.LoadDate = dateNode.InnerText.ToNullable<DateTime>();

                    optionsInfo.Add(oneOptionOnEquityInfo);
                }
            }

            return optionsInfo;
        }
        #endregion
    }
}
