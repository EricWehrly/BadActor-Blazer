using BadActor.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BadActor.GameObjects
{
    public class Resource
    {
        public static List<Resource> List { get; } = new List<Resource>();

        private static AppState appState { get { return AppState.Instance; } }

        public static Resource Get(string name)
        {
            foreach(Resource resource in List)
            {
                if (resource.Name == name) return resource;
            }

            return null;
        }

        public string Name { get; private set; }
        public double Value { get; private set; }
        public string Icon { get; private set; }

        public Resource(string name, string icon, int initialValue = 0)
        {
            Name = name;
            Icon = icon;
            Value = initialValue;

            List.Add(this);
        }

        public double Add(int amount)
        {
            Value += amount;

            appState.NeedsRedraw = true;

            return this.Value;
        }

        // can we overload to take an int ... ?
        // also this method is broke ...
        public static Resource operator ++(Resource resource)
        {
            resource.Value++;

            // BadActor.UI.Resources.ResourceChanged();
            if (appState == null)
            {
                Console.WriteLine("Mate, appState didn't get injected.");
            }
            else
            {
                appState.NeedsRedraw = true;
            }

            return resource;
        }
    }
}
 