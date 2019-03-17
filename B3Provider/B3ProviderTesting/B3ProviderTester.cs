#region License
/*
 * B3ProviderTester.cs
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

namespace B3ProviderTesting
{
    using B3Provider;
    using B3Provider.Records;
    using B3Provider.Utils;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NLog;
    using System;
    using System.Linq;


    //using NLog;

    [TestClass]
    public class B3ProviderTester
    {
        [TestMethod]
        public void B3ProviderMustDownloadInstrumentFiles()
        {
            var config = new B3Provider.B3ProviderConfig();
            config.ReplaceExistingFiles = true;

            var client = new B3Provider.B3ProviderClient(config);
            client.LoadInstruments();

            Assert.IsNotNull(client.EquityInstruments);
            Assert.IsNotNull(client.OptionInstruments);
            Assert.IsNotNull(client.FutureInstruments);

            Assert.AreNotEqual(0, client.EquityInstruments.Count);
            Assert.AreNotEqual(0, client.OptionInstruments.Count);
            Assert.AreNotEqual(0, client.FutureInstruments.Count);            
        }

        [TestMethod]
        public void B3ProviderMustDownloadQuoteFiles()
        {
            var config = new B3Provider.B3ProviderConfig();
            config.ReplaceExistingFiles = true;

            var client = new B3Provider.B3ProviderClient(config);
            client.LoadQuotes();

            // get information about PETR4 stock (the most popular in B3)
            var petr4 = client.CurrentMarketData.Where(e =>
                e.Ticker.Equals("PETR4", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

            var usim5 = client.CurrentMarketData.Where(e =>
                e.Ticker.Equals("USIM5", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

            var bbdc4 = client.CurrentMarketData.Where(e =>
                e.Ticker.Equals("BBDC4", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
        }

        [TestMethod]
        public void B3ProviderMustDownloadHistoricQuoteFiles()
        {
            var config = new B3Provider.B3ProviderConfig();
            config.ReplaceExistingFiles = true;

            var client = new B3Provider.B3ProviderClient(config);
            client.LoadHistoricQuotes(2018);

            var quotesPETR4 = client.GetHistoricMarketData().Where(e => e.Ticker.Equals("PETR4")).OrderByDescending(e => e.TradeDate).ToList();
        }

        [TestMethod]
        public void B3ProviderMustDownloadHistoricQuoteFilesGetUtilDates()
        {
            Logger logger = TestLogManager.Instance.GetLogger("B3ProviderTester");

            logger.Info("teste");


            var config = new B3Provider.B3ProviderConfig();
            config.ReplaceExistingFiles = true;

            var client = new B3Provider.B3ProviderClient(config);
            logger.Info("inicio 2018");
            client.LoadHistoricQuotes(2018);
            logger.Info("fim 2018");
            client.LoadHistoricQuotes(2017);

            DateTime? utilDatesStart = null;
            var utilDates = utilDatesStart.UtilDates(null);
            var quotesPETR4 = client.GetHistoricMarketData().Where(e => e.Ticker.Equals("PETR4")).OrderByDescending(e => e.TradeDate).ToList();

            var currentQuote = quotesPETR4.Where(e => e.TradeDate == utilDates.CurrentDate).FirstOrDefault();
            var oneDayQuote = quotesPETR4.Where(e => e.TradeDate == utilDates.OneDayDate).FirstOrDefault();
            var oneWeekQuote = quotesPETR4.Where(e => e.TradeDate == utilDates.OneWeekDate).FirstOrDefault();
            var oneMonthQuote = quotesPETR4.Where(e => e.TradeDate == utilDates.OneMonthDate).FirstOrDefault();
            var oneQuarteQuote = quotesPETR4.Where(e => e.TradeDate == utilDates.OneQuarterDate).FirstOrDefault();
            var oneYearQuote = quotesPETR4.Where(e => e.TradeDate == utilDates.OneYearDate).FirstOrDefault();

            var varDiaria = ((currentQuote?.Last ?? 1) / (oneDayQuote?.Last ?? 1)) - 1;
            var varSemanal = ((currentQuote?.Last ?? 1) / (oneWeekQuote?.Last ?? 1)) - 1;
            var varMensal = ((currentQuote?.Last ?? 1) / (oneMonthQuote?.Last ?? 1)) - 1;
            var varTrimestral = ((currentQuote?.Last ?? 1) / (oneQuarteQuote?.Last ?? 1)) - 1;
            var varAnual = ((currentQuote?.Last ?? 1) / (oneYearQuote?.Last ?? 1)) - 1;

            var WTDQuote = quotesPETR4.Where(e => e.TradeDate == utilDates.WTDDate).FirstOrDefault();
            var MTDQuote = quotesPETR4.Where(e => e.TradeDate == utilDates.MTDDate).FirstOrDefault();
            var QTDQuote = quotesPETR4.Where(e => e.TradeDate == utilDates.QTDDate).FirstOrDefault();
            var YTDQuote = quotesPETR4.Where(e => e.TradeDate == utilDates.YTDDate).FirstOrDefault();

        }

        [TestMethod]
        public void B3ProviderMustDownloadHistoricQuoteFilesFromMultipleYears()
        {
            var logger = TestLogManager.Instance.GetLogger("B3ProviderTesting");
            var config = new B3ProviderConfig();
            config.ReplaceExistingFiles = true;

            var client = new B3ProviderClient(config);
            var stopWatch = new System.Diagnostics.Stopwatch();
            var finalPath = string.Empty;

            stopWatch.Start();
            client.LoadHistoricQuotes(2018);
            stopWatch.Stop();
            logger.Info(string.Format("downloaded in: {0:hh\\:mm\\:ss\\.fff}", stopWatch.Elapsed));
            stopWatch.Reset();
            stopWatch.Start();
            client.LoadHistoricQuotes(2017);
            stopWatch.Stop();
            logger.Info(string.Format("downloaded in: {0:hh\\:mm\\:ss\\.fff}", stopWatch.Elapsed));
            stopWatch.Reset();
            stopWatch.Start();
            client.LoadHistoricQuotes(2016);
            stopWatch.Stop();
            logger.Info(string.Format("downloaded in: {0:hh\\:mm\\:ss\\.fff}", stopWatch.Elapsed));


        }

        [TestMethod]
        public void B3ProviderMustFindOptions()
        {
            // create a configuration instance
            var config = new B3ProviderConfig();

            // define properties
            config.ReplaceExistingFiles = true;

            // create an instance of the client
            var client = new B3ProviderClient(config);

            // load all instruments into memory
            client.LoadInstruments();

            // load all instruments into memory
            client.LoadQuotes();

            // load all instruments into memory
            client.LoadHistoricQuotes(2018);

            // get information about PETR4 stock (the most popular in B3)
            var equity = client.EquityInstruments.Where(e =>
                e.Ticker.Equals("PETR4", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

            // get information about option calls on PETR4 stock 
            var optionsCalls = client.OptionInstruments.Where(o => o.B3IDUnderlying == equity.B3ID
                && o.Type == B3OptionOnEquityTypeInfo.Call).ToList();

            var historicQuotes = client.GetHistoricMarketData().Where(md => md.Ticker == optionsCalls.FirstOrDefault().Ticker).ToList();

            // get information about option puts on PETR4 stock 
            var optionsPuts = client.OptionInstruments.Where(o => o.B3IDUnderlying == equity.B3ID
                && o.Type == B3OptionOnEquityTypeInfo.Put).ToList();
        }

        [TestMethod]
        public void AggreegatingFilesDownload()
        {
            System.Diagnostics.Stopwatch stopWatch = null;

            var logger = TestLogManager.Instance.GetLogger("B3ProviderTesting");

            //var summary = BenchmarkRunner.Run<BenchmarkDownloader>();
            

            // create a configuration instance
            var config = new B3ProviderConfig();

            // define properties
            config.ReplaceExistingFiles = true;

            // create an instance of the client
            var client = new B3ProviderClient(config);

            stopWatch = System.Diagnostics.Stopwatch.StartNew();
            client.LoadHistoricQuotes(2018);
            stopWatch.Stop();
            logger.Info(string.Format("loaded in: {0:hh\\:mm\\:ss\\.fff}", stopWatch.Elapsed));
            stopWatch = System.Diagnostics.Stopwatch.StartNew();
            client.LoadHistoricQuotes(2017);
            stopWatch.Stop();
            logger.Info(string.Format("loaded in: {0:hh\\:mm\\:ss\\.fff}", stopWatch.Elapsed));
            stopWatch = System.Diagnostics.Stopwatch.StartNew();
            client.LoadHistoricQuotes(2016);
            stopWatch.Stop();
            logger.Info(string.Format("loaded in: {0:hh\\:mm\\:ss\\.fff}", stopWatch.Elapsed));

            

        }

        [TestMethod]
        public void B3ProviderMustSectorClassification()
        {
            var config = new B3ProviderConfig();

            // define properties
            config.ReplaceExistingFiles = true;

            // create an instance of the client
            var client = new B3ProviderClient(config);

            // load all instruments into memory
            client.LoadSectorClassification();

            Assert.IsNotNull(client.SectorClassification);

            Assert.AreNotEqual(0, client.SectorClassification.Count);
        }


    }
}
