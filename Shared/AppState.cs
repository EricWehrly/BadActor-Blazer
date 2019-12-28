using System;
using BadActor.GameObjects;

namespace BadActor.Shared
{
    public class AppState
    {
        public static AppState Instance { get; private set; }

        public AppState()
        {
            if (Instance == null) Instance = this;
            else
            {
                Console.WriteLine("AppState is not OK.");
            }
        }

        public void SignalRedraw(Type filter)
        {
            RedrawNeeded?.Invoke(filter);
        }

        public void GameStateChanged()
        {
            OnGameStateChanged?.Invoke();
            // RedrawNeeded?.Invoke(null);  // should we? just for good measure?
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
