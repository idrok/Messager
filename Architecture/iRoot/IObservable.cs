namespace AI.Architecture.iRoot
{
    public interface IObservable<out T>
    {
        /// <summary>
        /// 面向接口编程，调用的是实现的具体子类
        /// 父类声明，子类调用
        /// 面向对象语言的基本属性：多态
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
        IDisposable Subscribe(IObserver<T> observer);
    }
}