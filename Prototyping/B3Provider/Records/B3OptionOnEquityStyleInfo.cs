using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B3Provider.Records
{
    /// <summary>
    /// Enumeration that indicates the Exercise Style of Option American or European
    /// </summary>
    public enum B3OptionOnEquityStyleInfo : short
    {
        /// <summary>
        /// Can be exercised any time prior to the expiration date
        /// </summary>
        American = 1

       /// <summary>
       /// Can be exercised only in the expiration date
       /// </summary>
       , European = 2
    }
}
