using System;
using System.Collections.Generic;

namespace AI.Architecture.iRoot.Subject
{
    public class Subject<T> : ISubject<T>
    {
        LinkedList<IObserver<T>> observers = new LinkedList<IObserver<T>>();
        public void OnNext(T t)
        {
            foreach (var observer in observers)
            {
                observer.OnNext(t);
            }
        }

        public void OnError(Exception e)
        {
            foreach (var observer in observers)
            {
                observer.OnError(e);
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