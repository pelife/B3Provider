using CenterSpace.NMath.Core;
using System;
using System.Linq;

namespace Prototyping.Code.Calc.Random
{
    public interface IRandomGenerator
    {
        // method for generating normally distributed random variable
        double GetRandom();
    }
    public abstract class RandomGenerator : IRandomGenerator
    {
        // abstract class implementing IRandomGenerator interface
        public abstract double GetRandom();
    }
    public class NormalApproximation : RandomGenerator
    {
        // concrete implementation for normal random variable approximation
        // normRand = sum of 12 independent uniformly disctibuted random numbers, minus 6
        private System.Random random;
        public NormalApproximation()
        {
            random = new System.Random();
        }
        public override double GetRandom()
        {
            // implementation uses C# uniform random generator
            double[] rnd = new double[12];
            Func<double> generator = () => { return random.NextDouble(); };
            return rnd.Select(r => generator()).Sum() - 6.0;
        }
    }

    public class MersenneTwisterRandomGenerator : RandomGenerator
    {

        // Change the uniform deviate generator to use the method NextDouble() form
        // NMath Cores RandGenMTwist class.
        const int seed = 0x124;
        const int trials = 2000;
        const double prob = 0.002;

        // Construct the MT generator with the given seed.
        private RandomNumberGenerator mt = null;
        private RandomNumberGenerator.UniformRandomNumber unrn = null;
        private RandGenBinomial binGen = null;

        public MersenneTwisterRandomGenerator()
        {
            mt = new RandGenMTwist();
            // Create the delegate.
            unrn = new RandomNumberGenerator.UniformRandomNumber(mt.NextDouble);
            binGen = new RandGenBinomial(trials, prob, unrn);
        }

        public override double GetRandom()
        {
            double[] rnd = new double[12];
            Func<double> generator = () => { return mt.NextDouble(); };
            return rnd.Select(r => generator()).Sum() - 6.0;
        }
    }

}
