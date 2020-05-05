using System;

namespace AI.Architecture.iRoot
{
    public static class AnonymsousObservable
    {
        public static IObservable<T> Create<T>(Func<IObserver<T>, IDisposable> subscribe)
        {
            return new Observable<T>(subscribe);
        }

        class Observable<T> :IObservable<T>
        {
            readonly Func<IObserver<T>, IDisposable> subscribe;

            public Observable(Func<IObserver<T>, IDisposable> func)
            {
                subscribe = func;
            }
            
            public IDisposable Subscribe(IObserver<T> observer)
            {
                return subscribe(observer);
            }
        }
    }
}