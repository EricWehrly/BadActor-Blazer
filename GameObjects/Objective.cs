using System;

namespace BadActor.GameObjects
{
    public class Objective : GameObject<Objective>
    {
        public bool Complete { get; private set; } = false;

        public ObjectiveCriteria[] CriteriaForObjective { get; private set; }

        public Objective[] Prerequisites { get; private set; } = new Objective[] { };

        public Objective(string name, ObjectiveCriteria[] objectiveCriterias,
            Objective[] prerequisites = null)
        {
            Name = name;

            CriteriaForObjective = objectiveCriterias;

            if (prerequisites != null)
            {
                Prerequisites = prerequisites;
            }

            // ugh we shouldn't have to do this but stupid appstate isn't ready for event sub?
            appState.SignalRedraw(GetType());

            appState.OnGameStateChanged += completeIfCriteriaMet;
        }

        private void completeIfCriteriaMet()
        {
            Complete = true;
            foreach(ObjectiveCriteria objectiveCriteria in CriteriaForObjective)
            {
                if(objectiveCriteria.Met == false)
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

            appState.GameStateChanged();
        }

        // TODO: Progressible criteria (like showing how many out of 10 workers we have)
        public class ObjectiveCriteria
        {
            public string Description { get; private set; }

            private Func<bool> doCriteriaCheck { get; set; }
            public bool Met { get
                {
                    return doCriteriaCheck();
                }
            }

            public ObjectiveCriteria(string description, Func<bool> checkCriteria)
            {
                Description = description;

                doCriteriaCheck = checkCriteria;
            }
        }
    }
}
