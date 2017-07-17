using Prototyping.Code.Calc.StochasticDifferentialEquation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototyping.Code.Calc.Discretization
{
    public interface IDiscretization
    {
        // method for discretizing stochastic differential equation
        double Next(double s, double t, double dt, double rnd);
    }

    public abstract class Discretization : IDiscretization
    {
        // abstract class implementing IDiscretization interface
        protected SDE sde;
        protected double initialPrice;
        protected double expiration;
        //
        // read-only properties for initial price and expiration
        public double InitialPrice { get { return initialPrice; } }
        public double Expiration { get { return expiration; } }
        public Discretization(SDE sde, double initialPrice, double expiration)
        {
            this.sde = sde;
            this.initialPrice = initialPrice;
            this.expiration = expiration;
        }
        public abstract double Next(double s, double t, double dt, double rnd);
    }

    public class EulerDiscretization : Discretization
    {
        // concrete implementation for Euler discretization scheme
        public EulerDiscretization(SDE sde, double initialPrice, double expiration)
            : base(sde, initialPrice, expiration) { }
        public override double Next(double s, double t, double dt, double rnd)
        {
            return s + sde.Drift(s, t) * dt + sde.Diffusion(s, t) * Math.Sqrt(dt) * rnd;
        }
    }
}
