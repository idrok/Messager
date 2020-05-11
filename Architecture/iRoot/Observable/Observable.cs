using System;

namespace AI.Architecture.iRoot
{
    public static class Observable
    {
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
                    observer.OnComplete();
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
                }, observer.OnError, observer.OnComplete);
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
                var disposable = source.Subscribe(FilterOp, observer.OnError, observer.OnComplete);

                return disposable;
            }
            // step1.Create Observerable(IObserver in)
            return AnonymsousObservable.Create<TS>(Subscribe);
        }
    }
}