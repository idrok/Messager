using System;
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

            var messager = BnyxMessager.GetSingleton();
            var multiType = Message.UI_CORE_FORM | Message.UI_CORE_AWARD;
            messager.Receive<MSG_NEWDAY>(multiType).Subscribe(msg1 => print($"---------------msg:{msg1.Emotion}"));

            var msg = new MSG_NEWDAY() { Emotion = "fresh sync code..." };
            messager.Public(Message.UI_CORE_FORM, msg, false);

            Message msg2 = default(Message);
            var msg3 = msg2.Build(Message.UI_BAG, Message.UI_TOP_HEAD);
            Debug.LogFormat("-------------msg2:" + msg2);
            Debug.LogFormat("-------------msg3:" + msg3);
            // foreach (var VARIABLE in msg.)
            // {
            //     
            // }
            
            TestThread();
        }

        private void TestThread()
        {
            // run on multi threads
            new Thread(() => Debug.LogFormat("UniId:" + Thread.CurrentThread.ManagedThreadId)).Start();
            
            // run on thread pool empty thread
            Task.Run(() => Debug.LogFormat("TaskId:" + Thread.CurrentThread.ManagedThreadId));
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                //MessageManager.Instance.Dispatch("MSG_LOCAL", new object[]{"bnyx", Time.deltaTime.ToString()});
                MessageBroker.Default.Publish( new MSG_NEWDAY() { Emotion = "not bad!"});
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