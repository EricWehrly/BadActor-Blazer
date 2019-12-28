using BadActor.Shared;
using System;
using System.Collections.Generic;

namespace BadActor.GameObjects
{
    public class ViralDistributor : GameObject<ViralDistributor>
    {
        public static double MachineProgress { get; private set; }

        private static readonly double MACHINE_UNLOCK_THRESHOLD = 100;

        static ViralDistributor()
        {
            GameLoop.RegisterLoopMethod(viralDistributorGameLoop);
        }

        private static void viralDistributorGameLoop(double elapsedSeconds)
        {
            // TODO: optimize
            foreach (ViralDistributor distributor in List)
            {
                if (distributor.Count > 0)
                {
                    foreach (ViralVector vector in distributor.DistributedVectors)
                    {
                        MachineProgress += (1 * elapsedSeconds);
                    }
                }
            }

            if(MachineProgress >= MACHINE_UNLOCK_THRESHOLD)
            {
                Console.WriteLine("Adding machine.");
                new Machine("sucker");
                MachineProgress -= MACHINE_UNLOCK_THRESHOLD;
            }
            appState.SignalRedraw(typeof(ViralDistributor));
        }

        public string[] Icons { get; private set; }
        public int Count { get; private set; } = 0;
        public double Cost { get; private set; } = 1;

        public List<ViralVector> DistributedVectors { get; private set; } = new List<ViralVector>();
        
        private double initialCost;


        public ViralDistributor(string name, double cost = 1, string[] icons = null,
            ViralVector[] distributedVectors = null)
        {
            Name = name;

            Cost = initialCost = cost;

            Icons = icons;

            if (distributedVectors != null) DistributedVectors.AddRange(distributedVectors);

            List.Add(this);
        }

        public void Buy()
        {
            var coin = Resource.Get("Coins");
            if (Resource.Pay(coin, Cost))
            {
                Count++;
                recalculateCost();
            }
        }

        public void AddVector(ViralVector vector)
        {
            if(!DistributedVectors.Contains(vector))
            {
                DistributedVectors.Add(vector);
            }
        }

        // TODO: cost multiplier
        private void recalculateCost()
        {
            Cost = initialCost * Count * 2.5;
        }
    }
}
