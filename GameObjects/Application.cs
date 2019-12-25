using BadActor.Shared;
using System;
using System.Collections.Generic;

namespace BadActor.GameObjects
{
    public class Application
    {
        private static readonly Multiplier processingPower;

        public static List<Application> List { get; } = new List<Application>();

        // maybe all of this, and the new application stuff should just be its own file
        static Application()
        {
            GameLoop.RegisterLoopMethod(applicationGameLoop);
            processingPower = new Multiplier("Available Processing Power", 0.1f);
        }

        private static void applicationGameLoop()
        {
            foreach(var application in List)
            {
                application.think(application);
            }
        }

        public List<Machine> Machines { get { return machines; } }
        public int ProcessingPower { get; private set; } = 0;

        private List<Machine> machines = new List<Machine>();

        public readonly string Name;
        private string Icon;
        private Action<Application> think;

        public Application(string name, string icon = null, Action<Application> thinkMethod = null)
        {
            Name = name;

            Icon = icon;

            think = thinkMethod;

            List.Add(this);
        }

        public void RunOnMachine(Machine machine)
        {
            if (!machines.Contains(machine))
            {
                machines.Add(machine);
                recomputeProcessingPower();
            }
        }

        public override string ToString()
        {
            return Name;
        }

        private void recomputeProcessingPower()
        {
            int processingPower = 0;
            foreach(Machine machine in machines)
            {
                Console.WriteLine(machine.AvailableComputingPower + " from " + machine.Name);
                processingPower += machine.AvailableComputingPower;
            }

            ProcessingPower = processingPower;
            Console.WriteLine(Name + " processing power now " + ProcessingPower);
        }
    }
}
