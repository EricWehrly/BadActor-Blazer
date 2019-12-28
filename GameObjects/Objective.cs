using System;

namespace BadActor.GameObjects
{
    public class Objective : GameObject<Objective>
    {
        public string Description { get; private set; }

        public bool Complete { get; private set; }

        // private Action CheckCriteria { get; set; }

        private Func<bool>[] checkCriteriaMet { get; set; }

        public Objective(string name, string description, Func<bool>[] criteriaMet)
        {
            Name = name;

            Description = description;

            checkCriteriaMet = criteriaMet;

            // ugh we shouldn't have to do this but stupid appstate isn't ready for event sub?
            appState.SignalRedraw(GetType());

            appState.OnGameStateChanged += completeIfCriteriaMet;
        }

        private void completeIfCriteriaMet()
        {
            Complete = true;
            foreach(Func<bool> criteriaCheckFunction in checkCriteriaMet)
            {
                if(criteriaCheckFunction() == false)
                {
                    Complete = false;
                }
            }

            Console.WriteLine("Complete is " + Complete);

            appState.SignalRedraw(GetType());
        }

        private void complete()
        {
            Complete = true;

            // appState.OnGameStateChanged -= CheckCriteria;
        }

        public class ObjectiveCriteria
        {
            // Func<bool> criteria met

            // string description
        }
    }
}
