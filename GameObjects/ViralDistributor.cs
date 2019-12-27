using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BadActor.GameObjects
{
    public class ViralDistributor : GameObject<ViralDistributor>
    {
        public string[] Icons { get; private set; }
        public int Count { get; private set; } = 0;
        public double Cost { get; private set; } = 1;


        private double initialCost;


        public ViralDistributor(string name, double cost = 1, string[] icons = null)
        {
            Name = name;

            Cost = initialCost = cost;

            Icons = icons;

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
