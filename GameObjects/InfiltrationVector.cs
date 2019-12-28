namespace BadActor.GameObjects
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
                return Name + " " + VectorType;
            }
        }
        public InfiltrationType VectorType;

        private double initialCost;

        public InfiltrationVector(string name, double cost = 1,
            InfiltrationType vectorType = InfiltrationType.Malware)
        {
            Name = name;

            Cost = initialCost = cost;

            VectorType = vectorType;
        }

        public void Buy()
        {
            var coin = Resource.Get("Coins");
            if(Resource.Pay(coin, Cost)) {
                Count++;
                recalculateCost();
            }
        }

        public enum InfiltrationType
        {
            Malware,
            Worm,
            Vulnerability
        }

        // TODO: cost multiplier
        private void recalculateCost()
        {
            Cost = initialCost * Count * 2.5;
        }
    }
}
