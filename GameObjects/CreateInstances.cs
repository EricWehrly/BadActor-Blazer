namespace BadActor.GameObjects
{
    public class CreateInstances
    {
        public static void Create()
        {
            new Machine("localhost");

            new Application("Coin Miner");
        }
    }
}
