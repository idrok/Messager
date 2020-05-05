using System;

namespace AI.Architecture.iRoot
{
    public interface IObserver<T>
    {
        void OnNext(T t);
        void OnError(Exception e);
        void OnComplete();
    }
}