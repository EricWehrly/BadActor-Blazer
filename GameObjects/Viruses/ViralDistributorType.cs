using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BadActor.GameObjects.Viruses
{
    public class ViralDistributorType : GameObject<ViralDistributorType>
    {
        public List<ViralVector> DistributedVectors { get; private set; } = new List<ViralVector>();
        public double Cost { get; private set; }

        private double initialCost;

        public ViralDistributorType(string name, ViralVector[] distributedVectors, 
            double cost = 1)
        {
            Name = name;

            DistributedVectors.AddRange(distributedVectors);

            Cost = cost;

            initialCost = cost;
        }

        public void Buy()
        {
            var coin = Resource.Get("Coins");
            if (Resource.Pay(coin, Cost))
            {
                recalculateCost();
            }

            appState.GameStateChanged();
        }

        // TODO: cost multiplier
        private void recalculateCost()
        {
            // Cost = initialCost * Count * 2.5;
        }
    }
}
