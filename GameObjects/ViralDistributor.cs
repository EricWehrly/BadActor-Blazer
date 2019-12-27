using BadActor.Shared;
using System.Collections.Generic;

namespace BadActor.GameObjects
{
    public class ViralDistributor : GameObject<ViralDistributor>
    {
        static ViralDistributor()
        {
            GameLoop.RegisterLoopMethod(applicationGameLoop);
        }

        private static void applicationGameLoop(double elapsedSeconds)
        {
            foreach (ViralDistributor distributor in List)
            {
                // application.think?.Invoke(application, elapsedSeconds);
            }
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
