using BadActor.Attributes;
using System;
using System.Threading.Tasks;

namespace BadActor.GameObjects
{
    [AutoRegister]
    public class CreateInstances
    {
        private readonly float COINS_PER_PROCESSING_POWER = 0.25f;

        // Who knows, maybe we can take the example and load all this from json
        public CreateInstances()
        {
            new Machine("localhost");

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

            createViruses();

            // Well this is dangeous. But we need to wait for appState to be ready ...
            Task.Delay(500).ContinueWith(e => createObjectives());
        }

        private void createViruses()
        {
            // new InfiltrationVector("Webserver", 10, InfiltrationVector.InfiltrationType.Vulnerability);
            // new InfiltrationVector("Audio", 100);

            var musicVector = new ViralVector("Music", 30, "mdi mdi-music");
            var movieVector = new ViralVector("Movie", 60, "mdi mdi-movie-open");
            var gameVector = new ViralVector("Game", 300, "mdi mdi-gamepad-variant-outline");
            var antiVirusVector = new ViralVector("Anti-Virus", 600, "mdi mdi-software");

            new ViralDistributor("XXX Website", 100, new[] { "mdi-web" }, new[] { movieVector });
            new ViralDistributor("Pirate Website", 80, new[] { "mdi-pirate" },
                new[] { musicVector, movieVector, gameVector, antiVirusVector });
        }

        private void createObjectives()
        {
            new Objective("Test", "This is a test", () =>
            {
                Console.WriteLine("Apparently the game state changed.");

                var coinMiner = Application.Get("Coin Miner");

                if (coinMiner.Unlocked)
                {
                    Console.WriteLine("Objective complete! Coin miner unlocked!");
                    // complete();
                }
            });
        }
    }
}
