using System;
using BadActor.Shared;

namespace BadActor.GameObjects
{
    public class Unlockable<T> : GameObject<T> where T : GameObjectBase
    {
        public bool Unlocked { get; protected set; }
        public double UnlockTime
        {
            get
            {
                return unlockTime;
            }
            protected set
            {
                unlockTime = value;
                UnlockTimeRemaining = unlockTime;
            }
        }
        public double UnlockTimeRemaining { get; private set; }
        public bool Unlocking { get; protected set; }

        private double unlockTime;

        public void StartUnlock()
        {
            GameLoop.RegisterLoopMethod(onGameLoop);

            Unlocking = true;

            appState.SignalRedraw(GetType());
        }

        private void onGameLoop(double elapsedSeconds)
        {
            UnlockTimeRemaining -= elapsedSeconds;

            if (UnlockTimeRemaining <= 0) unlock();
        }

        private void unlock()
        {
            Console.WriteLine(Name + " unlocked.");

            Unlocked = true;

            Unlocking = false;

            GameLoop.UnRegisterLoopMethod(onGameLoop);

            appState.GameStateChanged();

            appState.SignalRedraw(GetType());
        }
    }
}
