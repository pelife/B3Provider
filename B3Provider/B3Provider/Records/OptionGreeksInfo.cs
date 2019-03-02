using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B3Provider.Records
{
    public class OptionGreeksInfo
    {
        double Delta { get; set; }
        double Gama { get; set; }
        double Vega { get; set; }
        double Theta { get; set; }
        double Rho { get; set; }
    }
}
