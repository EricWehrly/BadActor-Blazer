using System;
using System.Collections.Generic;
using System.Threading;


namespace BadActor.Shared
{
    public class GameLoop
    {
        private static GameLoop instance;

        // Keep this, so we can implement pausing later.
        private Timer timer;

        private List<Action> loopMethods = new List<Action>();

        public GameLoop()
        {
            manageInstance();

            timer = new Timer((e) =>
            {
                loop();
            }, null, 1000, 250);
        }

        public static void RegisterLoopMethod(Action method)
        {
            if (instance == null) new GameLoop();

            instance.loopMethods.Add(method);
        }

        // I'm aware that this is getting laughably bad
        private void manageInstance()
        {
            if(instance != null)
            {
                loopMethods.AddRange(instance.loopMethods);
            }

            instance = this;
        }

        private void loop()
        {
            foreach(var action in loopMethods)
            {
                action.Invoke();
            }
        }
    }
}
