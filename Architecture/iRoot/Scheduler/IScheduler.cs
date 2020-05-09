using System;

namespace AI.Architecture.iRoot
{
    public interface IScheduler
    {
        IDisposable Schedule(Action action);
        // due adj.预期; 预计;
        IDisposable Schedule(Action action, TimeSpan dueTime);
        DateTimeOffset Now { get; }
    }
}