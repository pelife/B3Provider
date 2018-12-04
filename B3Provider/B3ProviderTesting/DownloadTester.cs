using B3Provider;
using B3Provider.Records;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace B3ProviderTesting
{
    /// <summary>
    /// Summary description for DownloadTester
    /// </summary>
    [TestClass]
    public class DownloadTester
    {
        public DownloadTester()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void DownloadTest()
        {
            var logger = TestLogManager.Instance.GetLogger("B3ProviderTesting");
            //
            // TODO: Add test logic here
            //
            //var summary = BenchmarkRunner.Run<BenchmarkDownloader>();
            var stopWatch = System.Diagnostics.Stopwatch.StartNew();
            var downloader = new B3Dowloader(@"c:\temp");
            var finalPath = downloader.DownloadYearHistoricFile(2018, true);
            stopWatch.Stop();
            logger.Info(string.Format("downloaded in: {0:hh\\:mm\\:ss\\.fff}", stopWatch.Elapsed));
        }

        [TestMethod]
        public void ReadingFileTime()
        {
            var logger = TestLogManager.Instance.GetLogger("B3ProviderTesting");
            //
            // TODO: Add test logic here
            //
            //var summary = BenchmarkRunner.Run<BenchmarkDownloader>();
            var stopWatch = System.Diagnostics.Stopwatch.StartNew();
            var downloader = new B3Dowloader(@"c:\temp");
            var finalPath = @"c:\temp\COTAHIST_A2018.ZIP";
            var historicMarketDataReader = ReaderFactory.CreateReader<B3HistoricMarketDataInfo>(ReadStrategy.ZipFileReadMostRecent);
            var historicMarketData = historicMarketDataReader.ReadRecords(finalPath);
            stopWatch.Stop();
            logger.Info(string.Format("downloaded in: {0:hh\\:mm\\:ss\\.fff}", stopWatch.Elapsed));

        }

        [TestMethod]
        public void AggreegatingFileTime()
        {
            var logger = TestLogManager.Instance.GetLogger("B3ProviderTesting");
            //
            // TODO: Add test logic here
            //
            //var summary = BenchmarkRunner.Run<BenchmarkDownloader>();
            var stopWatch = System.Diagnostics.Stopwatch.StartNew();
            var downloader = new B3Dowloader(@"c:\temp");
            var finalPath = @"c:\temp\COTAHIST_A2018.ZIP";
            var historicMarketDataReader = ReaderFactory.CreateReader<B3HistoricMarketDataInfo>(ReadStrategy.ZipFileReadMostRecent);
            var historicMarketData = historicMarketDataReader.ReadRecords(finalPath);
            stopWatch.Stop();
            logger.Info(string.Format("downloaded in: {0:hh\\:mm\\:ss\\.fff}", stopWatch.Elapsed));

            stopWatch = System.Diagnostics.Stopwatch.StartNew();
            //if the map already exists, it should merge (2018, 2017, 2016)
            var history = historicMarketData.GroupBy(historic => historic.Ticker)
                                   .AsParallel()
                                   .ToDictionary(group => group.Key,
                                       components => components.GroupBy(c => c.TradeDate)
                                       .AsParallel()
                                       .ToDictionary(g => g.Key, g => g.FirstOrDefault()));
            stopWatch.Stop();
            logger.Info(string.Format("aggregated in: {0:hh\\:mm\\:ss\\.fff}", stopWatch.Elapsed));

            stopWatch = System.Diagnostics.Stopwatch.StartNew();
            //if the map already exists, it should merge (2018, 2017, 2016)
            var history2 = historicMarketData.GroupBy(historic => historic.Ticker)                        
                                   .ToDictionary(group => group.Key,
                                       components => components.GroupBy(c => c.TradeDate)                                       
                                       .ToDictionary(g => g.Key, g => g.FirstOrDefault()));
            stopWatch.Stop();
            logger.Info(string.Format("aggregated in: {0:hh\\:mm\\:ss\\.fff}", stopWatch.Elapsed));

        }
    }

    [SimpleJob(RunStrategy.ColdStart, targetCount: 5)]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    public class BenchmarkDownloader
    {
        [Benchmark]
        public void Download1()
        {
            var downloader = new B3Dowloader(@"c:\temp");
            downloader.DownloadYearHistoricFile(2018, true);
        }

        [Benchmark]
        public void Download2()
        {
            var downloader = new B3Dowloader(@"c:\temp");
            downloader.DownloadYearHistoricFile(2018, true);
        }

    }
}
