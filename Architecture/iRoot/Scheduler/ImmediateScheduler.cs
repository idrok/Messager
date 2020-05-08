using System;

namespace AI.Architecture.iRoot
{
    public class ImmediateScheduler : IScheduler
    {
        public IDisposable Schedule(Action action)
        {
            action();
            return Disposable.Empty;
        }

        public IDisposable Schedule(Action action, TimeSpan dueTime)
        {
            System.Threading.Thread.Sleep(dueTime);
            action();
            return Disposable.Empty;
        }

        public DateTimeOffset Now { get => DateTimeOffset.Now; }
    }
}