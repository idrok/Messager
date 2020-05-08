using System;
using System.Threading;

namespace AI.Architecture.iRoot
{
    public class ThreadPoolScheduler : IScheduler
    {
        public IDisposable Schedule(Action action)
        {
            ThreadPool.QueueUserWorkItem(_ => action());
            return Disposable.Empty;
        }

        public IDisposable Schedule(Action action, TimeSpan dueTime)
        {
            var timer = new Timer(_ => action(), null, dueTime, TimeSpan.Zero);
            
            return Disposable.Empty;
        }

        public DateTimeOffset Now { get => DateTimeOffset.Now; }
    }
}