using BadActor.Attributes;
using System;

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

            var coins = new Resource("Coins", "mdi mdi-coin");

            new Application("Coin Miner", null, (application) =>
            {
                // Console.WriteLine(application.name + " thinking on " + application.Machines.Count + " machines.");
                // TODO: Processing power, not machine count
                for(var i = 0; i < application.Machines.Count; i++)
                {
                    coins++;
                }
                // coins.Add(application.Machines.Count);
            });

            // new Application("Nigerian Prince");
        }
    }
}
