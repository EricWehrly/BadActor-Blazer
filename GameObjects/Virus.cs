namespace BadActor.GameObjects
{
    public class Virus : GameObject<Virus>
    {
        public static double WriteTime(Exploit exploit, ViralVector vector)
        {
            // TODO: More complicated virus = longer scribe time
            return vector.UnlockTime;
        }

        public Exploit Exploit { get; private set; }

        public ViralVector Vector { get; private set; }

        public ViralDistributor DeliveryMechanism { get; private set; }

        public Virus(string name, Exploit exploit, ViralVector vector)
        {
            Name = name;

            Exploit = exploit;

            Vector = vector;
        }
    }
}
