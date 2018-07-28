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
    using System;
    using System.Linq;
    using B3Provider;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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

            Assert.AreNotEqual(0, client.EquityInstruments.Count);
            Assert.AreNotEqual(0, client.OptionInstruments.Count);
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

            // get information about PETR4 stock (the most popular in B3)
            var equity = client.EquityInstruments.Where(e => 
                e.Ticker.Equals("PETR4", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

            // get information about option calls on PETR4 stock 
            var optionsCalls = client.OptionInstruments.Where(o => o.B3IDUnderlying == equity.B3ID 
                && o.Type == B3OptionOnEquityTypeInfo.Call).ToList();

            // get information about option puts on PETR4 stock 
            var optionsPuts = client.OptionInstruments.Where(o => o.B3IDUnderlying == equity.B3ID 
                && o.Type == B3OptionOnEquityTypeInfo.Put).ToList();
        }



    }
}
