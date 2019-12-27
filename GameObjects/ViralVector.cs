using BadActor.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BadActor.GameObjects
{
    public class ViralVector : Unlockable
    {
        public static List<ViralVector> List { get; } = new List<ViralVector>();

        public string Name { get; private set; }
        public string DisplayName
        {
            get
            {
                return Name + " " + VectorType;
            }
        }
        public InfiltrationType VectorType;

        public ViralVector(string name, double unlockTime = 30, bool unlocked = false,
            InfiltrationType vectorType = InfiltrationType.Malware)
        {
            Name = name;

            VectorType = vectorType;

            Unlocked = unlocked;

            UnlockTime = unlockTime;

            Unlocking = false;

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
