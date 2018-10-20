#region License
/*
 * B3MarketDataInfoReader.cs
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
    using B3Provider.Records;
    using B3Provider.Utils;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Xml;

    /// <summary>
    /// Class responsible for reading all the Daily Market Data info from B3 Daily quotes file
    /// </summary>
    public class B3MarketDataInfoReader : AbstractReader<B3MarketDataInfo>
    {
        #region "public methods"

        /// <summary>
        /// Method responsible to read records from file of daily quotes file
        /// </summary>
        /// <param name="filePath">File to read records from</param>
        /// <returns>
        /// List of all daily quotes file
        /// </returns>
        public override IList<B3MarketDataInfo> ReadRecords(string filePath)
        {
            var listOfMarketData = new List<B3MarketDataInfo>();

            var temporaryPath = GetRandomTemporaryDirectory();
            var filesToRead = UnzipFile(filePath, temporaryPath);
            if (filesToRead != null && filesToRead.Length > 0)
            {
                foreach (string oneFileToRead in filesToRead)
                {
                    var marketDataInfo = ReadFile(oneFileToRead);
                    listOfMarketData.AddRange(marketDataInfo);
                }
            }

            DeleteDirectory(temporaryPath);
            return listOfMarketData;
        }
        #endregion

        #region "private methods"
        /// <summary>
        /// Internal private file responsible to read records of daily quotes from file
        /// </summary>
        /// <param name="filePath">File path to read daily quotes records from</param>
        /// <returns>
        /// All daily quotes found in the file
        /// </returns>
        private IList<B3MarketDataInfo> ReadFile(string filePath)
        {
            IList<B3MarketDataInfo> marketDataInfo = null;

            var cultureNumericAmerica = new CultureInfo("en-US");

            var marketDataDocument = new XmlDocument();
            var relevantMarketDataInfoQuery = "//price:PricRpt";

            marketDataDocument.Load(filePath);

            var rootNode = marketDataDocument.DocumentElement;
            var nameSpaceManager = new XmlNamespaceManager(marketDataDocument.NameTable);

            nameSpaceManager.AddNamespace("report", "urn:bvmf.052.01.xsd");
            nameSpaceManager.AddNamespace("price", "urn:bvmf.217.01.xsd");

            var dateNode = rootNode.SelectSingleNode("//report:CreDtAndTm[1]", nameSpaceManager);
            var marketDataNodes = rootNode.SelectNodes(relevantMarketDataInfoQuery, nameSpaceManager);

            if (marketDataNodes != null)
            {
                marketDataInfo = new List<B3MarketDataInfo>();

                foreach (XmlNode oneMarketDataNode in marketDataNodes)
                {
                    var oneMarketDataInfo = new B3MarketDataInfo();
                    var noTradeDate = oneMarketDataNode.SelectSingleNode("./price:TradDt/price:Dt", nameSpaceManager);
                    var noTicker = oneMarketDataNode.SelectSingleNode("./price:SctyId/price:TckrSymb", nameSpaceManager);
                    var noIdInterno = oneMarketDataNode.SelectSingleNode("./price:FinInstrmId/price:OthrId/price:Id", nameSpaceManager);
                    var noMarketData = oneMarketDataNode.SelectSingleNode("./price:FinInstrmAttrbts", nameSpaceManager);

                    oneMarketDataInfo.B3ID = noIdInterno.InnerText.ToNullable<long>();
                    oneMarketDataInfo.Ticker = noTicker.InnerText;
                    oneMarketDataInfo.TradeDate = noTradeDate.InnerText.ToNullable<DateTime>();
                    oneMarketDataInfo.NationalFinancialVolume = noMarketData["NtlFinVol"]?.InnerText.ToNullable<double>(cultureNumericAmerica) ?? null;
                    oneMarketDataInfo.NationalFinancialVolumeCurrency = noMarketData["NtlFinVol"]?.Attributes["Ccy"]?.InnerText ?? null;
                    oneMarketDataInfo.InternationalFinancialVolume = noMarketData["IntlFinVol"]?.InnerText.ToNullable<double>(cultureNumericAmerica) ?? null;
                    oneMarketDataInfo.InternationalFinancialVolumeCurrency = noMarketData["IntlFinVol"]?.Attributes["Ccy"]?.InnerText ?? null;
                    oneMarketDataInfo.OpenInterest = noMarketData["OpnIntrst"]?.InnerText.ToNullable<double>(cultureNumericAmerica);
                    oneMarketDataInfo.QuantityVolume = noMarketData["FinInstrmQty"]?.InnerText.ToNullable<long>();
                    oneMarketDataInfo.BestBidPrice = noMarketData["BestBidPric"]?.InnerText.ToNullable<double>(cultureNumericAmerica) ?? null;
                    oneMarketDataInfo.BestBidPriceCurrency = noMarketData["BestBidPric"]?.Attributes["Ccy"]?.InnerText ?? null;
                    oneMarketDataInfo.BestAskPrice = noMarketData["BestAskPric"]?.InnerText.ToNullable<double>(cultureNumericAmerica) ?? null;
                    oneMarketDataInfo.BestAskPriceCurrency = noMarketData["BestAskPric"]?.Attributes["Ccy"]?.InnerText ?? null;
                    oneMarketDataInfo.FirstPrice = noMarketData["FrstPric"]?.InnerText.ToNullable<double>(cultureNumericAmerica) ?? null;
                    oneMarketDataInfo.FirstPriceCurrency = noMarketData["FrstPric"]?.Attributes["Ccy"]?.InnerText ?? null;
                    oneMarketDataInfo.MinimumPrice = noMarketData["MinPric"]?.InnerText.ToNullable<double>(cultureNumericAmerica) ?? null;
                    oneMarketDataInfo.MinimumPriceCurrency = noMarketData["MinPric"]?.Attributes["Ccy"]?.InnerText ?? null;
                    oneMarketDataInfo.MaximumPrice = noMarketData["MaxPric"]?.InnerText.ToNullable<double>(cultureNumericAmerica) ?? null;
                    oneMarketDataInfo.MaximumPriceCurrency = noMarketData["MaxPric"]?.Attributes["Ccy"]?.InnerText ?? null;
                    oneMarketDataInfo.TradeAveragePrice = noMarketData["TradAvrgPric"]?.InnerText.ToNullable<double>(cultureNumericAmerica) ?? null;
                    oneMarketDataInfo.TradeAveragePriceCurrency = noMarketData["TradAvrgPric"]?.Attributes["Ccy"]?.InnerText ?? null;
                    oneMarketDataInfo.LastPrice = noMarketData["LastPric"]?.InnerText.ToNullable<double>(cultureNumericAmerica) ?? null;
                    oneMarketDataInfo.LastPriceCurrency = noMarketData["LastPric"]?.Attributes["Ccy"]?.InnerText ?? null;
                    oneMarketDataInfo.RegularTransactionQuantity = noMarketData["RglrTxsQty"]?.InnerText.ToNullable<int>() ?? null;
                    oneMarketDataInfo.NonRegularTransactionQuantity = noMarketData["NonRglrTxsQty"]?.InnerText.ToNullable<int>() ?? null;
                    oneMarketDataInfo.RegularTradedContracts = noMarketData["RglrTraddCtrcts"]?.InnerText.ToNullable<long>() ?? null;
                    oneMarketDataInfo.NonRegularTradedContracts = noMarketData["NonRglrTraddCtrcts"]?.InnerText.ToNullable<long>() ?? null;
                    oneMarketDataInfo.NationalRegularVolume = noMarketData["NtlRglrVol"]?.InnerText.ToNullable<double>(cultureNumericAmerica) ?? null;
                    oneMarketDataInfo.NationalRegularVolumeCurrency = noMarketData["NtlRglrVol"]?.Attributes["Ccy"]?.InnerText ?? null;
                    oneMarketDataInfo.NationalNonRegularVolume = noMarketData["NtlNonRglrVol"]?.InnerText.ToNullable<double>(cultureNumericAmerica) ?? null;
                    oneMarketDataInfo.NationalNonRegularVolumeCurrency = noMarketData["NtlNonRglrVol"]?.Attributes["Ccy"]?.InnerText ?? null;
                    oneMarketDataInfo.InternationalRegularVolume = noMarketData["IntlRglrVol"]?.InnerText.ToNullable<double>(cultureNumericAmerica) ?? null;
                    oneMarketDataInfo.InternationalRegularVolumeCurrency = noMarketData["IntlRglrVol"]?.Attributes["Ccy"]?.InnerText ?? null;
                    oneMarketDataInfo.InternationalNonRegularVolume = noMarketData["IntlNonRglrVol"]?.InnerText.ToNullable<double>(cultureNumericAmerica) ?? null;
                    oneMarketDataInfo.InternationalNonRegularVolumeCurrency = noMarketData["IntlNonRglrVol"]?.Attributes["Ccy"]?.InnerText ?? null;
                    oneMarketDataInfo.AdjustedQuote = noMarketData["AdjstdQt"]?.InnerText.ToNullable<double>(cultureNumericAmerica) ?? null;
                    oneMarketDataInfo.AdjustedQuoteCurrency = noMarketData["AdjstdQt"]?.Attributes["Ccy"]?.InnerText ?? null;
                    oneMarketDataInfo.AdjustedQuoteTax = noMarketData["AdjstdQtTax"]?.InnerText.ToNullable<double>(cultureNumericAmerica) ?? null;
                    oneMarketDataInfo.AdjustedQuoteTaxCurrency = noMarketData["AdjstdQtTax"]?.Attributes["Ccy"]?.InnerText ?? null;
                    oneMarketDataInfo.AdjustedQuoteSituation = noMarketData["AdjstdQtStin"]?.InnerText ?? null;
                    oneMarketDataInfo.PreviousAdjustedQuote = noMarketData["PrvsAdjstdQt"]?.InnerText.ToNullable<double>(cultureNumericAmerica) ?? null;
                    oneMarketDataInfo.PreviousAdjustedQuoteCurrency = noMarketData["PrvsAdjstdQt"]?.Attributes["Ccy"]?.InnerText ?? null;
                    oneMarketDataInfo.PreviousAdjustedQuoteTax = noMarketData["PrvsAdjstdQtTax"]?.InnerText.ToNullable<double>(cultureNumericAmerica) ?? null;
                    oneMarketDataInfo.PreviousAdjustedQuoteTaxCurrency = noMarketData["PrvsAdjstdQtTax"]?.Attributes["Ccy"]?.InnerText ?? null;
                    oneMarketDataInfo.PreviousAdjustedQuoteSituation = noMarketData["PrvsAdjstdQtStin"]?.InnerText ?? null;
                    oneMarketDataInfo.OscillationPercentage = noMarketData["OscnPctg"]?.InnerText.ToNullable<double>(cultureNumericAmerica) ?? null;
                    oneMarketDataInfo.VariationPoints = noMarketData["VartnPts"]?.InnerText.ToNullable<double>(cultureNumericAmerica) ?? null;
                    oneMarketDataInfo.VariationPointsCurrency = noMarketData["VartnPts"]?.Attributes["Ccy"]?.InnerText ?? null;
                    oneMarketDataInfo.EquivalentValue = noMarketData["EqvtVal"]?.InnerText.ToNullable<double>(cultureNumericAmerica) ?? null;
                    oneMarketDataInfo.EquivalentValueCurrency = noMarketData["EqvtVal"]?.Attributes["Ccy"]?.InnerText ?? null;
                    oneMarketDataInfo.AdjustedValueContract = noMarketData["AdjstdValCtrct"]?.InnerText.ToNullable<double>(cultureNumericAmerica) ?? null;
                    oneMarketDataInfo.AdjustedValueContractCurrency = noMarketData["AdjstdValCtrct"]?.Attributes["Ccy"]?.InnerText ?? null;
                    oneMarketDataInfo.MaximumTradeLimit = noMarketData["MaxTradLmt"]?.InnerText.ToNullable<double>(cultureNumericAmerica) ?? null;
                    oneMarketDataInfo.MaximumTradeLimitCurrency = noMarketData["MaxTradLmt"]?.Attributes["Ccy"]?.InnerText ?? null;
                    oneMarketDataInfo.MinimumTradeLimit = noMarketData["MinTradLmt"]?.InnerText.ToNullable<double>(cultureNumericAmerica) ?? null;
                    oneMarketDataInfo.MinimumTradeLimitCurrency = noMarketData["MinTradLmt"]?.Attributes["Ccy"]?.InnerText ?? null;

                    marketDataInfo.Add(oneMarketDataInfo);
                }
            }
            return marketDataInfo;
        }

        #endregion
    }
}
