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

        public ComputingPower MachineComputingPower { get; private set; } = ComputingPower.Ancient;
        public AccessLevel MachineAccessLevel { get; private set; } = AccessLevel.GUEST;
        public List<Application> Applications { get; } = new List<Application>();

        public Machine(string name)
        {
            // boolean constructor is localhost ...
            if (name == "localhost") MachineAccessLevel = AccessLevel.ROOT;

            Name = name;

            List.Add(this);

            // TODO: can we somehow animate machine draw in?
            if(appState != null) appState.SignalRedraw(GetType());
        }

        public void RunApplication(Application application)
        {
            if (!Applications.Contains(application))
            {
                Applications.Add(application);
            }
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
            Ancient = 100,
            Disposable = 500,
            Grandma = 1000,
            Standard = 1000,
            Server = 2500,
            Gaming = 5000
        }

        public enum AccessLevel
        {
            ROOT = 1,
            ADMIN = 2,
            USER = 3,
            GUEST = 4
        }
    }
}
