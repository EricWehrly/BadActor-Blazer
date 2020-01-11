using BadActor.Shared;
using System;
using System.Collections.Generic;

namespace BadActor.GameObjects.Viruses
{
    public partial class Virus : Unlockable<Virus>
    {
        public static readonly string TypeIcon = "mdi mdi-skull-crossbones";
        public static double WriteTime(Exploit exploit, ViralVector vector)
        {
            // TODO: More complicated virus = longer scribe time
            return vector.UnlockTime;
        }

        public static bool CanWriteVirus()
        {
            foreach(Virus virus in List)
            {
                if (virus.UnlockTimeRemaining > 0) return false;
            }

            return true;
        }

        public Exploit Exploit { get; private set; }

        public ViralVector Vector { get; private set; }

        public List<Machine> InfectedMachines = new List<Machine>();

        public bool Patched { get; private set; }

        public Virus(string name, Exploit exploit, ViralVector vector, double unlockTime)
        {
            Console.WriteLine("Beginning virus " + name);
            Name = name;
            Exploit = exploit;
            Vector = vector;
            UnlockTime = unlockTime;

            StartUnlock();
        }
    }
}
