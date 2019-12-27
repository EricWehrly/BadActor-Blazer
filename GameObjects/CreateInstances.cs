using BadActor.Attributes;
using System;

namespace BadActor.GameObjects
{
    [AutoRegister]
    public class CreateInstances
    {
        private readonly float COINS_PER_PROCESSING_POWER = 0.025f;

        // Who knows, maybe we can take the example and load all this from json
        public CreateInstances()
        {
            new Machine("localhost");

            new Machine("ui test");
            new Machine("ui test 2");

            var coins = new Resource("Coins", "mdi mdi-coin");

            // should apps have a 'type'
            // and coin miner would show the coin currency icon to denote it's a money making type?
            new Application("Coin Miner", null, 0, (application, elapsedSeconds) =>
            {
                double coinsToAdd = application.ProcessingPower * COINS_PER_PROCESSING_POWER * elapsedSeconds;
                coins.Add(coinsToAdd);
                // Console.WriteLine(application.name + " thinking on " + application.Machines.Count + " machines.");
            });

            new Application("Nigerian Prince", null, 3000);

            // new InfiltrationVector("Webserver", 10, InfiltrationVector.InfiltrationType.Vulnerability);
            // new InfiltrationVector("Audio", 100);

            var musicVector = new ViralVector("Music", 30, "mdi mdi-music");
            var movieVector = new ViralVector("Movie", 60, "mdi mdi-movie");
            var gameVector = new ViralVector("Game", 300, "mdi mdi-game");
            var antiVirusVector = new ViralVector("Anti-Virus", 600, "mdi mdi-software");

            // icons?
            new ViralDistributor("XXX Website", 100, new[] { "mdi-web" }, new[] { movieVector });
            new ViralDistributor("Pirate Website", 80, new[] { "mdi-pirate" },
                new[] { musicVector, movieVector, gameVector, antiVirusVector });
        }
    }
}
