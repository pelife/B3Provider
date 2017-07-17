using ExcelDna.Integration;
using Prototyping.Code.Calc.Random;
using Prototyping.Code.Calc.StochasticDifferentialEquation;
using System;
using Prototyping.Code.Calc.Discretization;

namespace Prototyping.Code.Calc.Builder
{
    public interface IBuilder
    {
        // method for creating all the needed objects for asset price simulations
        Tuple<SDE, Discretization.Discretization, RandomGenerator> Build();
    }

    public abstract class Builder : IBuilder
    {
        // abstract class implementing IBuilder interface
        public abstract Tuple<SDE, Discretization.Discretization, RandomGenerator> Build();
    }

    public class ExcelBuilder : Builder
    {
        private dynamic Excel = ExcelDnaUtil.Application;
        //
        public override Tuple<SDE, Discretization.Discretization, RandomGenerator> Build()
        {
            // build all objects needed for asset path simulations
            SDE sde = BuildSDE();
            Discretization.Discretization discretization = BuildDiscretization(sde);
            RandomGenerator randomGenerator = BuildRandomGenerator();
            return new Tuple<SDE, Discretization.Discretization, RandomGenerator>(sde, discretization, randomGenerator);
        }
        private SDE BuildSDE()
        {
            SDE sde = null;
            string sdeType = (string)Excel.Range("_stochasticModel").Value;
            //
            if (sdeType == "GBM")
            {
                double r = (double)Excel.Range("_rate").Value2;
                double q = (double)Excel.Range("_yield").Value2;
                double v = (double)Excel.Range("_volatility").Value2;
                sde = new GBM(r, q, v);
            }
            // insert new stochastic model choices here
            return sde;
        }
        private Discretization.Discretization BuildDiscretization(SDE sde)
        {
            Discretization.Discretization discretization = null;
            string discretizationType = (string)Excel.Range("_discretization").Value;
            //
            if (discretizationType == "EULER")
            {
                double initialPrice = (double)Excel.Range("_spot").Value2;
                double expiration = (double)Excel.Range("_maturity").Value2;
                discretization = new Discretization.EulerDiscretization(sde, initialPrice, expiration);
            }
            // insert new discretization scheme choices here
            return discretization;
        }
        private RandomGenerator BuildRandomGenerator()
        {
            RandomGenerator randomGenerator = null;
            string randomGeneratorType = (string)Excel.Range("_randomGenerator").Value;
            //
            if (randomGeneratorType == "CLT")
            {
                randomGenerator = new NormalApproximation();
            }
            // insert new random generator choices here
            return randomGenerator;
        }
    }

    public class ConsoleBuilder : Builder
    {
        private double? _initialPrice = null;
        private double? _expiration = null;
        private double? _r = null;  // risk-free rate
        private double? _q = null;  // dividend yield
        private double? _v = null;  // volatility

        private RandomGenerator BuildRandomGenerator()
        {
            RandomGenerator randomGenerator = null;

            randomGenerator = new MersenneTwisterRandomGenerator();

            // insert new random generator choices here
            return randomGenerator;
        }

        public ConsoleBuilder()
        {
        }

        public ConsoleBuilder(double initialPrice,
                                double expiration,
                                double r,
                                double q,
                                double v)
        {
            this._initialPrice = initialPrice;
            this._expiration = expiration;
            this._r = r;
            this._q = q;
            this._v = v;
        }

        private Discretization.Discretization BuildDiscretization(SDE sde)
        {
            Discretization.Discretization discretization = null;

            string value = string.Empty;

            if (!_initialPrice.HasValue)
            {
                System.Console.WriteLine("enter spot:");
                value = System.Console.ReadLine();
                _initialPrice = double.Parse(value);
            }

            if (!_expiration.HasValue)
            {
                System.Console.WriteLine("enter maturity:");
                value = System.Console.ReadLine();
                _expiration = double.Parse(value);
            }

            discretization = new Discretization.EulerDiscretization(sde, _initialPrice.Value, _expiration.Value); // quando tiver outros discretos, perguntar

            return discretization;
        }

        private SDE BuildSDE()
        {
            string value = string.Empty;
            SDE sde = null;

            if (!_r.HasValue)
            {
                System.Console.WriteLine("enter rate:");
                value = System.Console.ReadLine();
                _r = double.Parse(value);
            }

            if (!_q.HasValue)
            {
                System.Console.WriteLine("enter yield:");
                value = System.Console.ReadLine();
                _q = double.Parse(value);
            }

            if (!_r.HasValue)
            {
                System.Console.WriteLine("enter volatility:");
                value = System.Console.ReadLine();
                _r = double.Parse(value);
            }

            sde = new GBM(_r.Value, _q.Value, _v.Value); // quando tiver outros sde, perguntar ao usuário

            return sde;
        }

        public override Tuple<SDE, Discretization.Discretization, RandomGenerator> Build()
        {
            // build all objects needed for asset path simulations
            SDE sde = BuildSDE();
            Discretization.Discretization discretization = BuildDiscretization(sde);
            RandomGenerator randomGenerator = BuildRandomGenerator();
            return new Tuple<SDE, Discretization.Discretization, RandomGenerator>(sde, discretization, randomGenerator);
        }
    }
}
