using System;
using System.Collections.Generic;

namespace AI.Architecture.iRoot.Subject
{
    public class Subject<T> : ISubject<T>
    {
        LinkedList<IObserver<T>> observers = new LinkedList<IObserver<T>>();
        public void OnNext(T value)
        {
            foreach (var observer in observers)
            {
                observer.OnNext(value);
            }
        }

        public void OnError(Exception error)
        {
            foreach (var observer in observers)
            {
                observer.OnError(error);
            }
        }

        public void OnComplete()
        {
            foreach (var observer in observers)
            {
                observer.OnComplete();
            }
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            var node = new LinkedListNode<IObserver<T>>(observer);
            observers.AddLast(node);
            return Disposable.Create(() => observers.Remove(node));
        }
    }
}