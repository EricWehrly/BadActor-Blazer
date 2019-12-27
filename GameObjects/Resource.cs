﻿using BadActor.Shared;

namespace BadActor.GameObjects
{
    public class Resource : GameObject<Resource>
    {
        private static AppState appState { get { return AppState.Instance; } }

        public static bool Pay(Resource resource, double amount)
        {
            if (amount > resource.Value) return false;
            else
            {
                resource.Add(amount * -1);
                return true;
            }
        }

        public double Value { get; private set; }
        public string Icon { get; private set; }

        public Resource(string name, string icon, int initialValue = 0)
        {
            Name = name;
            Icon = icon;
            Value = initialValue;

            List.Add(this);
        }

        public double Add(double amount)
        {
            Value += amount;

            appState.NeedsRedraw = true;

            return Value;
        }

        // can we overload to take an int ... ?
        // also this method is broke ...
        public static Resource operator ++(Resource resource)
        {
            resource.Value++;

            appState.NeedsRedraw = true;

            return resource;
        }
    }
}
 