using BadActor.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BadActor.GameObjects
{
    public class ViralVector : Unlockable
    {
        public static List<ViralVector> List { get; } = new List<ViralVector>();

        public static ViralVector Get(string name)
        {
            foreach(ViralVector vector in List)
            {
                if (vector.Name == name) return vector;
            }

            return null;
        }

        public string Name { get; private set; }
        public string Icon { get; private set; }
        public string DisplayName
        {
            get
            {
                return Name + " " + VectorType;
            }
        }
        public InfiltrationType VectorType;

        public ViralVector(string name, double unlockTime = 30, string icon = null,
            InfiltrationType vectorType = InfiltrationType.Malware)
        {
            Unlocking = false;

            Name = name;

            UnlockTime = unlockTime;

            if (unlockTime == 0) Unlocked = true;

            Icon = icon;

            VectorType = vectorType;

            List.Add(this);
        }

        public enum InfiltrationType
        {
            Malware,
            Worm,
            Vulnerability
        }
    }
}
