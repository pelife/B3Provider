#region License
/*
 * B3FutureInfoReader.cs
 *
 * The MIT License
 *
 * Copyright (c) 2019 Felipe Bahiana Almeida
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
    using B3Provider.Records;
    using B3Provider.Utils;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Xml;

    /// <summary>
    /// Method responsible for reading all the options on future info from B3 instrument file
    /// </summary>
    public class B3FutureInfoReader : AbstractReader<B3FutureInfo>
    {
        #region "public methods"
        /// <summary>
        /// Method responsible to read records from file
        /// </summary>
        /// <param name="filePath">File to read records from</param>
        /// <returns>
        /// List of all options on future instruments found
        /// </returns>
        public override IList<B3FutureInfo> ReadRecords(string filePath)
        {
            var listOfFuture = new List<B3FutureInfo>();

            var temporaryPath = GetRandomTemporaryDirectory();
            var filesToRead = UnzipFile(filePath, temporaryPath);
            if (filesToRead != null && filesToRead.Length > 0)
            {
                foreach (string oneFileToRead in filesToRead)
                {
                    var futureInfo = ReadFile(oneFileToRead);
                    listOfFuture.AddRange(futureInfo);
                }
            }

            DeleteDirectory(temporaryPath);
            return listOfFuture;
        }
        #endregion

        #region "private methods"
        /// <summary>
        /// Internal private file responsible to read records of futures from file
        /// </summary>
        /// <param name="filePath">File path to read futre records from</param>
        /// <returns>
        /// All futures found in file
        /// </returns>
        private IList<B3FutureInfo> ReadFile(string filePath)
        {
            IList<B3FutureInfo> futureInfo = null;


            var cultureNumericAmerica = new CultureInfo("en-US");
            var futureInfoDocument = new XmlDocument();
            var relevantFutureInfoQuery = "//instrument:FinInstrmAttrCmon[instrument:Mkt=2 and instrument:Sgmt=5]/..//instrument:FutrCtrctsInf";

            futureInfoDocument.Load(filePath);

            var rootNode = futureInfoDocument.DocumentElement;
            var nameSpaceManager = new XmlNamespaceManager(futureInfoDocument.NameTable);

            nameSpaceManager.AddNamespace("report", "urn:bvmf.052.01.xsd");
            nameSpaceManager.AddNamespace("instrument", "urn:bvmf.100.02.xsd");

            var dateNode = rootNode.SelectSingleNode("//report:CreDtAndTm[1]", nameSpaceManager);
            var futureNodes = rootNode.SelectNodes(relevantFutureInfoQuery, nameSpaceManager);

            if (futureNodes != null)
            {
                futureInfo = new List<B3FutureInfo>();

                foreach (XmlNode oneFutureNode in futureNodes)
                {
                    var oneFutureInfo = new B3FutureInfo();
                    var noAvo = oneFutureNode.ParentNode?.ParentNode;
                    var noIdInterno = noAvo?.SelectSingleNode("./instrument:FinInstrmId/instrument:OthrId/instrument:Id", nameSpaceManager);
                    var noDescricao = noAvo?.SelectSingleNode("./instrument:FinInstrmAttrCmon/instrument:Desc", nameSpaceManager);
                    var noDescricaoAsset = noAvo?.SelectSingleNode("./instrument:FinInstrmAttrCmon/instrument:AsstDesc", nameSpaceManager);
                    var noNameAsset = noAvo?.SelectSingleNode("./instrument:FinInstrmAttrCmon/instrument:Asst", nameSpaceManager);
                    var underlyingData = oneFutureNode.SelectSingleNode("./instrument:UndrlygInstrmId", nameSpaceManager);
                    var settlementData = oneFutureNode.SelectSingleNode("./instrument:AsstSttlmInd", nameSpaceManager);

                    oneFutureInfo.B3ID = noIdInterno.InnerText.ToNullable<long>();
                    oneFutureInfo.AssetName = noNameAsset.InnerText.RemoveMultipleSpaces().Trim();
                    oneFutureInfo.AssetDescription = noDescricaoAsset.InnerText.RemoveMultipleSpaces().Trim();
                    oneFutureInfo.Description = noDescricao.InnerText.RemoveMultipleSpaces().Trim();

                    oneFutureInfo.ISIN = oneFutureNode["ISIN"].InnerText;
                    oneFutureInfo.SecurityCategoryCode = oneFutureNode["SctyCtgy"].InnerText.To<int>();
                    oneFutureInfo.Ticker = oneFutureNode["TckrSymb"].InnerText;
                    oneFutureInfo.Expiration = oneFutureNode["XprtnDt"].InnerText.To<DateTime>();
                    oneFutureInfo.ExpirationCode = oneFutureNode["XprtnCd"].InnerText;
                    oneFutureInfo.TradeStart = oneFutureNode["TradgStartDt"].InnerText.To<DateTime>();
                    oneFutureInfo.TradeEnd = oneFutureNode["TradgEndDt"].InnerText.To<DateTime>();
                    oneFutureInfo.ValueTypeCode = oneFutureNode["ValTpCd"].InnerText.To<int>();
                    oneFutureInfo.DaycountBase = oneFutureNode["BaseCd"]?.InnerText.ToNullable<int>();
                    oneFutureInfo.ConversionCriteriaCode = oneFutureNode["ConvsCrit"]?.InnerText.ToNullable<int>();
                    oneFutureInfo.MaturityContractValueInPoints = oneFutureNode["MtrtyDtTrgtPt"]?.InnerText.ToNullable<double>(cultureNumericAmerica);
                    oneFutureInfo.RequiredConversionIndicator = oneFutureNode["ReqrdConvsInd"]?.InnerText.To<bool>();
                    oneFutureInfo.CFICategoryCode = oneFutureNode["CFICd"].InnerText.Trim();
                    oneFutureInfo.DeliveryTypeCode = oneFutureNode["DlvryTp"].InnerText.To<int>();
                    oneFutureInfo.DeliveryNoticeStart = oneFutureNode["DlvryNtceStartDt"] != null ? oneFutureNode["DlvryNtceStartDt"].InnerText.ToNullable<DateTime>() : null;
                    oneFutureInfo.DeliveryNoticeEnd = oneFutureNode["DlvryNtceEndDt"] != null ? oneFutureNode["DlvryNtceEndDt"].InnerText.ToNullable<DateTime>() : null;
                    oneFutureInfo.PaymentTypeCode = oneFutureNode["PmtTp"].InnerText.To<int>();
                    oneFutureInfo.ContractMultiplier = oneFutureNode["CtrctMltplr"].InnerText.To<double>(cultureNumericAmerica);
                    oneFutureInfo.AssetQuotationQuantity = oneFutureNode["AsstQtnQty"].InnerText.To<double>(cultureNumericAmerica);

                    if (settlementData != null)
                    {
                        oneFutureInfo.SettlementIndexInfo = new B3FutureDerivativeInfo();
                        oneFutureInfo.SettlementIndexInfo.Identifier = settlementData.SelectSingleNode("./instrument:OthrId/instrument:Id", nameSpaceManager).InnerText.To<long>();
                        oneFutureInfo.SettlementIndexInfo.IdentifierTypeCode = settlementData.SelectSingleNode("./instrument:OthrId/instrument:Tp/instrument:Prtry", nameSpaceManager).InnerText.To<int>();
                        oneFutureInfo.SettlementIndexInfo.PlaceOfListing = settlementData.SelectSingleNode("./instrument:PlcOfListg/instrument:MktIdrCd", nameSpaceManager).InnerText;
                    }

                    oneFutureInfo.AllocationRoundLot = oneFutureNode["AllcnRndLot"].InnerText.To<double>(cultureNumericAmerica);
                    oneFutureInfo.Currency = oneFutureNode["TradgCcy"].InnerText;

                    if (underlyingData != null)
                    {
                        oneFutureInfo.UnderlyingIntrument = new B3FutureDerivativeInfo();
                        oneFutureInfo.UnderlyingIntrument.Identifier = underlyingData.SelectSingleNode("./instrument:OthrId/instrument:Id", nameSpaceManager).InnerText.To<long>();
                        oneFutureInfo.UnderlyingIntrument.IdentifierTypeCode = underlyingData.SelectSingleNode("./instrument:OthrId/instrument:Tp/instrument:Prtry", nameSpaceManager).InnerText.To<int>();
                        oneFutureInfo.UnderlyingIntrument.PlaceOfListing = underlyingData.SelectSingleNode("./instrument:PlcOfListg/instrument:MktIdrCd", nameSpaceManager).InnerText;
                    }

                    oneFutureInfo.WithdrawalDays = oneFutureNode["WdrwlDays"].InnerText.To<int>();
                    oneFutureInfo.WorkingDays = oneFutureNode["WrkgDays"].InnerText.To<int>();
                    oneFutureInfo.CalendarDays = oneFutureNode["ClnrDays"].InnerText.To<int>();

                    oneFutureInfo.LoadDate = dateNode.InnerText.ToNullable<DateTime>();
                    futureInfo.Add(oneFutureInfo);
                }
            }

            return futureInfo;
        }
        #endregion
    }
}