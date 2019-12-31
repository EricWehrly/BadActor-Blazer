using System;
using System.Collections.Generic;
using System.Linq;

namespace BadActor.GameObjects
{
    public class Machine : GameObject<Machine>
    {
        public static List<Machine> MachineGroup(ComputingPower computingPower, bool excludeLocalhost = true)
        {
            var machines = List.Where(machine => machine.MachineComputingPower == computingPower);
            if (excludeLocalhost) machines = machines.Where(machine => machine.Name != "localhost");

            return machines.ToList();
        }

        public ComputingPower MachineComputingPower { get; private set; } = ComputingPower.Granny;
        public AccessLevel MachineAccessLevel { get; private set; } = AccessLevel.guest;
        public List<Application> Applications { get; } = new List<Application>();

        public Machine(string name)
        {
            // boolean constructor is localhost ...
            if (name == "localhost") MachineAccessLevel = AccessLevel.root;

            Name = name;

            // TODO: can we somehow animate machine draw in?
            if(appState != null) appState.SignalRedraw(GetType());
        }

        public void RunApplication(Application application)
        {
            if (!Applications.Contains(application))
            {
                Applications.Add(application);
            }

            appState.GameStateChanged();
        }

        public int AvailableComputingPower
        {
            get
            {
                return (int)MachineComputingPower / (int)MachineAccessLevel;
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public enum ComputingPower
        {
            Granny = 100,
            Disposable = 500,
            Family = 1000,
            Gaming = 1000,
            Server = 2500,
            Mainframe = 5000
        }

        public enum AccessLevel
        {
            root = 1,
            admin = 2,
            user = 3,
            guest = 4
        }
    }
}
