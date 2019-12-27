using System.Collections.Generic;

namespace BadActor.GameObjects
{
    public class Machine : GameObject<Machine>
    {
        public ComputingPower MachineComputingPower { get; private set; } = ComputingPower.LOW;
        public AccessLevel MachineAccessLevel { get; private set; } = AccessLevel.GUEST;
        public List<Application> Applications { get; } = new List<Application>();

        public Machine(string name)
        {
            // boolean constructor is localhost ...
            if (name == "localhost") MachineAccessLevel = AccessLevel.ROOT;

            Name = name;

            List.Add(this);
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
            LOW = 100,
            MEDIUM = 500,
            HIGH = 1000,
            SERVER = 2500,
            BEAST = 5000
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
