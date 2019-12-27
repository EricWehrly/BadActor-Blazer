using BadActor.Shared;
using System;
using System.Collections.Generic;

namespace BadActor.GameObjects
{
    public class Application : GameObject<Application>
    {
        private static readonly Multiplier processingPower;

        // maybe all of this, and the new application stuff should just be its own file
        static Application()
        {
            GameLoop.RegisterLoopMethod(applicationGameLoop);
            processingPower = new Multiplier("Available Processing Power", 0.1f);
        }

        private static void applicationGameLoop(double elapsedSeconds)
        {
            foreach (Application application in List)
            {
                application.think?.Invoke(application, elapsedSeconds);
            }
        }
        public string Icon { get; private set; }
        public double Cost { get; private set; }
        public bool Unlocked { get; private set; }
        public List<Machine> Machines { get; } = new List<Machine>();
        public int ProcessingPower { get; private set; } = 0;

        private Action<Application, double> think;

        public Application(string name, string icon = null, double cost = 0,
            Action<Application, double> thinkMethod = null, bool unlocked = false)
        {
            Name = name;

            Icon = icon;

            Cost = cost;

            Unlocked = unlocked;

            think = thinkMethod;

            List.Add(this);
        }

        public void RunOnMachine(Machine machine)
        {
            if (!Machines.Contains(machine))
            {
                Machines.Add(machine);
                recomputeProcessingPower();
            }
        }

        public bool Unlock()
        {
            Resource coins = Resource.Get("Coins");

            Unlocked = Resource.Pay(coins, Cost);

            return Unlocked;
        }

        public override string ToString()
        {
            return Name;
        }

        private void recomputeProcessingPower()
        {
            int processingPower = 0;
            foreach (Machine machine in Machines)
            {
                Console.WriteLine(machine.AvailableComputingPower + " from " + machine.Name);
                processingPower += machine.AvailableComputingPower;
            }

            ProcessingPower = processingPower;
            Console.WriteLine(Name + " processing power now " + ProcessingPower);
        }
    }
}
