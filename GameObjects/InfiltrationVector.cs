using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BadActor.GameObjects
{
    // Maybe malware is an infiltration vector
    // and 'Audio' is a vector .. channel ?
    public class InfiltrationVector
    {
        public static List<InfiltrationVector> List { get; } = new List<InfiltrationVector>();
        public string Name { get; private set; }
        public string DisplayName
        {
            get
            {
                return Name + " " + VectorType;
            }
        }
        public InfiltrationType VectorType;

        public InfiltrationVector(string name, InfiltrationType vectorType = InfiltrationType.Malware)
        {
            Name = name;

            VectorType = vectorType;

            List.Add(this);
        }

        public enum InfiltrationType
        {
            Malware,
            Worm,
            Vulnerability
        }
    }
}
