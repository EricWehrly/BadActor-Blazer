using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BadActor.GameObjects
{
    public class Virus : GameObject<Virus>
    {
        public Exploit Exploit { get; private set; }

        public ViralVector Vector { get; private set; }

        public ViralDistributor DeliveryMechanism { get; private set; }

        public Virus(string name)
        {
            Name = name;
        }
    }
}
