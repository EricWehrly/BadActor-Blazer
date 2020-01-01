using System;
using BadActor.GameObjects;

namespace BadActor.Shared
{
    public class AppState
    {
        private static AppState _instance;
        public static AppState Instance
        {
            get
            {
                if (_instance == null) _instance = new AppState();

                return _instance;
            }
        }

        public void SignalRedraw(Type filter)
        {
            RedrawNeeded?.Invoke(filter);
        }

        public void GameStateChanged()
        {
            OnGameStateChanged?.Invoke();
            RedrawNeeded?.Invoke(null);  // should we? just for good measure?
        }

        private Application _applicationBeingDragged;
        public Application ApplicationBeingDragged
        {
            get
            {
                return _applicationBeingDragged;
            }
            set
            {
                _applicationBeingDragged = value;
                OnApplicationDraggedChanged?.Invoke();
            }
        }

        public event Action<Type> RedrawNeeded;
        public event Action OnApplicationDraggedChanged;
        public event Action OnGameStateChanged;
    }
}
