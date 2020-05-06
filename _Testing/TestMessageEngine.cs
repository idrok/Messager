using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BehaviorDesigner.Runtime.Tasks;
using Bnyx.Messager;
using Tang;
using UniRx;
using UnityEngine;
using Task = System.Threading.Tasks.Task;
using Time = Tang.Time;

namespace Bnyx.AI
{
    public class TestMessageEngine : MonoBehaviour
    {
        private BnyxMessager _messager;

        private void Start()
        {
            MessageManager.Instance.Init();
            
            MessageManager.Instance.Subscribe("MSG_LOCAL", obj =>
            {
                Debug.LogFormat($"obj1:{obj[0]}  obj2:{obj[1]}");
            });
            
            //MessageManager.Instance.Dispatch("MSG_LOCAL", new object[]{"bnyx", "rider"});

            MessageBroker.Default.Receive<MSG_NEWDAY>().Subscribe(
                entity => 
                Debug.LogFormat($"The Content:{entity.Emotion}"));

            // var pool = new PoolProvider();
            // pool.InitPoolable();
            // pool.Provider(Message.UI_BAG | Message.UI_TOP_HEAD);

            // var ob1 = Observable.Return(1);
            // var ob2 = Observable.Return(2);
            // IObservable<int> ob = Observable.Empty<int>();
            // var result = Observable.Merge(ob, ob1);
            // result.Subscribe(i => print("-------------------i:" + i));

            _messager = BnyxMessager.GetSingleton();
            var multiType = Message.UI_CORE_FORM | Message.UI_CORE_AWARD;
            _messager.Receive<MSG_NEWDAY>(multiType).Subscribe(msg1 => print($"---------------msg:{msg1.Emotion}"));

            Message msg2 = default(Message);
            var msg3 = msg2.Build(Message.UI_BAG, Message.UI_TOP_HEAD);
            Debug.LogFormat("-------------msg2:" + msg2);
            Debug.LogFormat("-------------msg3:" + msg3);
            // foreach (var VARIABLE in msg.)
            // {
            //     
            // }
            
            var l1 = new List<byte>() { 1, 3, 5 };
            var l2 = new List<byte>() { 2, 4, 6, 5, 3, 1 };
            // 超集 去重复
            var union = l1.Union(l2);
            union.ToList().ForEach(x => Debug.LogFormat("ll:" + x));

            // 差集 l2-l1
            var except = l2.Except(l1);
            Array.ForEach(except.ToArray(), by => Debug.LogFormat("by:" + by));

            // 串联
            var concat = l1.Concat(l2);
            concat.ToList().ForEach(y => Debug.LogFormat("cc:" + y));
            
            //TestThread();
        }

        private void TestThread()
        {
            // run on multi threads
            new Thread(() => Debug.LogFormat("UniId:" + Thread.CurrentThread.IsThreadPoolThread)).Start();
            
            // run on thread pool empty thread
            Task.Run(() => Debug.LogFormat("TaskId:" + Thread.CurrentThread.ManagedThreadId));
            
            ThreadPool.GetAvailableThreads(out int threads, out int io);
            Debug.LogFormat($"Available Threads:{threads} Completion:{io}");
            
            // 最大线程数 = works + io = 800 + 200 = 1000
            // 如果线程数量大于1000则进入队列等待
            ThreadPool.GetMaxThreads(out int max, out int iomax);
            Debug.LogFormat($"Max Threads:{max} Completion:{iomax}");

            ThreadPool.QueueUserWorkItem(_ => { Debug.LogFormat("work thread:" + Thread.CurrentThread.ManagedThreadId); });
            // ThreadPool.QueueUserWorkItem ≈ Task.Run ≈ Threading.Timer
            Task.Run(() => Debug.LogFormat($"Task.Run is threadpool:" + Thread.CurrentThread.IsThreadPoolThread));

            var timer = new System.Threading.Timer(_ =>
            {
                Debug.LogFormat($"Threading.Timer:" + Thread.CurrentThread.IsThreadPoolThread);
            }, null, TimeSpan.FromSeconds(5f), TimeSpan.Zero);
            //timer.

            Observable.Start(() => { Debug.LogFormat($"Observable.Start:" + Thread.CurrentThread.IsThreadPoolThread); })//true
                .Subscribe(_ => Debug.LogFormat($"Subscribe:" + Thread.CurrentThread.IsThreadPoolThread));//true
            
            Debug.LogFormat($"Unity.Thread:" + Thread.CurrentThread.IsThreadPoolThread);

            Observable.TimerFrame(10).Subscribe(_ =>
                Debug.LogFormat($"Observable.Timer:" + Thread.CurrentThread.IsThreadPoolThread));//false
            
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                //MessageManager.Instance.Dispatch("MSG_LOCAL", new object[]{"bnyx", Time.deltaTime.ToString()});
                // MessageBroker.Default.Publish( new MSG_NEWDAY() { Emotion = "not bad!"});
                
                var msg = new MSG_NEWDAY() { Emotion = "fresh sync code..." };
                _messager.Public(Message.UI_CORE_FORM | Message.UI_CORE_AWARD, msg, false);
            }
        }
        
        private class MSG_NEWDAY : IMessage
        {
            private string emotion;
            
            public string Emotion
            {
                get => emotion;
                set => emotion = value;

            }
        }
    }
    
    
}