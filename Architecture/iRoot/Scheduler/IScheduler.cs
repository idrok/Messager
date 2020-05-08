using System;

namespace AI.Architecture.iRoot
{
    public interface IScheduler
    {
        IDisposable Schedule(Action action);
        IDisposable Schedule(Action action, TimeSpan dueTime);
        DateTimeOffset Now { get; }
    }
}