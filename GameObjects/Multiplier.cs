using System;
using System.Collections.Generic;

namespace BadActor.GameObjects
{
    // Need to structure this so that multiple things can contribute
    public class Multiplier
    {
        private static readonly string INITIAL_VALUE_KEY = "Initial Value";

        public static List<Multiplier> List { get; } = new List<Multiplier>();

        public string Name { get; private set; }

        public float Value { get; private set; }

        private Dictionary<string, float> contributors = new Dictionary<string, float>();

        public Multiplier(string name, float initialValue = 1)
        {
            Name = name;
            contributors.Add(INITIAL_VALUE_KEY, initialValue);
        }

        public void SetContributor(string name, int value)
        {
            contributors[name] = value;
            recomputeValue();
        }

        // TODO: remove contributor

        private void recomputeValue()
        {
            float currentValue = contributors[INITIAL_VALUE_KEY];
            foreach (string contributor in contributors.Keys)
            {
                if (contributor != INITIAL_VALUE_KEY)
                {
                    currentValue *= contributors[contributor];
                }
            }

            Value = currentValue;
        }
    }
}
