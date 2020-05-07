using System;

namespace AI.Architecture.iRoot
{
    public static class Disposable
    {
        public static readonly IDisposable Empty = EmptyDisposable.Singleton;

        public static IDisposable Create(Action dispose)
        {
            return new AnonymousDisposable(dispose);
        }
        
        class EmptyDisposable : IDisposable
        {
            public static EmptyDisposable Singleton = new EmptyDisposable();
            public EmptyDisposable() { }
            
            public void Dispose()
            {
                
            }
        }

        class AnonymousDisposable : IDisposable
        {
            private bool isDisposed = false;
            private readonly Action dispose;

            public AnonymousDisposable(Action dispose)
            {
                this.dispose = dispose;
            }
            
            public void Dispose()
            {
                if (isDisposed == false)
                {
                    isDisposed = true;
                    dispose();
                }
            }
        }
    }
}