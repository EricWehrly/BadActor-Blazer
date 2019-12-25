using BadActor.Attributes;

namespace BadActor.GameObjects
{
    [AutoRegister]
    public class CreateInstances
    {
        public CreateInstances()
        {
            new Machine("localhost");

            new Machine("ui test");

            new Machine("ui test 2");

            new Application("Coin Miner");

            new Application("Nigerian Prince");

            new Application("a third thing");
        }
    }
}
