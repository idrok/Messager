using System;

namespace AI.Architecture.iRoot
{
    public static class Observable
    {
        // 程序执行流程解析
        // IObservable可观测结果接口定义 => IDisposable Subscribe(IObserver<T> observer);
        // 这个接口定义非常异常，输入是一个接口，返回也是一个接口，输入带 操作返回
        // 调用的其实是AnonymsousObservable中的Observable的Subscribe方法
        // 操作的接口数据最终以实际的子类为准来进行输出
        public static IObservable<int> Range(int start, int count)
        {
            return AnonymsousObservable.Create<int>(observer =>
            {
                try
                {
                    for (int i = 0; i < count; i++)
                    {
                        observer.OnNext(start++);
                    }
                    observer.OnCompleted();
                }
                catch (Exception e)
                {
                    observer.OnError(e);
                }
                
                return Disposable.Empty;
            });
        }

        public static IObservable<TR> Select<TS, TR>(this IObservable<TS> source, Func<TS, TR> selector)
        {
            return AnonymsousObservable.Create<TR>(observer =>
            {
                return source.Subscribe(next =>
                {
                    var result = selector(next);
                    observer.OnNext(result);
                }, observer.OnError, observer.OnCompleted);
            });
        }
        
        /// <summary>
        /// 功能定义三步，创建可观测结果的对象，对数据操作，将结果返回到源
        /// 程序的执行步骤，由外到里
        /// </summary>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <typeparam name="TS"></typeparam>
        /// <returns></returns>
        public static IObservable<TS> Select<TS>(this IObservable<TS> source, Func<TS, bool> predicate)
        {
            IDisposable Subscribe(IObserver<TS> observer)
            {
                // step2.对数据进行操作
                void FilterOp(TS next)
                {
                    var passed = predicate(next);
                    if (passed) observer.OnNext(next);
                }
                
                // step3.将结果订阅到源
                var disposable = source.Subscribe(FilterOp, observer.OnError, observer.OnCompleted);

                return disposable;
            }
            // step1.Create Observerable(IObserver in)
            return AnonymsousObservable.Create<TS>(Subscribe);
        }

        public static IObservable<T> Where<T>(this IObservable<T> source, Predicate<T> predicate)
        {
            // func函数指针，没有带泛型的版本
            IDisposable FuncSubscribe(IObserver<T> observer)
            {
                void FilterOp(T value)
                {
                    var vaild = predicate(value);
                    if (vaild == true)
                    {
                        observer.OnNext(value);
                    }
                }
                // 因为需要对source进行数据筛选，重写了OnNext方法
                var disposable = source.Subscribe(AnonymsousObserver.Create<T>(FilterOp, observer.OnError, observer.OnCompleted));
                return disposable;
            }
            // 入口函数如果有泛型版本，必须指定约束之后的数据类型
            return AnonymsousObservable.Create<T>(FuncSubscribe);
        }
    }
}