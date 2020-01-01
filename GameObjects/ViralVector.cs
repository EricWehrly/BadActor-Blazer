using BadActor.GameObjects.Enums;

namespace BadActor.GameObjects
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

            if (unlockTime == 0) Unlocked = true;

            Icon = icon;

            InfiltrationType = vectorType;
        }
    }
}
