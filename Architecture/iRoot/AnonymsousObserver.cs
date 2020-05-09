using System;

namespace AI.Architecture.iRoot
{
    public static class AnonymsousObserver
    {
        public static IObserver<T> Create<T>( Action<T> next, Action<Exception> error, Action complete) 
        {
            return new Observer<T>(next, error, complete);
        }

        class Observer<T> : IObserver<T>
        {
            readonly Action<T> onNext;
            readonly Action<Exception> onError;
            readonly Action onComplete;

            public Observer(Action<T> next, Action<Exception> error, Action complete)
            {
                onNext = next;
                onError = error;
                onComplete = complete;
            }
            
            public void OnNext(T value)
            {
                onNext(value);
            }

            public void OnError(Exception error)
            {
                onError(error);
            }

            public void OnComplete()
            {
                onComplete();
            }
        }
    }
}