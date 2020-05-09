using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Architecture.iRoot
{
    public class GameLoopDispatcher : MonoBehaviour
    {
        static object _gate = new object();
        Queue<Action> mActionQueue = new Queue<Action>();

        private static GameLoopDispatcher mInstance;
        private static bool mInitialized;
        
        private GameLoopDispatcher() { }

        public static GameLoopDispatcher Instance
        {
            get
            {
                Initialize();
                return mInstance;
            }
        }

        static void Initialize()
        {
            if (mInitialized == false)
            {
                if (Application.isPlaying == false)
                {
                    return;
                }

                mInitialized = true;
                mInstance = new GameObject("GameLoopDispatcher").AddComponent<GameLoopDispatcher>();
            }
        }

        private void Awake()
        {
            mInstance = this;
            mInitialized = true;
        }

        private void Update()
        {
            lock (mActionQueue)
            {
                while (mActionQueue.Count != 0)
                {
                    var action = mActionQueue.Dequeue();
                    action();
                }
            }
        }

        public static void Post(Action item)
        {
            lock (_gate)
            {
                mInstance.mActionQueue.Enqueue(item);
            }
        }

        public static Coroutine StartCoroutine1(IEnumerator enumerator)
        {
            lock (_gate)
            {
                var _ = GameLoopDispatcher.Instance;
                return mInstance.StartCoroutine(enumerator);
            }
        }
    }
}