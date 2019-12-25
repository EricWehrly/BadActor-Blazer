using BadActor.Attributes;
using System;

namespace BadActor.GameObjects
{
    [AutoRegister]
    public class CreateInstances
    {
        private readonly float COINS_PER_PROCESSING_POWER = 0.025f;

        public CreateInstances()
        {
            new Machine("localhost");

            new Machine("ui test");

            new Machine("ui test 2");

            var coins = new Resource("Coins", "mdi mdi-coin");

            new Application("Coin Miner", null, (application, elapsedSeconds) =>
            {
                double coinsToAdd = application.ProcessingPower * COINS_PER_PROCESSING_POWER * elapsedSeconds;
                coins.Add(coinsToAdd);
                // Console.WriteLine(application.name + " thinking on " + application.Machines.Count + " machines.");
            });

            // new Application("Nigerian Prince");
        }
    }
}
