using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using B3Provider.Utils;

namespace B3ProviderTesting
{
    [TestClass]
    public class DateTimeExtensionTests
    {
        [TestMethod]
        public void TestingDateTimeExtension()
        {
            DateTime? now = DateTime.Now;

            var utilDates = now.UtilDates(null);

        }
    }
}
