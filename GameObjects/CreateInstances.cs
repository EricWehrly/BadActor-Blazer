using BadActor.Attributes;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

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
            new Objective("Get some money", 
                "Open Apps, <br />Purchase the Coin Miner,<br />Drag it to localhost to run it on your machine.",
                new Func<bool>[] { coinMinerPurchased, coinMinerApplied });
        }

        private bool coinMinerPurchased()
        {
            Console.WriteLine("Hello 1");
            
            var coinMiner = Application.Get("Coin Miner");
            return coinMiner.Unlocked;
        }

        private bool coinMinerApplied()
        {
            var coinMiner = Application.Get("Coin Miner");

            return coinMiner.Machines.Count > 0;
        }
    }
}
