namespace BadActor.GameObjects
{
    public class Resource : GameObject<Resource>
    {
        public static bool Pay(Resource resource, double amount)
        {
            if (amount > resource.Value) return false;
            else
            {
                resource.Add(amount * -1);
                return true;
            }
        }

        public double Value { get; private set; }
        public string Icon { get; private set; }

        public Resource(string name, string icon, int initialValue = 0)
        {
            Name = name;
            Icon = icon;
            Value = initialValue;

            List.Add(this);
        }

        public double Add(double amount)
        {
            if (amount == 0) return Value;

            Value += amount;

            appState.SignalRedraw(typeof(Resource));

            return Value;
        }

        // can we overload to take an int ... ?
        // also this method is broke ...
        public static Resource operator ++(Resource resource)
        {
            resource.Value++;

            appState.SignalRedraw(typeof(Resource));

            return resource;
        }
    }
}
 