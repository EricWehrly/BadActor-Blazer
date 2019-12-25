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

            new Application("Coin Miner", null, (application) =>
            {
                Console.WriteLine(application.name + " thinking on " + application.Machines.Count + " machines.");
            });

            // new Application("Nigerian Prince");
        }
    }
}
