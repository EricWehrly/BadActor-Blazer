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

            Machine.OnApplicationRun += listenForInfectedMachineApplicationRun;
        }

        private void infectNextMachine()
        {
            int timeUntilNextInfection = (int)(
                MAX_MILLISECONDS_TO_INFECT
                / Vector.DistributionChannels.Count
                * random.NextDouble());

            // for testing
            if(InfectedMachines.Count == 0)
            {
                timeUntilNextInfection = 5;
            }

            Task.Delay(timeUntilNextInfection, infectionCancellation.Token).ContinueWith(t =>
            {
                if (!infectionCancellation.IsCancellationRequested)
                {
                    InfectedMachines.Add(new Machine("infected"));

                    infectNextMachine();
                }
            });
        }

        private void listenForInfectedMachineApplicationRun(Machine machine, Application application)
        {
            if(InfectedMachines.Contains(machine))
            {
                initiatePatch();

                Machine.OnApplicationRun -= listenForInfectedMachineApplicationRun;
            }
        }

        private void initiatePatch()
        {
            int timeUntilPatched = (int)(WriteTime(Exploit, Vector) * 1000
                * PATCH_TIME_MULTIPLIER * random.NextDouble());
            
            timeUntilPatched = 1000;    // for testing

            Console.WriteLine(Name + " patch time: " + timeUntilPatched);
            Task.Delay(timeUntilPatched).ContinueWith(t =>
            {
                Console.WriteLine(Name + " got patched.");

                Patched = true;

                initiatePatchDistribution();

                infectionCancellation.Cancel();

                appState.SignalRedraw(typeof(Virus));
            });
        }

        private void initiatePatchDistribution()
        {
            Console.WriteLine("Patching " + InfectedMachines.Count + " machines.");

            for(var index = InfectedMachines.Count - 1; index >= 0; index--)
            {
                var machine = InfectedMachines[index];

                int timeUntilPatched = (int)AVERAGE_PATCH_DISTRIBUTION_TIME;
                timeUntilPatched = timeUntilPatched / (((int)machine.ComputingPower) / 100);
                timeUntilPatched = (int)(timeUntilPatched * random.NextDouble());

                Console.WriteLine("Time until machine " + index + " patched: " + timeUntilPatched);

                Task.Delay(timeUntilPatched).ContinueWith(t =>
                {
                    Console.WriteLine(machine.Name + " getting patch for virus " + Name);
                    // TODO: We shouldn't be able to remove items from this list from this class ...
                    // TODO: Once a machine can have multiple viruses, we can't just remove it from 'machines'
                    Machine.List.Remove(machine);
                    InfectedMachines.Remove(machine);

                    machine = null;

                    // TODO: recompute application processing power

                    if (InfectedMachines.Count == 0)
                    {
                        List.Remove(this);
                    }

                    appState.SignalRedraw(typeof(Virus));
                    appState.SignalRedraw(typeof(Machine));
                });
            }
        }
    }
}
