using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace B3ProviderTesting
{
    [TestClass]
    public class B3ProviderTester
    {
        [TestMethod]
        public void B3ProviderMustDownloadFiles()
        {
            var config = new B3Provider.B3ProviderConfig();
            config.ReplaceExistingFiles = true;

            var client = new B3Provider.B3ProviderClient(config);
            client.LoadInstruments();
            client.LoadQuotes();


        }
    }
}
