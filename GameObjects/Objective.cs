using System;

namespace BadActor.GameObjects
{
    public class Objective : GameObject<Objective>
    {
        public bool Complete { get; private set; }

        public ObjectiveCriteria[] CriteriaForObjective { get; private set; }

        public Objective(string name, ObjectiveCriteria[] objectiveCriterias)
        {
            Name = name;

            CriteriaForObjective = objectiveCriterias;

            // ugh we shouldn't have to do this but stupid appstate isn't ready for event sub?
            appState.SignalRedraw(GetType());

            appState.OnGameStateChanged += completeIfCriteriaMet;
        }

        private void completeIfCriteriaMet()
        {
            Complete = true;
            foreach(ObjectiveCriteria objectiveCriteria in CriteriaForObjective)
            {
                if(objectiveCriteria.CheckCriteria() == false)
                {
                    Complete = false;
                }
            }

            if(Complete == true)
            {
                onComplete();
            }

            appState.SignalRedraw(GetType());
        }

        private void onComplete()
        {
            appState.OnGameStateChanged -= completeIfCriteriaMet;
        }

        public class ObjectiveCriteria
        {
            public string Description { get; private set; }
            public bool Met { get; set; }

            private Func<bool> doCriteriaCheck { get; set; }

            public ObjectiveCriteria(string description, Func<bool> checkCriteria)
            {
                Description = description;

                doCriteriaCheck = checkCriteria;
            }

            public bool CheckCriteria()
            {
                Met = doCriteriaCheck();

                return Met;
            }
        }
    }
}
