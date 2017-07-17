using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototyping.Code.Calc.Pricer
{
    public interface IPricer
    {
        void ProcessPath(ref double[] path);
        void Calculate();
        double Price();
    }

    public delegate double OneFactorPayoff(double spot, double strike);

    public abstract class Pricer : IPricer
    {
        protected OneFactorPayoff payoff; // delegate function for payoff calculation
        protected Func<double> discountFactor; // generic delegate function for discount factor
        protected double v; // option price
        protected long paths; // running counter
        //
        public Pricer(OneFactorPayoff payoff, Func<double> discountFactor)
        {
            this.payoff = payoff; this.discountFactor = discountFactor;
        }
        public abstract void ProcessPath(ref double[] path);
        public void Calculate()
        {
            // calculate discounted expectation
            v = (v / paths) * discountFactor();
        }
        public double Price()
        {
            // return option value
            return v;
        }
    }

    public class EuropeanPricer : Pricer
    {
        private double x; // option strike
        //
        public EuropeanPricer(OneFactorPayoff payoff, double x, Func<double> discountFactor)
            : base(payoff, discountFactor)
        {
            this.x = x;
        }
        public override void ProcessPath(ref double[] path)
        {
            // calculate payoff
            v += payoff(path[path.Length - 1], x);
            paths++;
        }
    }

    public enum ENUM_ASIAN_TYPE { average_price, average_strike }
    //
    public class ArithmeticAsianPricer : Pricer
    {
        private ENUM_ASIAN_TYPE asianType;
        private double x; // option strike
        private double averagePeriodStart; // time for starting averaging period
        private double t;
        private int steps;
        //
        public ArithmeticAsianPricer(OneFactorPayoff payoff, double x, Func<double> discountFactor,
            double t, double averagePeriodStart, int steps, ENUM_ASIAN_TYPE asianType)
            : base(payoff, discountFactor)
        {
            this.x = x;
            this.t = t;
            this.steps = steps;
            this.averagePeriodStart = averagePeriodStart;
            this.asianType = asianType;
        }
        public override void ProcessPath(ref double[] path)
        {
            double dt = t / steps;
            int timeCounter = -1;
            //
            // generic delegate for SkipWhile method to test if averaging period for an item has started
            Func<double, bool> timeTest = (double p) =>
            {
                timeCounter++;
                if ((dt * timeCounter) < averagePeriodStart) return true;
                return false;
            };
            //
            // calculate average price for averaging period
            double pathAverage = path.SkipWhile(timeTest).ToArray().Average();
            //
            // calculate payoff
            if (asianType == ENUM_ASIAN_TYPE.average_price) v += payoff(pathAverage, x);
            if (asianType == ENUM_ASIAN_TYPE.average_strike) v += payoff(path[path.Length - 1], pathAverage);
            paths++;
        }
    }

    public enum ENUM_BARRIER_TYPE { up_and_in, up_and_out, down_and_in, down_and_out }
    //
    public class BarrierPricer : Pricer
    {
        private double x; // option strike
        private double b; // barrier level
        private ENUM_BARRIER_TYPE barrierType;
        //
        public BarrierPricer(OneFactorPayoff payoff, double x, Func<double> discountFactor,
            double b, ENUM_BARRIER_TYPE barrierType) : base(payoff, discountFactor)
        {
            this.x = x;
            this.b = b;
            this.barrierType = barrierType;
        }
        public override void ProcessPath(ref double[] path)
        {
            // calculate payoff - check barrier breaches
            if ((barrierType == ENUM_BARRIER_TYPE.up_and_in) && (path.Max() > b)) v += payoff(path[path.Length - 1], x);
            if ((barrierType == ENUM_BARRIER_TYPE.up_and_out) && (path.Max() < b)) v += payoff(path[path.Length - 1], x);
            if ((barrierType == ENUM_BARRIER_TYPE.down_and_in) && (path.Min() < b)) v += payoff(path[path.Length - 1], x);
            if ((barrierType == ENUM_BARRIER_TYPE.down_and_out) && (path.Min() > b)) v += payoff(path[path.Length - 1], x);
            paths++;
        }
    }
}
