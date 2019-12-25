using System;
using System.Collections.Generic;

namespace BadActor.GameObjects
{
    public class Application
    {        public static List<Application> list { get; } = new List<Application>();

        public readonly string name;
        private string icon;

        public Application(string name, string icon = null)
        {
            this.name = name;

            this.icon = icon;

            list.Add(this);
        }

        void think()
        {
            Console.WriteLine(name + " thinking ...");
        }
        public override string ToString()
        {
            return name;
        }
    }
}
