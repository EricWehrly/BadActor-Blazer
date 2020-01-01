using BadActor.Attributes;
using BadActor.GameObjects.Exploits;
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

            new Resource("Coins", "mdi mdi-bitcoin");

            createApplications();

            createExploits();

            createViruses();

            createObjectives();
        }

        private void createApplications()
        {
            // should apps have a 'type'
            // and coin miner would show the coin currency icon to denote it's a money making type?
            new Application("Coin Miner", null, 0, (application, elapsedSeconds) =>
            {
                double coinsToAdd = application.ProcessingPower * COINS_PER_PROCESSING_POWER * elapsedSeconds;
                Resource.Get("Coins").Add(coinsToAdd);
                // Console.WriteLine(application.name + " thinking on " + application.Machines.Count + " machines.");
            });

            new Application("Music Sharer", null, 0, (application, elapsedSeconds) =>
            {
                // functions as a music distributor ...
            });

            new Application("Zamundan Prince", null, 3000);

            // new Application("Exploit Finder");

            // Open-Source Webserver
            // Rinkydink Webserver
            // Commercial Webserver
            // Enterprise Webserver
        }

        private void createViruses()
        {
            // new InfiltrationVector("Webserver", 10, InfiltrationVector.InfiltrationType.Vulnerability);
            // new InfiltrationVector("Audio", 100);

            var musicVector = new ViralVector("Music", 30, "mdi mdi-music");
            var movieVector = new ViralVector("Movie", 60, "mdi mdi-movie-open");
            var gameVector = new ViralVector("Game", 300, "mdi mdi-gamepad-variant-outline");
            var antiVirusVector = new ViralVector("Anti-Virus", 600, "mdi mdi-application");

            new ViralDistributor("XXX Website", 100, new[] { "mdi-web" }, new[] { movieVector });
            new ViralDistributor("Pirate Website", 80, new[] { "mdi-pirate" },
                new[] { musicVector, movieVector, gameVector, antiVirusVector });
        }

        private void createExploits()
        {
            var fakeOsExploit = new Exploitable("FakeOS");
            // other OS's
            // Applications (like webservers ...)

            new Exploit(fakeOsExploit);

        }

        private void createObjectives()
        {
            var getMoney = new Objective("Get some money", new Objective.ObjectiveCriteria[] {
                coinMinerPurchased, coinMinerApplied
            });

            var writeVirus = new Objective("Spread some love", new Objective.ObjectiveCriteria[] {
                malwareWritten, distributorUnlocked
            }, new[] { getMoney });

            new Objective("Get some workers", new Objective.ObjectiveCriteria[] {
                getTenWorkers
            }, new[] { writeVirus });
        }

        private Objective.ObjectiveCriteria coinMinerPurchased =
            new Objective.ObjectiveCriteria("Purchase coin miner from apps", () =>
        {
            return Application.Get("Coin Miner").Unlocked;
        });

        private Objective.ObjectiveCriteria coinMinerApplied =
            new Objective.ObjectiveCriteria("Drag coin miner to localhost<br />to run it on your machine", () =>
            {
                return Application.Get("Coin Miner").Machines.Count > 0;
            });

        private Objective.ObjectiveCriteria malwareWritten =
            new Objective.ObjectiveCriteria("Write a piece of malware", () =>
            {
                foreach(ViralVector vector in ViralVector.List)
                {
                    if (vector.Unlocked) return true;
                }

                return false;
            });

        private Objective.ObjectiveCriteria distributorUnlocked =
            new Objective.ObjectiveCriteria("Host a malware distributor", () =>
            {
                foreach(ViralDistributor distributor in ViralDistributor.List)
                {
                    if (distributor.Unlocked) return true;
                }

                return false;
            });

        private Objective.ObjectiveCriteria getTenWorkers =
            new Objective.ObjectiveCriteria("Accumulate 10 workers<br />from malware distribution.", () =>
            {
                return Machine.List.Count > 9;
            });
    }
}
