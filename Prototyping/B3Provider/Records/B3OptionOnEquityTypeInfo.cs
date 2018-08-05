using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B3Provider.Records
{
    /// <summary>
    /// Enumeration that indicates the type of Option Call or Put
    /// </summary>
    public enum B3OptionOnEquityTypeInfo : short
    {
        /// <summary>
        /// Call option is an obligation to sell at a price to call issuer.
        /// </summary>
        Call = 1

       /// <summary>
       /// Put option is an obligation to buy at a price to call issuer.
       /// </summary>
       , Put = -1
    }
}
