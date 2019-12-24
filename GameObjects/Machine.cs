using System.Collections.Generic;

namespace BadActor.GameObjects
{
    public class Machine
    {
        public static List<Machine> list { get; } = new List<Machine>();

        public readonly string name;

        // should we differentiate between running and installed?
        private List<Application> applications = new List<Application>();

        public Machine(string name)
        {
            this.name = name;

            list.Add(this);

            System.Console.WriteLine(this.name);
        }

        public void RunApplication(Application application)
        {
            this.applications.Add(application);
        }
    }
}
