using System;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;

namespace BadActor.Shared
{
    public class GameLoop
    {
        private static GameLoop instance;

        // Keep this, so we can implement pausing later.
        private Timer timer;
        Stopwatch stopwatch = new Stopwatch();

        private List<Action<double>> loopMethods = new List<Action<double>>();

        public GameLoop()
        {
            manageInstance();
            stopwatch.Start();

            timer = new Timer((e) =>
            {
                loop();
            }, null, 1000, 250);
        }

        public static void RegisterLoopMethod(Action<double> method)
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
            stopwatch.Stop();
            foreach (var action in loopMethods)
            {
                action.Invoke(stopwatch.Elapsed.TotalSeconds);
            }
            stopwatch.Restart();
        }
    }
}
