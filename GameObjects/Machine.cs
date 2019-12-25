using System.Collections.Generic;

namespace BadActor.GameObjects
{
    public class Machine
    {
        public static List<Machine> List { get; } = new List<Machine>();

        public readonly string name;

        // should we differentiate between running and installed?
        private List<Application> applications = new List<Application>();
        public List<Application> Applications { get { return applications; } }

        public Machine(string name)
        {
            this.name = name;

            List.Add(this);
        }

        public void RunApplication(Application application)
        {
            if(!applications.Contains(application)) {
                applications.Add(application);
            }
        }
        public override string ToString()
        {
            return name;
        }
    }
}
