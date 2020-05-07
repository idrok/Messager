using System;

namespace AI.Architecture.iRoot
{
    public static partial class ObservableExtensions
    {
        public static IDisposable Subscribe<T>(this IObservable<T> source)
        {
            return source.Subscribe(AnonymsousObserver.Create<T>(_ => { }, _ => { }, () => { }));
        }

        public static IDisposable Subscribe<T>(this IObservable<T> source, Action<T> onNext)
        {
            return source.Subscribe(AnonymsousObserver.Create<T>(onNext, _ => { }, () => { }));
        }

        public static IDisposable Subscribe<T>(this IObservable<T> source, Action<T> onNext, Action<Exception> onError)
        {
            return source.Subscribe(AnonymsousObserver.Create<T>(onNext, onError, () => { }));
        }

        public static IDisposable Subscribe<T>(this IObservable<T> source, Action<T> onNext, Action onComplete)
        {
            return source.Subscribe(AnonymsousObserver.Create<T>(onNext, _ => { }, onComplete));
        }

        public static IDisposable Subscribe<T>(this IObservable<T> source, 
            Action<T> onNext, 
            Action<Exception> onError,
            Action onComplete)
        {
            return source.Subscribe(AnonymsousObserver.Create<T>(onNext, onError, onComplete));
        }
    }
}