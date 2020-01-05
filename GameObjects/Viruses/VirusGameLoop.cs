using System;
using System.Threading.Tasks;
using System.Threading;

namespace BadActor.GameObjects.Viruses
{
    public partial class Virus
    {
        private static readonly double MAX_MILLISECONDS_TO_INFECT = 60000;
        private static readonly double PATCH_TIME_MULTIPLIER = 10;
        private static readonly double AVERAGE_PATCH_DISTRIBUTION_TIME = 60000;

        private Random random = new Random();
        private CancellationTokenSource infectionCancellation = new CancellationTokenSource();

        protected override void onUnlock()
        {
            infectNextMachine();

            initiatePatch();
        }

        private void infectNextMachine()
        {
            // Unlockable<ViralDistributor>.OnUnlock += onUnlock;

            int timeUntilNextInfection = (int)(MAX_MILLISECONDS_TO_INFECT * random.NextDouble());

            Task.Delay(timeUntilNextInfection, infectionCancellation.Token).ContinueWith(t =>
            {
                InfectedMachines.Add(new Machine("infected"));

                infectNextMachine();
            });
        }

        private void initiatePatch()
        {
            int timeUntilPatched = (int)(WriteTime(Exploit, Vector) * 1000
                * PATCH_TIME_MULTIPLIER * random.NextDouble());

            Console.WriteLine(Name + " patch time: " + timeUntilPatched);
            Task.Delay(timeUntilPatched).ContinueWith(t =>
            {
                Console.WriteLine(Name + " got patched.");

                Patched = true;

                initiatePatchDistribution();

                infectionCancellation.Cancel();
            });
        }

        // TODO: pick off computers one by one, base time off of computing power (more == sooner)
        private void initiatePatchDistribution()
        {
            for(var index = InfectedMachines.Count; index > 0; index--)
            {
                var machine = InfectedMachines[index];

                int timeUntilPatched = (int)((AVERAGE_PATCH_DISTRIBUTION_TIME
                    / (int)machine.ComputingPower)
                    * random.NextDouble());

                Task.Delay(timeUntilPatched).ContinueWith(t =>
                {
                    Console.WriteLine(machine.Name + " getting patch for virus " + Name);
                    // TODO: We shouldn't be able to remove items from this list from this class ...
                    Machine.List.Remove(machine);
                    machine = null;
                });

                // TODO: on the last one, delete the virus ...
            }
        }
    }
}
