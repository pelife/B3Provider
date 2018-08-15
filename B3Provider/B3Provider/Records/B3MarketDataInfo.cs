#region License
/*
 * B3MarketDataInfo.cs
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

    public class B3MarketDataInfo
    {
        public DateTime? TradeDate { get; set; }
        public long? B3ID { get; set; }
        public string Ticker { get; set; }        
        public double? NationalFinancialVolume { get; set; }
        public string NationalFinancialVolumeCurrency { get; set; }
        public double? InternationalFinancialVolume { get; set; }
        public string InternationalFinancialVolumeCurrency { get; set; }
        public double? OpenInterest { get; set; }
        public int? QuantityVolume { get; set; }
        public double? BestBidPrice { get; set; }
        public string BestBidPriceCurrency { get; set; }
        public double? BestAskPrice { get; set; }
        public string BestAskPriceCurrency { get; set; }
        public double? FirstPrice { get; set; }
        public string FirstPriceCurrency { get; set; }
        public double? MinimumPrice { get; set; }
        public string MinimumPriceCurrency { get; set; }
        public double? MaximumPrice { get; set; }
        public string MaximumPriceCurrency { get; set; }
        public double? TradeAveragePrice { get; set; }
        public string TradeAveragePriceCurrency { get; set; }
        public double? LastPrice { get; set; }
        public string LastPriceCurrency { get; set; }
        public int? RegularTransactionQuantity { get; set; }
        public int? NonRegularTransactionQuantity { get; set; }
        public int? RegularTradedContracts { get; set; }
        public int? NonRegularTradedContracts { get; set; }
        public double? NationalRegularVolume { get; set; }
        public string NationalRegularVolumeCurrency { get; set; }
        public double? NationalNonRegularVolume { get; set; }
        public string NationalNonRegularVolumeCurrency { get; set; }
        public double? InternationalRegularVolume { get; set; }
        public string InternationalRegularVolumeCurrency { get; set; }
        public double? InternationalNonRegularVolume { get; set; }
        public string InternationalNonRegularVolumeCurrency { get; set; }
        public double? AdjustedQuote { get; set; }
        public string AdjustedQuoteCurrency { get; set; }
        public double? AdjustedQuoteTax { get; set; }
        public string AdjustedQuoteTaxCurrency { get; set; }
        public string AdjustedQuoteSituation { get; set; }
        public double? PreviousAdjustedQuote { get; set; }
        public string PreviousAdjustedQuoteCurrency { get; set; }
        public double? PreviousAdjustedQuoteTax { get; set; }
        public string PreviousAdjustedQuoteTaxCurrency { get; set; }
        public string PreviousAdjustedQuoteSituation { get; set; }
        public double? OscillationPercentage { get; set; }
        public double? VariationPoints { get; set; }
        public string VariationPointsCurrency { get; set; }
        public double? EquivalentValue { get; set; }
        public string EquivalentValueCurrency { get; set; }
        public double? AdjustedValueContract { get; set; }
        public string AdjustedValueContractCurrency { get; set; }
        public double? MaximumTradeLimit { get; set; }
        public string MaximumTradeLimitCurrency { get; set; }
        public double? MinimumTradeLimit { get; set; }
        public string MinimumTradeLimitCurrency { get; set; }

    }
}
