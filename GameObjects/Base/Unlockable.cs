using BadActor.Shared;
using System;
using System.Threading.Tasks;

namespace BadActor.GameObjects
{
    public class Unlockable
    {
        private static AppState appState { get { return AppState.Instance; } }

        public bool Unlocked { get; protected set; }
        public double UnlockTime { get; protected set; }
        public bool Unlocking { get; protected set; }

        // eventually we may want to track unlock progress in here, so we can interrupt ...

        public void StartUnlock()
        {
            Task.Delay((int)UnlockTime * 1000).ContinueWith(t => unlock());

            Unlocking = true;

            appState.SignalRedraw(GetType());
        }

        private void unlock()
        {
            Unlocked = true;

            Unlocking = false;

            appState.SignalRedraw(GetType());
        }
    }
}
