using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototyping.Code.Calc.StochasticDifferentialEquation
{
    /// <summary>
    /// SDE - Stochastic Differential Equation
    /// Interface ISDE defines methods for retrieving drift and diffusion terms for a given S and t
    /// </summary>
    public interface ISDE
    {
        // methods for calculating drift and diffusion term of stochastic differential equation
        double Drift(double s, double t);
        double Diffusion(double s, double t);
    }

    public abstract class SDE : ISDE
    {
        public abstract double Diffusion(double s, double t);
        public abstract double Drift(double s, double t);
        
    }

    public class GBM : SDE
    {
        private double r; // risk-free rate
        private double q; // dividend yield
        private double v; // volatility
        //
        public GBM(double r, double q, double v)
        {
            this.r = r;
            this.q = q;
            this.v = v;
        }

        public override double Diffusion(double s, double t)
        {
            return (r - q) * s;
        }

        public override double Drift(double s, double t)
        {
            return v * s;
        }
    }
}
