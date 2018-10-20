#region License
/*
 * B3ProviderDbContext.cs
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
namespace B3Provider.Database
{
    using B3Provider.Records;
    using SQLite.CodeFirst;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    public class B3ProviderDbContext : DbContext
    {
        public B3ProviderDbContext(Action<string> logger)
        {
            this.Database.Log = logger;
        }

        public DbSet<B3SectorClassifcationInfo> SectorClassification { get; set; }
        public DbSet<B3EquityInfo> EquityInstruments { get; set; }
        public DbSet<B3OptionOnEquityInfo> OptionsOnEquityInstruments { get; set; }
        public DbSet<B3MarketDataInfo> MarketDataInfo { get; set; }
        public DbSet<B3HistoricMarketDataInfo> HistoricMarketDataInfo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Configurations.Add(new B3SectorClassifcationInfoEntityConfiguration());
            modelBuilder.Configurations.Add(new B3EquityInfoEntityConfiguration());
            modelBuilder.Configurations.Add(new B3OptionOnEquityInfoEntityConfiguration());
            modelBuilder.Configurations.Add(new B3MarketDataInfoEntityConfiguration());
            modelBuilder.Configurations.Add(new B3HistoricMarketDataInfoEntityConfiguration());

            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<B3ProviderDbContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }
    }

    public class B3SectorClassifcationInfoEntityConfiguration : EntityTypeConfiguration<B3SectorClassifcationInfo>
    {
        public B3SectorClassifcationInfoEntityConfiguration()
        {
            ToTable("tb_b3_sector_classification");
            HasKey(k => k.ID).Property(p => p.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnName("id_sector");

            Property(p => p.EconomicSector).HasColumnName("tx_economic_sector").IsRequired();
            Property(p => p.EconomicSubSector).HasColumnName("tx_economic_subsector").IsRequired();
            Property(p => p.EconomicSegment).HasColumnName("tx_economic_segment").IsRequired();
            Property(p => p.CompanyName).HasColumnName("tx_company_name").IsRequired();
            Property(p => p.CompanyListingCode).HasColumnName("tx_company_listing_code").IsRequired()
                 .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("ix_sc_tx_company_listing_code", 1) { IsUnique = true }));

            Property(p => p.CompanyListingSegment).HasColumnName("tx_company_listing_segment").IsRequired();
        }

    }

    public class B3EquityInfoEntityConfiguration : EntityTypeConfiguration<B3EquityInfo>
    {
        public B3EquityInfoEntityConfiguration()
        {
            ToTable("tb_b3_equity_info");
            HasKey(k => k.B3ID);
            Property(p => p.B3ID).HasColumnName("id_b3").IsRequired();

            Property(p => p.Ticker).HasColumnName("tx_ticker").IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("ix_ei_tx_ticker", 1) { IsUnique = true }));

            Property(p => p.ISIN).HasColumnName("tx_isin").IsRequired()
                .HasColumnAnnotation(
                        IndexAnnotation.AnnotationName,
                        new IndexAnnotation(
                            new IndexAttribute("ix_ei_tx_isin", 2) { IsUnique = true }));

            Property(p => p.CompanyName).HasColumnName("tx_company_name").IsRequired();
            Property(p => p.Description).HasColumnName("tx_description").IsRequired();
            Property(p => p.Currency).HasColumnName("tx_trading_ccy").IsRequired();
            Property(p => p.MarketCapitalization).HasColumnName("vl_market_capitalization").IsRequired();
            Property(p => p.LastPrice).HasColumnName("vl_last_price").IsRequired();
            Property(p => p.LoadDate).HasColumnName("dt_update").IsRequired();
            Ignore(t => t.SectorClassification);

        }
    }

    public class B3OptionOnEquityInfoEntityConfiguration : EntityTypeConfiguration<B3OptionOnEquityInfo>
    {
        public B3OptionOnEquityInfoEntityConfiguration()
        {
            ToTable("tb_b3_option_equity_info");
            HasKey(k => k.B3ID);
            Property(p => p.B3ID).HasColumnName("id_b3").IsRequired();
            
            Property(p => p.Ticker).HasColumnName("tx_ticker").IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("ix_oe_tx_ticker", 1) { IsUnique = true }));

            Property(p => p.ISIN).HasColumnName("tx_isin").IsRequired()
               .HasColumnAnnotation(
                       IndexAnnotation.AnnotationName,
                       new IndexAnnotation(
                           new IndexAttribute("ix_oe_tx_isin", 2) { IsUnique = true }));

            Property(p => p.Description).HasColumnName("tx_description").IsRequired();
            Property(p => p.Strike).HasColumnName("vl_strike").IsRequired();
            Property(p => p.StrikeCurrency).HasColumnName("tx_strike_ccy").IsRequired();
            Property(p => p.Style).HasColumnName("tp_style").IsRequired(); //todo: mapping enum
            Property(p => p.Type).HasColumnName("tp_type").IsRequired(); //todo: mapping enum
            Property(p => p.Expiration).HasColumnName("dt_expiration").IsRequired();
            Property(p => p.Currency).HasColumnName("tx_trading_ccy").IsRequired();
            Property(p => p.LoadDate).HasColumnName("dt_update").IsRequired();
            Property(p => p.B3IDUnderlying).HasColumnName("id_b3_underlying").IsRequired();
        }
    }

    public class B3MarketDataInfoEntityConfiguration : EntityTypeConfiguration<B3MarketDataInfo>
    {
        public B3MarketDataInfoEntityConfiguration()
        {
            ToTable("tb_b3_market_data_info");
            HasKey(k => new { k.TradeDate, k.B3ID });

            Property(p => p.TradeDate).HasColumnName("dt_session").IsRequired();
            Property(p => p.B3ID).HasColumnName("id_b3").IsRequired();
            Property(p => p.Ticker).HasColumnName("tx_ticker").IsRequired();
            Property(p => p.NationalFinancialVolume).HasColumnName("vl_national_fin_volume");
            Property(p => p.NationalFinancialVolumeCurrency).HasColumnName("tx_national_fin_volume_ccy");
            Property(p => p.InternationalFinancialVolume).HasColumnName("vl_international_fin_vol");
            Property(p => p.InternationalFinancialVolumeCurrency).HasColumnName("tx_international_fin_vol_ccy");
            Property(p => p.OpenInterest).HasColumnName("vl_open_interest");
            Property(p => p.QuantityVolume).HasColumnName("vl_qty_volume");
            Property(p => p.BestBidPrice).HasColumnName("vl_best_bid");
            Property(p => p.BestBidPriceCurrency).HasColumnName("tx_best_bid_ccy");
            Property(p => p.BestAskPrice).HasColumnName("vl_best_ask");
            Property(p => p.BestAskPriceCurrency).HasColumnName("tx_best_ask_ccy");
            Property(p => p.FirstPrice).HasColumnName("vl_open");
            Property(p => p.FirstPriceCurrency).HasColumnName("tx_open_ccy");
            Property(p => p.MinimumPrice).HasColumnName("vl_low");
            Property(p => p.MinimumPriceCurrency).HasColumnName("tx_low_ccy");
            Property(p => p.MaximumPrice).HasColumnName("vl_high");
            Property(p => p.MaximumPriceCurrency).HasColumnName("tx_high_ccy");
            Property(p => p.TradeAveragePrice).HasColumnName("vl_average");
            Property(p => p.TradeAveragePriceCurrency).HasColumnName("tx_average_ccy");
            Property(p => p.LastPrice).HasColumnName("vl_close");
            Property(p => p.LastPriceCurrency).HasColumnName("tx_close_ccy");
            Property(p => p.RegularTransactionQuantity).HasColumnName("vl_qty_regular_transaction");
            Property(p => p.NonRegularTransactionQuantity).HasColumnName("vl_qty_nonregular_transaction");
            Property(p => p.RegularTradedContracts).HasColumnName("vl_qty_regular_contract");
            Property(p => p.NonRegularTradedContracts).HasColumnName("vl_qty_nonregular_contract");
            Property(p => p.NationalRegularVolume).HasColumnName("vl_national_regular_volume");
            Property(p => p.NationalRegularVolumeCurrency).HasColumnName("tx_national_regular_volume_ccy");
            Property(p => p.NationalNonRegularVolume).HasColumnName("vl_national_nonregular_volume");
            Property(p => p.NationalNonRegularVolumeCurrency).HasColumnName("tx_national_nonregular_volume_ccy");
            Property(p => p.InternationalRegularVolume).HasColumnName("vl_international_regular_volume");
            Property(p => p.InternationalRegularVolumeCurrency).HasColumnName("tx_international_regular_volume_ccy");
            Property(p => p.InternationalNonRegularVolume).HasColumnName("vl_international_nonregular_volume");
            Property(p => p.InternationalNonRegularVolumeCurrency).HasColumnName("tx_international_nonregular_volume_ccy");
            Property(p => p.AdjustedQuote).HasColumnName("vl_curr_adjusted");
            Property(p => p.AdjustedQuoteCurrency).HasColumnName("tx_curr_adjusted_ccy");
            Property(p => p.AdjustedQuoteTax).HasColumnName("vl_curr_adjusted_tax");
            Property(p => p.AdjustedQuoteTaxCurrency).HasColumnName("tx_curr_adjusted_tax_ccy");
            Property(p => p.AdjustedQuoteSituation).HasColumnName("tx_curr_adjusted_situation");
            Property(p => p.PreviousAdjustedQuote).HasColumnName("vl_prev_adjusted");
            Property(p => p.PreviousAdjustedQuoteCurrency).HasColumnName("tx_prev_adjusted_ccy");
            Property(p => p.PreviousAdjustedQuoteTax).HasColumnName("vl_prev_adjusted_tax");
            Property(p => p.PreviousAdjustedQuoteTaxCurrency).HasColumnName("tx_prev_adjusted_ccy");
            Property(p => p.PreviousAdjustedQuoteSituation).HasColumnName("tx_prev_adjusted_situation");
            Property(p => p.OscillationPercentage).HasColumnName("vl_percentage_change");
            Property(p => p.VariationPoints).HasColumnName("vl_points_change");
            Property(p => p.VariationPointsCurrency).HasColumnName("tx_points_change_ccy");
            Property(p => p.EquivalentValue).HasColumnName("vl_equivalent_value");
            Property(p => p.EquivalentValueCurrency).HasColumnName("tx_equivalent_value_ccy");
            Property(p => p.AdjustedValueContract).HasColumnName("vl_curr_adjusted_contract");
            Property(p => p.AdjustedValueContractCurrency).HasColumnName("tx_curr_adjusted_contract_ccy");
            Property(p => p.MaximumTradeLimit).HasColumnName("vl_maximum_trade_limit");
            Property(p => p.MaximumTradeLimitCurrency).HasColumnName("tx_maximum_trade_limit_ccy");
            Property(p => p.MinimumTradeLimit).HasColumnName("vl_minimum_trade_limit");
            Property(p => p.MinimumTradeLimitCurrency).HasColumnName("tx_vl_minimum_trade_limit_ccy");
        }
    }

    public class B3HistoricMarketDataInfoEntityConfiguration : EntityTypeConfiguration<B3HistoricMarketDataInfo>
    {
        public B3HistoricMarketDataInfoEntityConfiguration()
        {
            ToTable("tb_b3_historic_market_data_info");
            HasKey(k => new { k.TradeDate, k.Ticker });

            Property(p => p.Type).HasColumnName("tp_record").IsRequired();
            Property(p => p.TradeDate).HasColumnName("dt_session").IsRequired();
            Property(p => p.Ticker).HasColumnName("tx_ticker").IsRequired();
            Property(p => p.ISIN).HasColumnName("tx_isin").IsRequired();
            Property(p => p.ShortName).HasColumnName("tx_shortname").IsRequired();
            Property(p => p.BDICode).HasColumnName("cd_bdi").IsRequired();            
            Property(p => p.MarketTypeCode).HasColumnName("cd_market_type").IsRequired();            
            Property(p => p.Specification).HasColumnName("tx_specification");
            Property(p => p.ForwardDaysToExpiry).HasColumnName("tx_forward_days_to_expiry");
            Property(p => p.ReferenceCurrency).HasColumnName("tx_reference_ccy");
            Property(p => p.Opening).HasColumnName("vl_open");
            Property(p => p.Maximum).HasColumnName("vl_high");
            Property(p => p.Minimum).HasColumnName("vl_low");
            Property(p => p.Average).HasColumnName("vl_average");
            Property(p => p.Last).HasColumnName("vl_close");
            Property(p => p.BestBidPrice).HasColumnName("vl_best_bid");
            Property(p => p.BestAskPrice).HasColumnName("vl_best_ask");
            Property(p => p.TradeQuantity).HasColumnName("vl_qty_transaction");
            Property(p => p.ContractQuantity).HasColumnName("vl_qty_contract");
            Property(p => p.FinancialVolume).HasColumnName("vl_fin_volume");
            Property(p => p.StrikePrice).HasColumnName("vl_strike");
            Property(p => p.DollarOptionPointsStrikePrice).HasColumnName("vl_strike_dollar_points");
            Property(p => p.StrikePriceCorrectionIndicator).HasColumnName("cd_strike_correction_indicator");
            Property(p => p.ExpiryDate).HasColumnName("dt_expiration");
            Property(p => p.QuoteFactor).HasColumnName("vl_quote_factor");            
            Property(p => p.InstrumentDistributionNumber).HasColumnName("cd_distribution_type");
        }
    }

}

