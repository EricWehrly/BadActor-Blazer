using BadActor.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace BadActor.GameObjects
{
    public class Application
    {
        public static List<Application> List { get; } = new List<Application>();

        // tried to do a static constructor and it was NOT having it so ...
        private static bool registeredLoopMethod = false;

        static Application()
        {
            // gameLoop.RegisterLoopMethod(applicationGameLoop);

            GameLoop.RegisterLoopMethod(applicationGameLoop);
        }

        private static void applicationGameLoop()
        {
            foreach(var application in List)
            {
                application.think(application);
            }
        }

        public List<Machine> Machines { get { return machines; } }

        private List<Machine> machines = new List<Machine>();

        public readonly string name;
        private string icon;
        private Action<Application> think;

        public Application(string name, string icon = null, Action<Application> think = null)
        {
            this.name = name;

            this.icon = icon;

            this.think = think;

            List.Add(this);

            if(registeredLoopMethod == false)
            {
                // GameLoop.RegisterLoopMethod(applicationGameLoop);
                registeredLoopMethod = true;
            }
        }

        public void RunOnMachine(Machine machine)
        {
            if (!machines.Contains(machine))
            {
                machines.Add(machine);
            }
        }

        public override string ToString()
        {
            return name;
        }
    }
}
