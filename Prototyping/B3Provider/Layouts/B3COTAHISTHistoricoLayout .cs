namespace B3Provider.Layouts
{

    using B3Provider.Converters;
    using FlatFile.FixedLength.Implementation;

    public sealed class B3COTAHISTHistoricoLayout : FixedLayout<B3HistoricMarketDataInfo>
    {
        public B3COTAHISTHistoricoLayout()
        {
            this.WithMember(x => x.Type, c => c.WithLength(2))                                                                              //2     -   2
                .WithMember(x => x.TradeDate, c => c.WithLength(8).WithTypeConverter<B3DateTypeConverter>())                              //10    -   8
                .WithMember(x => x.BDICode, c => c.WithLength(2).WithTypeConverter<B3StringTypeConverter>())                             //12    -   2
                .WithMember(x => x.TradeCode, c => c.WithLength(12).WithTypeConverter<B3StringTypeConverter>())                     //24    -   12
                .WithMember(x => x.MarketTypeCode, c => c.WithLength(3))                                                                 //27    -   3
                .WithMember(x => x.ShortName, c => c.WithLength(12).WithTypeConverter<B3StringTypeConverter>())                         //39    -   12
                .WithMember(x => x.Specification, c => c.WithLength(10).WithTypeConverter<B3StringTypeConverter>())                        //40    -   10
                .WithMember(x => x.ForwardDaysToExpiry, c => c.WithLength(3))                                                                    //43    -   3
                .WithMember(x => x.ReferenceCurrency, c => c.WithLength(4).WithTypeConverter<B3StringTypeConverter>())                                                                   //47    -   4
                .WithMember(x => x.Opening, c => c.WithLength(13).WithTypeConverter<B3Double100TypeConverter>())                          //60    -   13 
                .WithMember(x => x.Maximum, c => c.WithLength(13).WithTypeConverter<B3Double100TypeConverter>())                            //73    -   13
                .WithMember(x => x.Minimum, c => c.WithLength(13).WithTypeConverter<B3Double100TypeConverter>())                            //86    -   13
                .WithMember(x => x.Average, c => c.WithLength(13).WithTypeConverter<B3Double100TypeConverter>())                             //99    -   13
                .WithMember(x => x.Last, c => c.WithLength(13).WithTypeConverter<B3Double100TypeConverter>())                            //112   -   13
                .WithMember(x => x.BestBidPrice, c => c.WithLength(13).WithTypeConverter<B3Double100TypeConverter>())                //125   -   13
                .WithMember(x => x.BestAskPrice, c => c.WithLength(13).WithTypeConverter<B3Double100TypeConverter>())                 //138   -   13
                .WithMember(x => x.TradeQuantity, c => c.WithLength(5))                                                                     //143   -   5
                .WithMember(x => x.ContractQuantity, c => c.WithLength(18))                                                                //161   -   18
                .WithMember(x => x.FinancialVolume, c => c.WithLength(18).WithTypeConverter<B3Double100TypeConverter>())                       //178   -   17
                .WithMember(x => x.StrikePrice, c => c.WithLength(13).WithTypeConverter<B3Double100TypeConverter>())                    //191   -   13
                .WithMember(x => x.StrikePriceCorrectionIndicator, c => c.WithLength(1))                                                   //192   -   1
                .WithMember(x => x.ExpiryDate, c => c.WithLength(8).WithTypeConverter<B3DateTypeConverter>())                          //200   -   8
                .WithMember(x => x.QuoteFactor, c => c.WithLength(7))                                                                      //207   -   7
                .WithMember(x => x.DollarOptionPointsStrikePrice, c => c.WithLength(13).WithTypeConverter<B3Double100TypeConverter>())    //220   -   13
                .WithMember(x => x.ISIN, c => c.WithLength(12).WithTypeConverter<B3StringTypeConverter>())                           //233   -   13
                .WithMember(x => x.InstrumentDistributionNumber, c => c.WithLength(3));                                                          //236   -   3
        }
    }
}
