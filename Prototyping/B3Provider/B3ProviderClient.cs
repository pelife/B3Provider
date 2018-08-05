#region License
/*
 * B3ProviderClient.cs
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

/// <summary>
/// Default namespace of the package
/// </summary>
namespace B3Provider
{
    using B3Provider.Records;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Class that provide data made available by Brazil Stock Market (B3 former BMF Bovespa)
    /// for aplications to use it.
    /// </summary>
    public class B3ProviderClient
    {
        #region "ctor"
        /// <summary>
        /// Defaulf constructor, it receives the path to where the files that are going to be downloaded from
        /// B3 (former BMF & Bovespa) are going will reside
        /// </summary>
        /// <param name="configuration"></param>
        public B3ProviderClient(B3ProviderConfig configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException("configuration", "the parameter configuration of type B3ProviderConfig cannot be null");
            _downloader = new B3Dowloader(_configuration.DownloadPath);
        }
        #endregion

        #region "private members"
        private B3ProviderConfig _configuration = null;
        private B3Dowloader _downloader = null;
        private bool _setupExecuted = false;
        #endregion

        #region "properties to records found in files"
        /// <summary>
        /// All equity information found in a file.
        /// </summary>
        public IList<B3EquityInfo> EquityInstruments { get; set; } = null;

        /// <summary>
        /// All options on equity found in a file.
        /// </summary>
        public IList<B3OptionOnEquityInfo> OptionInstruments { get; set; } = null;

        /// <summary>
        /// Current market data (princes in the same day of the instruments file)
        /// </summary>
        public IList<B3MarketDataInfo> CurrentMarketData { get; set; } = null;

        /// <summary>
        /// Historic market data past princes
        /// </summary>
        public IList<B3HistoricMarketDataInfo> HistoricMarketData { get; set; } = null;

        /// <summary>
        /// Index to convert an instrument ticker to internal ID
        /// </summary>
        public IDictionary<string, long> TickerIDIndex { get; private set; } = new Dictionary<string, long>();

        /// <summary>
        /// Colection of market sector classification to companies listed on B3 stock exchange. 
        /// </summary>
        public IList<B3SectorClassifcationInfo> SectorClassification { get; set; } = null;

        #endregion

        #region "public methods"
        /// <summary>
        /// It makes all the preparation in order to run operations on provider
        /// </summary>
        public void Setup()
        {
            SetupFileSystem();
            _setupExecuted = true;
        }

        /// <summary>
        /// Load all the instruments found in files
        /// </summary>
        public void LoadInstruments()
        {
            Dictionary<string, long> tickerIDIndexDictionary = null;

            SetupIfNotSetup();
            var filePath = _downloader.DownloadInstrumentFile(null, _configuration.ReplaceExistingFiles);

            var equityReader = ReaderFactory.CreateReader<B3EquityInfo>(_configuration.ReadStrategy);
            EquityInstruments = equityReader.ReadRecords(filePath);

            tickerIDIndexDictionary = EquityInstruments.ToDictionary(k => k.Ticker, v => v.B3ID.HasValue? v.B3ID.Value :0);
            TickerIDIndex = TickerIDIndex.Union(tickerIDIndexDictionary).ToDictionary(k => k.Key, v => v.Value);

            var optionsReader = ReaderFactory.CreateReader<B3OptionOnEquityInfo>(_configuration.ReadStrategy);
            OptionInstruments = optionsReader.ReadRecords(filePath);
            tickerIDIndexDictionary = OptionInstruments.ToDictionary(k => k.Ticker, v => v.B3ID.HasValue ? v.B3ID.Value : 0);
            TickerIDIndex = TickerIDIndex.Union(tickerIDIndexDictionary).ToDictionary(k => k.Key, v => v.Value);
        }

        /// <summary>
        /// Load all the quotes found  in files (for a specific date)
        /// </summary>
        public void LoadQuotes()
        {
            SetupIfNotSetup();
            var filePath = _downloader.DownloadQuoteFile(null, _configuration.ReplaceExistingFiles);

            var marketDataReader = ReaderFactory.CreateReader<B3MarketDataInfo>(_configuration.ReadStrategy);
            CurrentMarketData = marketDataReader.ReadRecords(filePath);
        }

        /// <summary>
        /// Load all the quotes found  in files (for a year file)
        /// </summary>
        /// <param name="yearToReadHistory">
        /// year of historic quotes file
        /// they are separated per year ans that's why we need to inform
        /// this parameter
        /// </param>
        public void LoadHistoricQuotes(int yearToReadHistory)
        {
            SetupIfNotSetup();
            var filePath = _downloader.DownloadYearHistoricFile(yearToReadHistory, _configuration.ReplaceExistingFiles);

            var historicMarketDataReader = ReaderFactory.CreateReader<B3HistoricMarketDataInfo>(_configuration.ReadStrategy);
            HistoricMarketData = historicMarketDataReader.ReadRecords(filePath);
        }

        /// <summary>
        /// Load all the company sector info found  in file
        /// </summary>
        public void LoadSectorClassification()
        {
            SetupIfNotSetup();
            var filePath = _downloader.DownloadSectorClassificationFile(_configuration.ReplaceExistingFiles);

            var sectorClassificationDataReader = ReaderFactory.CreateReader<B3SectorClassifcationInfo>(_configuration.ReadStrategy);
            SectorClassification = sectorClassificationDataReader.ReadRecords(filePath);
        }

        #endregion

        #region "private methods"
        /// <summary>
        /// Method that checks if provider is initialized and setup if it was not performed by calling
        /// code already.
        /// </summary>
        private void SetupIfNotSetup()
        {
            if (!_setupExecuted)
                Setup();
        }

        /// <summary>
        /// Creates the necessary directory to download files to.
        /// </summary>
        private void SetupFileSystem()
        {
            //if directory does exist, it will return directory info to that
            //if directory does not exist, it will be created
            Directory.CreateDirectory(_configuration.DownloadPath);            
        }

        #endregion

    }
}
