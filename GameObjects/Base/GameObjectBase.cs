using BadActor.Shared;

namespace BadActor.GameObjects
{
    public class GameObjectBase<T>
    {
        protected static AppState appState { get { return AppState.Instance; } }

        public string Name { get; protected set; }
    }
}
