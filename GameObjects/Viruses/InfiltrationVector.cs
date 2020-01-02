using BadActor.GameObjects.Enums;

namespace BadActor.GameObjects.Viruses
{
    // Maybe malware is an infiltration vector
    // and 'Audio' is a vector .. channel ?
    public class InfiltrationVector : GameObject<InfiltrationVector>
    {
        public int Count { get; private set; } = 0;
        public double Cost { get; private set; } = 1;
        public string DisplayName
        {
            get
            {
                return Name + " " + InfiltrationType;
            }
        }
        public InfiltrationType InfiltrationType;

        private double initialCost;

        public InfiltrationVector(string name, double cost = 1,
            InfiltrationType vectorType = InfiltrationType.Malware)
        {
            Name = name;

            Cost = initialCost = cost;

            InfiltrationType = vectorType;
        }

        public void Buy()
        {
            var coin = Resource.Get("Coins");
            if(Resource.Pay(coin, Cost)) {
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
