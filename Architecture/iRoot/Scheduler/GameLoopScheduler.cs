using System;
using System.Collections;
using UnityEngine;

namespace AI.Architecture.iRoot
{
    public class GameLoopScheduler : IScheduler
    {
        public GameLoopScheduler()
        {
            var _ = GameLoopDispatcher.Instance;
        }
        
        public IDisposable Schedule(Action action)
        {
            GameLoopDispatcher.Post(action);
            return Disposable.Empty;
        }

        public IDisposable Schedule(Action action, TimeSpan dueTime)
        {
            GameLoopDispatcher.StartCoroutine1(DelayAction(action, dueTime));
            return Disposable.Empty;
        }

        IEnumerator DelayAction(Action action, TimeSpan dueTime)
        {
            yield return new WaitForSeconds((float) dueTime.TotalSeconds);
            GameLoopDispatcher.Post(action);
        }

        public DateTimeOffset Now { get => DateTimeOffset.Now; }
    }
}