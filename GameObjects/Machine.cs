using BadActor.GameObjects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BadActor.GameObjects
{
    public class Machine : GameObject<Machine>
    {
        public static string Icon = "mdi mdi-desktop-classic";

        public static List<Machine> MachineGroup(ComputingPower computingPower, bool excludeLocalhost = true)
        {
            var machines = List.Where(machine => machine.ComputingPower == computingPower);
            if (excludeLocalhost) machines = machines.Where(machine => machine.Name != "localhost");

            return machines.ToList();
        }

        public ComputingPower ComputingPower { get; private set; } = ComputingPower.Granny;
        public AccessLevel AccessLevel { get; private set; } = AccessLevel.guest;
        public List<Application> Applications { get; } = new List<Application>();

        public Machine(string name)
        {
            // boolean constructor is localhost ...
            if (name == "localhost") AccessLevel = AccessLevel.root;

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
                return (int)ComputingPower / (int)AccessLevel;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
