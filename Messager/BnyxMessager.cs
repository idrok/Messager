/**
 * 基于UniRx的游戏消息管理，可以实现同步消息，异步消息
 * v 1.1
 # 可以自定义消息池子
 # 可以实现多对多的消息发送和接收
 # 实例化同步锁
 # 内置游戏的通用消息管理
 # Copyright XHTT com.
 # 2020.4.27 Code from lenggl
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Bnyx.AI;
using Tang;
using UniRx;
using UnityEngine;

namespace Bnyx.Messager {
    public class BnyxMessager {
        // 同步消息，第一时间会收到，先发先到，有序消息列表
        // 异步消息，无序，会收到

        #region [singleton] 单列模式创建
        private static BnyxMessager _singleton = null;
        private static object _lock = new object ();

        private BnyxMessager () {
            mProvider = new PoolProvider ();
        }

        public static BnyxMessager GetSingleton () {
            if (_singleton == null) {
                lock (_lock) {
                    _singleton = new BnyxMessager ();
                }
            }
            return _singleton;
        }
        #endregion

        private PoolProvider mProvider;

        #region 订阅组功能
        
        // 默认订阅的消息组，全局事件
        // 单一订阅
        public IObservable<T> Receive<T> (MessageFixed fixedType) where T : IMessage, new()
        {
            MessageBroker broker = GetFixedBroker(fixedType);
            return broker.Receive<T>();
        }

        // 消息组订阅，可以同时接收多个类型的消息
        public IObservable<T> Receive<T> (Message multiType) where T : IMessage, new()
        {
            var query = mProvider.Provider(multiType);
            IObservable<T> root = Observable.Empty<T>();
            List<IObservable<T>> caches = new List<IObservable<T>>();
            foreach(var entity in query)
            {
                if (entity.Valid == true)
                {
                    var receive = entity.Broker.Receive<T>();
                    caches.Add(receive);
                }
            }

            var observables = caches.ToArray();
            var merge = Observable.Merge(observables).Distinct();
            
            return merge;
            // throw new BnyxMessageException($"当前接受的消息类型组不存在{multiType}");
        }
        
        #endregion

        #region 发布组功能
        public void Public<T> (MessageFixed fixedType, T value, bool sync = true) where T : IMessage, new()
        {
            var broker = GetFixedBroker(fixedType);
            if (sync == true)
            {
                broker.Publish(value);
            }
            else
            {
                Task.Run(() => broker.Publish(value));
            }
        }

        public async void Public<T>(Message multiType, T value, bool sync = true) where T : IMessage, new()
        {
            var query = mProvider.Provider(multiType);
            if (sync == true)
            {
                Broadcast(value, query);
            }
            else
            {
                Task.Run(() => Broadcast(value, query));
            }
            // throw new BnyxMessageException($"当前发送的消息类型组不存在{multiType}");
        }

        private void Broadcast<T>(T value, List<FilterEntity> query) where T : IMessage, new()
        {
            foreach (var entity in query)
            {
                if (entity.Valid == true)
                {
                    entity.Broker.Publish(value);
                }
            }
            
            // Debug.LogFormat("----------------Broadcast:" + Thread.CurrentThread.ManagedThreadId);
        }

        #endregion
        
        private MessageBroker GetFixedBroker(MessageFixed fixedType)
        {
            MessageBroker broker = null;
            
            switch (fixedType) {
                case MessageFixed.FIXED_ASSERT:
                    broker = PoolProvider.ASSERT;
                    break;
                case MessageFixed.FIXED_GLOBAL:
                    broker = PoolProvider.GLOBAL;
                    break;
                case MessageFixed.FIXED_ACTIVITY:
                    broker = PoolProvider.ACTIVITY;
                    break;
                case MessageFixed.FIXED_PAYPAL:
                    broker = PoolProvider.PAYPAL;
                    break;
                case MessageFixed.FIXED_STORE:
                    broker = PoolProvider.STORE;
                    break;
            }

            return broker;
        }

    }
}