using BadActor.Shared;
using System;
using System.Collections.Generic;

namespace BadActor.GameObjects.Viruses
{
    public class ViralDistributor : GameObject<ViralDistributor>
    {
        public static readonly string TypeIcon = "mdi mdi-ray-start-arrow";

        public static List<ViralDistributor> GetDistributionChannels(ViralVector vector)
        {
            var distributors = new List<ViralDistributor>();

            foreach (ViralDistributor distributor in List)
            {
                if(distributor.DistributedVectors.Contains(vector))
                {
                    distributors.Add(distributor);
                }
            }

            return distributors;
        }


        public string[] Icons { get; private set; }
        public int Count { get; private set; } = 0;
        public bool Unlocked { get { return Count > 0; } }

        public ViralDistributorType ViralDistributorType { get; private set; }

        public List<ViralVector> DistributedVectors
        {
            get
            {
                return ViralDistributorType.DistributedVectors;
            }
        }

        public ViralDistributor(string name, ViralDistributorType distributorType,
            double cost = 1, string[] icons = null)
        {
            Name = name;

            ViralDistributorType = distributorType;

            Icons = icons;
        }
    }
}
