﻿using BadActor.Shared;
using System;
using System.Collections.Generic;

namespace BadActor.GameObjects.Viruses
{
    public class ViralDistributor : GameObject<ViralDistributor>
    {
        public static readonly string TypeIcon = "mdi mdi-ray-start-arrow";
        // TODO: should this be per distributor?
        public static double MachineProgress { get; private set; }

        private static readonly double MACHINE_UNLOCK_THRESHOLD = 100;

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

        // This is part of virus now
        static ViralDistributor()
        {
            // GameLoop.RegisterLoopMethod(viralDistributorGameLoop);
        }

        private static void viralDistributorGameLoop(double elapsedSeconds)
        {
            // TODO: Optimizing this will result in a significant performance boost
            var initialMachineProgress = MachineProgress;
            List<ViralVector> activeVectors = new List<ViralVector>();
            
            foreach (ViralDistributor distributor in List)
            {
                if (distributor.Unlocked)
                {
                    foreach (ViralVector vector in distributor.DistributedVectors)
                    {
                        if (vector.Unlocked) activeVectors.Add(vector);
                    }
                }
            }
            foreach (Virus virus in Virus.List)
            {
                if (virus.Unlocked && activeVectors.Contains(virus.Vector))
                {
                    MachineProgress += (2.5 * elapsedSeconds);
                }
            }

            if (MachineProgress >= MACHINE_UNLOCK_THRESHOLD)
            {
                new Machine("sucker");
                MachineProgress -= MACHINE_UNLOCK_THRESHOLD;
            }
            if (MachineProgress != initialMachineProgress)
            {
                appState.SignalRedraw(typeof(ViralDistributor));
            }
        }

        public string[] Icons { get; private set; }
        public int Count { get; private set; } = 0;
        public double Cost { get; private set; } = 1;
        public bool Unlocked { get { return Count > 0; } }

        public List<ViralVector> DistributedVectors { get; private set; } = new List<ViralVector>();
        
        private double initialCost;


        public ViralDistributor(string name, double cost = 1, string[] icons = null,
            ViralVector[] distributedVectors = null)
        {
            Name = name;

            Cost = initialCost = cost;

            Icons = icons;

            if (distributedVectors != null) DistributedVectors.AddRange(distributedVectors);
        }

        public void Buy()
        {
            var coin = Resource.Get("Coins");
            if (Resource.Pay(coin, Cost))
            {
                Count++;
                recalculateCost();
            }

            appState.GameStateChanged();
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