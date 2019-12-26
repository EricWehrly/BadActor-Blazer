using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;

namespace BadActor.GameObjects
{
    public class ViralVector
    {
        public static List<ViralVector> List { get; } = new List<ViralVector>();

        public string Name { get; private set; }
        public bool Unlocked { get; private set; }
        public double UnlockTime { get; private set; }
        public string DisplayName
        {
            get
            {
                return Name + " " + VectorType;
            }
        }
        public InfiltrationType VectorType;

        private Timer unlockTimer = new Timer();

        public ViralVector(string name, double unlockTime = 30, bool unlocked = false,
            InfiltrationType vectorType = InfiltrationType.Malware)
        {
            Name = name;

            VectorType = vectorType;

            Unlocked = unlocked;

            UnlockTime = unlockTime;

            unlockTimer.AutoReset = false;

            List.Add(this);
        }

        public void StartUnlock()
        {
            Console.WriteLine("Starting unlock");
            // unlockTimer = new Timer(UnlockTime * 1000);
            // unlockTimer.AutoReset = false;
            Task.Delay((int)UnlockTime * 1000).ContinueWith(t => unlock());
        }

        public enum InfiltrationType
        {
            Malware,
            Worm,
            Vulnerability
        }

        private void unlock()
        {
            Unlocked = true;
            Console.WriteLine(Name + " unlocked now.");
        }
    }
}
