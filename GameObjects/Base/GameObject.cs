using System.Collections.Generic;

namespace BadActor.GameObjects
{
    public abstract class GameObject<T> : GameObjectBase<T> where T: GameObjectBase<T>
    {
        public static List<T> List { get; } = new List<T>();

        public static T Get(string name)
        {
            foreach (var gameObject in List)
            {
                if (gameObject.Name == name) return gameObject;
            }

            return null;
        }
    }
}
