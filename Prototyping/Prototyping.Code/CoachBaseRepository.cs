using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Couchbase.Management;
using Common.Logging;


namespace Prototyping.Code
{
    public class CoachBaseRepository
    {
        ILog logger = LogManager.GetCurrentClassLogger();

        public void HelloWorld()
        {
            logger.Info("inicio Hello World");

            logger.Info("fim Hello World");
        }

    }
}
