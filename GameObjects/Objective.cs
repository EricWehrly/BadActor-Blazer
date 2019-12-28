using System;

namespace BadActor.GameObjects
{
    public class Objective : GameObject<Objective>
    {
        public string Description { get; private set; }

        public bool Complete { get; private set; }

        private Action CheckCriteria { get; set; }

        public Objective(string name, string description, Action checkCriteria)
        {
            Name = name;

            Description = description;

            CheckCriteria = checkCriteria;

            appState.OnGameStateChanged += CheckCriteria;

            // ugh we shouldn't have to do this but stupid appstate isn't ready for event sub?
            appState.SignalRedraw(GetType());
        }

        private void complete()
        {
            Complete = true;

            appState.OnGameStateChanged -= CheckCriteria;
        }
    }
}
