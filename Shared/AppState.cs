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

        private bool needsRedraw = false;
        public bool NeedsRedraw
        {
            get { return needsRedraw; }
            set
            {
                needsRedraw = value;
                if(needsRedraw) RedrawNeeded?.Invoke();
            }
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

        public event Action RedrawNeeded;
        public event Action OnApplicationDraggedChanged;
    }
}
