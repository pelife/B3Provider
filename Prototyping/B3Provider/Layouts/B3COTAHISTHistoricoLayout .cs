#region License
/*
 * B3COTAHISTTrailerLayout.cs
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

namespace B3Provider.Layouts
{
    using B3Provider.Converters;
    using B3Provider.Records;
    using FlatFile.FixedLength.Implementation;

    /// <summary>
    /// class that represents the layout of records in file cotahist to be read from the framework
    /// </summary>
    public sealed class B3COTAHISTHistoricoLayout : FixedLayout<B3HistoricMarketDataInfo>
    {
        /// <summary>
        /// Default constructor where we map all the properties of the returnin class <see cref="B3HistoricMarketDataInfo"/>
        /// </summary>
        public B3COTAHISTHistoricoLayout()
        {
            WithMember(x => x.Type, c => c.WithLength(2))                                                                               //2     -   2  N(02) 01 02
                .WithMember(x => x.TradeDate, c => c.WithLength(8).WithTypeConverter<B3DateTypeConverter>())                            //10    -   8  N(08) 03 10
                .WithMember(x => x.BDICode, c => c.WithLength(2).WithTypeConverter<B3StringTypeConverter>())                            //12    -   2  X(02) 11 12
                .WithMember(x => x.Ticker, c => c.WithLength(12).WithTypeConverter<B3StringTypeConverter>())                         //24    -   12 X(12) 13 24
                .WithMember(x => x.MarketTypeCode, c => c.WithLength(3))                                                                //27    -   3  N(03) 25 27 
                .WithMember(x => x.ShortName, c => c.WithLength(12).WithTypeConverter<B3StringTypeConverter>())                         //39    -   12 X(12) 28 39
                .WithMember(x => x.Specification, c => c.WithLength(10).WithTypeConverter<B3StringTypeConverter>())                     //40    -   10 X(10) 40 49
                .WithMember(x => x.ForwardDaysToExpiry, c => c.WithLength(2))                                                           //43    -   3  X(03) 50 52
                .WithMember(x => x.ReferenceCurrency, c => c.WithLength(4).WithTypeConverter<B3StringTypeConverter>())                  //47    -   4  X(04) 53 56
                .WithMember(x => x.Opening, c => c.WithLength(13).WithTypeConverter<B3Double100TypeConverter>())                        //60    -   13 (11)V99 57 69
                .WithMember(x => x.Maximum, c => c.WithLength(13).WithTypeConverter<B3Double100TypeConverter>())                        //73    -   13 (11)V99 70 82
                .WithMember(x => x.Minimum, c => c.WithLength(13).WithTypeConverter<B3Double100TypeConverter>())                        //86    -   13 (11)V99 83 95
                .WithMember(x => x.Average, c => c.WithLength(13).WithTypeConverter<B3Double100TypeConverter>())                        //99    -   13 (11)V99 96 108
                .WithMember(x => x.Last, c => c.WithLength(13).WithTypeConverter<B3Double100TypeConverter>())                           //112   -   13 (11)V99 109 121
                .WithMember(x => x.BestBidPrice, c => c.WithLength(13).WithTypeConverter<B3Double100TypeConverter>())                   //125   -   13 (11)V99 122 134
                .WithMember(x => x.BestAskPrice, c => c.WithLength(13).WithTypeConverter<B3Double100TypeConverter>())                   //138   -   13 (11)V99 135 147
                .WithMember(x => x.TradeQuantity, c => c.WithLength(5))                                                                 //143   -   5  N(05) 148 152
                .WithMember(x => x.ContractQuantity, c => c.WithLength(18))                                                             //161   -   18 N(18) 153 170
                .WithMember(x => x.FinancialVolume, c => c.WithLength(18).WithTypeConverter<B3Double100TypeConverter>())                //178   -   17 (16)V99 171 188
                .WithMember(x => x.StrikePrice, c => c.WithLength(13).WithTypeConverter<B3Double100TypeConverter>())                    //191   -   13 (11)V99 189 201
                .WithMember(x => x.StrikePriceCorrectionIndicator, c => c.WithLength(1))                                                //192   -   1  N(01) 202 202
                .WithMember(x => x.ExpiryDate, c => c.WithLength(8).WithTypeConverter<B3DateTypeConverter>())                           //200   -   8  N(08) 203 210
                .WithMember(x => x.QuoteFactor, c => c.WithLength(7))                                                                   //207   -   7  N(07) 211 217
                .WithMember(x => x.DollarOptionPointsStrikePrice, c => c.WithLength(13).WithTypeConverter<B3Double100TypeConverter>())  //220   -   13 (07)V06 218 230
                .WithMember(x => x.ISIN, c => c.WithLength(12).WithTypeConverter<B3StringTypeConverter>())                              //233   -   13 X(12) 231 242
                .WithMember(x => x.InstrumentDistributionNumber, c => c.WithLength(3));                                                 //236   -   3  9(03) 243 245
        }
    }
}
