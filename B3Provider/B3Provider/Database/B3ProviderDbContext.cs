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
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    public class B3ProviderDbContext : DbContext
    {
        public DbSet<B3EquityInfo> EquityInstruments { get; set; }

        //public DbSet<B3OptionOnEquityInfo> OptionsOnEquityInstruments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.Configurations.Add(new B3SectorClassifcationInfoEntityConfiguration());
            modelBuilder.Configurations.Add(new B3EquityInfoEntityConfiguration());

            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<B3ProviderDbContext>(modelBuilder);            
            Database.SetInitializer(sqliteConnectionInitializer);

            //.ToTable("tb_b3_equity_info")
            //.Property(p => p.B3ID).HasColumnName("id_b3")
            //.Property(p => p.Ti).HasColumnName("tx_ticker")
            //.Property(p => p.B3ID).HasColumnName("id_b3")
            //.Property(p => p.B3ID).HasColumnName("id_b3")



            //base.OnModelCreating(modelBuilder);

            //create table `tb_b3_equity_info` 
            //(
            //	`id_b3`						integer unique,
            //	`tx_ticker`					text unique,
            //	`tx_isin`					text unique,
            //	`tx_company_name`			text,
            //	`tx_description`			text,
            //	`tx_trading_ccy`			text,
            //	`vl_market_capitalization`	real,
            //	`vl_last_price`				real,
            //	`dt_update`					text,
            //	primary key(id_b3)
            //);
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
            Property(p => p.CompanyListingCode).HasColumnName("tx_company_listing_code").IsRequired();
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
                        new IndexAttribute("ix_tx_ticker", 1) { IsUnique = true }));

            Property(p => p.ISIN).HasColumnName("tx_isin").IsRequired()
                .HasColumnAnnotation(
                        IndexAnnotation.AnnotationName,
                        new IndexAnnotation(
                            new IndexAttribute("ix_tx_isin", 2) { IsUnique = true }));

            Property(p => p.CompanyName).HasColumnName("tx_company_name").IsRequired();
            Property(p => p.Description).HasColumnName("tx_description").IsRequired();
            Property(p => p.Currency).HasColumnName("tx_trading_ccy").IsRequired();
            Property(p => p.MarketCapitalization).HasColumnName("vl_market_capitalization").IsRequired();
            Property(p => p.LastPrice).HasColumnName("vl_last_price").IsRequired();
            Property(p => p.LoadDate).HasColumnName("dt_update").IsRequired();
            Ignore(t => t.SectorClassification);

        }
    }
}

