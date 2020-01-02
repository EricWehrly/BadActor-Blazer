using BadActor.GameObjects.Enums;

namespace BadActor.GameObjects.Viruses
{
    public class ViralVector : Unlockable<ViralVector>
    {
        public string DisplayName
        {
            get
            {
                return Name + " " + InfiltrationType;
            }
        }
        public InfiltrationType InfiltrationType;

        public ViralVector(string name, double unlockTime = 30, string icon = null,
            InfiltrationType vectorType = InfiltrationType.Malware)
        {
            Unlocking = false;

            Name = name;

            UnlockTime = unlockTime;

            // Vectors are unlocked by default now ... might even just want an enum instead
            // (and drop infiltration type)

            // if (unlockTime == 0) Unlocked = true;
            Unlocked = true;

            Icon = icon;

            InfiltrationType = vectorType;
        }
    }
}
