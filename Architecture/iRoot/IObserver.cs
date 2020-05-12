using System;

namespace AI.Architecture.iRoot
{
    public interface IObserver<in T>
    {
        void OnNext(T value);
        void OnError(Exception error);
        void OnCompleted();
    }
}