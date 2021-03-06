﻿using System.Collections.Generic;
using System;

namespace BadActor.GameObjects
{
    public abstract class GameObject<T> : GameObjectBase where T : GameObjectBase
    {
        public static List<T> List { get; } = new List<T>();

        public static T Get(string name)
        {
            foreach (var gameObject in List)
            {
                if (gameObject.Name == name) return gameObject;
            }

            return null;
        }

        public GameObject()
        {
            List.Add(this as T);

            appState.SignalRedraw(typeof(T));
        }
    }
}
