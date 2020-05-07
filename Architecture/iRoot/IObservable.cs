namespace AI.Architecture.iRoot
{
    public interface IObservable<out T>
    {
        IDisposable Subscribe(IObserver<T> observer);
    }
}