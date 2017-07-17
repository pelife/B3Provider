using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prototyping.Code.Calc.Pricer;
using Prototyping.Code.Calc.MonteCarlo;
using ExcelDna.Integration;
using Prototyping.Code.Calc.Builder;
using Common.Logging;

namespace Prototyping.Code.Calc.Runner
{

    public static class ConsolePricer
    {

        private static ILog logger = LogManager.GetLogger("ConsolePricer");

        private static dynamic Excel;
        private static Dictionary<string, Pricer.Pricer> pricer;
        private static MonteCarloEngine engine;
        private static OneFactorPayoff callPayoff;
        private static OneFactorPayoff putPayoff;
        private static Func<double> discountFactor;
        //
        public static void run()
        {
            try
            {
                int steps = 0;
                long paths = 0;
                double v = 0;
                double r = 0;
                double t = 0;
                double x = 0.0;
                double s = 0.0;
                double q = 0.0;
                double averagePeriodStart = 0.0;                
                double upperBarrier = 0.0;                
                double lowerBarrier = 0.0;
                string value = string.Empty;

                System.Console.WriteLine("enter steps:");
                value = System.Console.ReadLine();
                steps = int.Parse(value);

                System.Console.WriteLine("enter paths:");
                value = System.Console.ReadLine();
                paths = long.Parse(value);

                System.Console.WriteLine("enter rate:");
                value = System.Console.ReadLine();
                r = double.Parse(value);

                System.Console.WriteLine("enter dividend yield:");
                value = System.Console.ReadLine();
                q = double.Parse(value);

                System.Console.WriteLine("enter maturity:");
                value = System.Console.ReadLine();
                t = double.Parse(value);
                
                System.Console.WriteLine("enter averagingPeriod:");
                value = System.Console.ReadLine();
                averagePeriodStart = double.Parse(value);

                System.Console.WriteLine("enter upperBarrier:");
                value = System.Console.ReadLine();
                upperBarrier = double.Parse(value);

                System.Console.WriteLine("enter lowerBarrier:");
                value = System.Console.ReadLine();
                lowerBarrier = double.Parse(value);

                System.Console.WriteLine("enter strike:");
                value = System.Console.ReadLine();
                x = double.Parse(value);

                System.Console.WriteLine("enter spot:");
                value = System.Console.ReadLine();
                s = double.Parse(value);

                System.Console.WriteLine("enter volatility:");
                value = System.Console.ReadLine();
                v = double.Parse(value);


                //
                // create Monte Carlo engine, payoff functions and discounting factor
                var builder = new ConsoleBuilder(s, t, r, q, v);
                engine = new MonteCarloEngine(builder, paths, steps);
                callPayoff = (double spot, double strike) => Math.Max(0.0, spot - strike);
                putPayoff = (double spot, double strike) => Math.Max(0.0, strike - spot);
                discountFactor = () => Math.Exp(-r * t);
                //
                // create pricers into dictionary
                pricer = new Dictionary<string, Pricer.Pricer>();
                pricer.Add("Vanilla call", new EuropeanPricer(callPayoff, x, discountFactor));
                pricer.Add("Vanilla put", new EuropeanPricer(putPayoff, x, discountFactor));
                pricer.Add("Asian average price call", new ArithmeticAsianPricer(callPayoff, x, discountFactor, t, averagePeriodStart, steps, ENUM_ASIAN_TYPE.average_price));
                pricer.Add("Asian average price put", new ArithmeticAsianPricer(putPayoff, x, discountFactor, t, averagePeriodStart, steps, ENUM_ASIAN_TYPE.average_price));
                pricer.Add("Asian average strike call", new ArithmeticAsianPricer(callPayoff, x, discountFactor, t, averagePeriodStart, steps, ENUM_ASIAN_TYPE.average_strike));
                pricer.Add("Asian average strike put", new ArithmeticAsianPricer(putPayoff, x, discountFactor, t, averagePeriodStart, steps, ENUM_ASIAN_TYPE.average_strike));
                pricer.Add("Up-and-in barrier call", new BarrierPricer(callPayoff, x, discountFactor, upperBarrier, ENUM_BARRIER_TYPE.up_and_in));
                pricer.Add("Up-and-out barrier call", new BarrierPricer(callPayoff, x, discountFactor, upperBarrier, ENUM_BARRIER_TYPE.up_and_out));
                pricer.Add("Down-and-in barrier call", new BarrierPricer(callPayoff, x, discountFactor, lowerBarrier, ENUM_BARRIER_TYPE.down_and_in));
                pricer.Add("Down-and-out barrier call", new BarrierPricer(callPayoff, x, discountFactor, lowerBarrier, ENUM_BARRIER_TYPE.down_and_out));
                pricer.Add("Up-and-in barrier put", new BarrierPricer(putPayoff, x, discountFactor, upperBarrier, ENUM_BARRIER_TYPE.up_and_in));
                pricer.Add("Up-and-out barrier put", new BarrierPricer(putPayoff, x, discountFactor, upperBarrier, ENUM_BARRIER_TYPE.up_and_out));
                pricer.Add("Down-and-in barrier put", new BarrierPricer(putPayoff, x, discountFactor, lowerBarrier, ENUM_BARRIER_TYPE.down_and_in));
                pricer.Add("Down-and-out barrier put", new BarrierPricer(putPayoff, x, discountFactor, lowerBarrier, ENUM_BARRIER_TYPE.down_and_out));
                //
                // order path updates for all pricers from engine
                foreach (KeyValuePair<string, Pricer.Pricer> kvp in pricer) engine.sendPath += kvp.Value.ProcessPath;
                //
                // order process stop notification for all pricers from engine
                foreach (KeyValuePair<string, Pricer.Pricer> kvp in pricer) engine.stopProcess += kvp.Value.Calculate;
                //
                // run Monte Carlo engine
                engine.Run();
                //
                // print option types to Excel
                string[] optionTypes = pricer.Keys.ToArray();
                //Excel.Range["_options"] = Excel.WorksheetFunction.Transpose(optionTypes);
                //
                // print option prices to Excel
                double[] optionPrices = new double[pricer.Count];
                for (int i = 0; i < pricer.Count; i++)
                {
                    optionPrices[i] = pricer.ElementAt(i).Value.Price();
                    Console.WriteLine(String.Format("{0}:{1}", optionTypes[i], optionPrices[i]));
                } 
                                
            }
            catch (Exception e)
            {
                logger.Error("erro running", e);
            }
        }
    }

    public static class MCPricer
    {

        private static ILog logger = LogManager.GetLogger("MCPricer");

        private static dynamic Excel;
        private static Dictionary<string, Pricer.Pricer> pricer;
        private static MonteCarloEngine engine;
        private static OneFactorPayoff callPayoff;
        private static OneFactorPayoff putPayoff;
        private static Func<double> discountFactor;
        //
        public static void run()
        {
            try
            {
                // create Excel application
                Excel = ExcelDnaUtil.Application;
                //
                // fetch pricing parameters from named Excel ranges
                int steps = (int)Excel.Range("_steps").Value2;
                long paths = (long)Excel.Range("_paths").Value2;
                double r = (double)Excel.Range("_rate").Value2;
                double t = (double)Excel.Range("_maturity").Value2;
                double averagePeriodStart = (double)Excel.Range("_averagingPeriod").Value2;
                double upperBarrier = (double)Excel.Range("_upperBarrier").Value2;
                double lowerBarrier = (double)Excel.Range("_lowerBarrier").Value2;
                double x = (double)Excel.Range("_strike").Value2;
                //
                // create Monte Carlo engine, payoff functions and discounting factor
                engine = new MonteCarloEngine(new ExcelBuilder(), paths, steps);
                callPayoff = (double spot, double strike) => Math.Max(0.0, spot - strike);
                putPayoff = (double spot, double strike) => Math.Max(0.0, strike - spot);
                discountFactor = () => Math.Exp(-r * t);
                //
                // create pricers into dictionary
                pricer = new Dictionary<string, Pricer.Pricer>();
                pricer.Add("Vanilla call", new EuropeanPricer(callPayoff, x, discountFactor));
                pricer.Add("Vanilla put", new EuropeanPricer(putPayoff, x, discountFactor));
                pricer.Add("Asian average price call", new ArithmeticAsianPricer(callPayoff, x, discountFactor, t, averagePeriodStart, steps, ENUM_ASIAN_TYPE.average_price));
                pricer.Add("Asian average price put", new ArithmeticAsianPricer(putPayoff, x, discountFactor, t, averagePeriodStart, steps, ENUM_ASIAN_TYPE.average_price));
                pricer.Add("Asian average strike call", new ArithmeticAsianPricer(callPayoff, x, discountFactor, t, averagePeriodStart, steps, ENUM_ASIAN_TYPE.average_strike));
                pricer.Add("Asian average strike put", new ArithmeticAsianPricer(putPayoff, x, discountFactor, t, averagePeriodStart, steps, ENUM_ASIAN_TYPE.average_strike));
                pricer.Add("Up-and-in barrier call", new BarrierPricer(callPayoff, x, discountFactor, upperBarrier, ENUM_BARRIER_TYPE.up_and_in));
                pricer.Add("Up-and-out barrier call", new BarrierPricer(callPayoff, x, discountFactor, upperBarrier, ENUM_BARRIER_TYPE.up_and_out));
                pricer.Add("Down-and-in barrier call", new BarrierPricer(callPayoff, x, discountFactor, lowerBarrier, ENUM_BARRIER_TYPE.down_and_in));
                pricer.Add("Down-and-out barrier call", new BarrierPricer(callPayoff, x, discountFactor, lowerBarrier, ENUM_BARRIER_TYPE.down_and_out));
                pricer.Add("Up-and-in barrier put", new BarrierPricer(putPayoff, x, discountFactor, upperBarrier, ENUM_BARRIER_TYPE.up_and_in));
                pricer.Add("Up-and-out barrier put", new BarrierPricer(putPayoff, x, discountFactor, upperBarrier, ENUM_BARRIER_TYPE.up_and_out));
                pricer.Add("Down-and-in barrier put", new BarrierPricer(putPayoff, x, discountFactor, lowerBarrier, ENUM_BARRIER_TYPE.down_and_in));
                pricer.Add("Down-and-out barrier put", new BarrierPricer(putPayoff, x, discountFactor, lowerBarrier, ENUM_BARRIER_TYPE.down_and_out));
                //
                // order path updates for all pricers from engine
                foreach (KeyValuePair<string, Pricer.Pricer> kvp in pricer) engine.sendPath += kvp.Value.ProcessPath;
                //
                // order process stop notification for all pricers from engine
                foreach (KeyValuePair<string, Pricer.Pricer> kvp in pricer) engine.stopProcess += kvp.Value.Calculate;
                //
                // run Monte Carlo engine
                engine.Run();
                //
                // print option types to Excel
                string[] optionTypes = pricer.Keys.ToArray();
                Excel.Range["_options"] = Excel.WorksheetFunction.Transpose(optionTypes);
                //
                // print option prices to Excel
                double[] optionPrices = new double[pricer.Count];
                for (int i = 0; i < pricer.Count; i++) optionPrices[i] = pricer.ElementAt(i).Value.Price();
                Excel.Range["_prices"] = Excel.WorksheetFunction.Transpose(optionPrices);
            }
            catch (Exception e)
            {
                logger.Error("erro running", e);
            }
        }
    }
}
