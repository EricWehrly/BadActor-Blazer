using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BadActor.GameObjects
{
    public class ViralDistributor
    {
        public static List<ViralDistributor> List { get; } = new List<ViralDistributor>();
                
        public string Name { get; private set; }
        public int Count { get; private set; } = 0;
        public double Cost { get; private set; } = 1;


        private double initialCost;


        public ViralDistributor(string name, double cost = 1)
        {
            Name = name;

            Cost = initialCost = cost;

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

        // TODO: cost multiplier
        private void recalculateCost()
        {
            Cost = initialCost * Count * 2.5;
        }
    }
}
