using Prototyping.Code.Calc.Random;
using Prototyping.Code.Calc.StochasticDifferentialEquation;
using Prototyping.Code.Calc.Builder;
using System;

namespace Prototyping.Code.Calc.MonteCarlo
{
    public delegate void PathSender(ref double[] path);
    public delegate void ProcessStopper();
    //
    public class MonteCarloEngine
    {
        private SDE sde;
        private Discretization.Discretization discretization;
        private RandomGenerator randomGenerator;
        private long paths;
        private int steps;

        public Discretization.Discretization Discretization { get => discretization; set => discretization = value; }

        public event PathSender sendPath;
        public event ProcessStopper stopProcess;
        //
        public MonteCarloEngine(Builder.Builder builder, long paths, int steps)
        {
            Tuple<SDE, Discretization.Discretization, RandomGenerator> parts = builder.Build();
            sde = parts.Item1;
            Discretization = parts.Item2;
            randomGenerator = parts.Item3;
            this.paths = paths;
            this.steps = steps;
        }
        public void Run()
        {
            double[] path = new double[steps + 1];
            double dt = Discretization.Expiration / steps;
            double vOld = 0.0; double vNew = 0.0;
            //
            for (int i = 0; i < paths; i++)
            {
                path[0] = vOld = Discretization.InitialPrice;
                //
                for (int j = 1; j <= steps; j++)
                {
                    // get next value using discretization scheme
                    vNew = Discretization.Next(vOld, (dt * j), dt, randomGenerator.GetRandom());
                    path[j] = vNew; vOld = vNew;
                }
                sendPath(ref path); // send one simulated path to pricer to be processed
            }
            stopProcess(); // simulation ends - notify pricer
        }
    }
}
