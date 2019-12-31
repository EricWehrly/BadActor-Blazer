namespace BadActor.GameObjects
{
    public class ViralVector : Unlockable<ViralVector>
    {
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
        }

        public enum InfiltrationType
        {
            Malware,
            Worm,
            Vulnerability
        }
    }
}
