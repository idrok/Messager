namespace AI.Architecture.iRoot
{
    // 定义接口对应的观察类型，1对1
    public interface ISubject<in TSource, out TResult> : IObserver<TSource>, IObservable<TResult>
    {
        
    }

    // 定义接口的对应类型，1对多
    public interface ISubject<T> : ISubject<T, T>, IObserver<T>, IObservable<T>
    {
        
    }
    
    
    
}