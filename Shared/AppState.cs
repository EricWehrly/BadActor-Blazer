using System;
using BadActor.GameObjects;

namespace BadActor.Shared
{
    public class AppState
    {

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

        public event Action OnApplicationDraggedChanged;
    }
}
