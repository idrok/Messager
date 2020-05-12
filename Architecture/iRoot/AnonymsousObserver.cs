using System;

namespace AI.Architecture.iRoot
{
    public static class AnonymsousObserver
    {
        public static IObserver<T> Create<T>(Action<T> next, Action<Exception> error, Action completed) 
        {
            return new Observer<T>(next, error, completed);
        }

        class Observer<T> : IObserver<T>
        {
            readonly Action<T> onNext;
            readonly Action<Exception> onError;
            readonly Action onCompleted;

            public Observer(Action<T> next, Action<Exception> error, Action completed)
            {
                onNext = next;
                onError = error;
                onCompleted = completed;
            }
            
            public void OnNext(T value)
            {
                onNext(value);
            }

            public void OnError(Exception error)
            {
                onError(error);
            }

            public void OnCompleted()
            {
                onCompleted();
            }
        }
    }
}