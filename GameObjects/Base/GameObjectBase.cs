using BadActor.Shared;

namespace BadActor.GameObjects
{
    public class GameObjectBase
    {
        protected static AppState appState { get { return AppState.Instance; } }

        public virtual string Name { get; protected set; }
    }
}
