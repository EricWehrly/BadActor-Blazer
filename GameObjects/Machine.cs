using System.Collections.Generic;

namespace BadActor.GameObjects
{
    public class Machine
    {
        private static List<Machine> _list = new List<Machine>();

        public readonly string name;

        public static List<Machine> list
        {
            get { return _list; }
        }

        public Machine(string name)
        {
            this.name = name;

            _list.Add(this);

            System.Console.WriteLine(this.name);
        }
    }
}
