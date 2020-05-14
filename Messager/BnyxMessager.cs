// ReSharper disable once InvalidXmlDocComment
/**
 * 基于UniRx的游戏消息管理，可以实现同步消息，异步消息
 * v 1.1
 # 可以自定义消息池子
 # 可以实现多对多的消息发送和接收
 # 实例化同步锁
 # 内置游戏的通用消息管理
 # Copyright XHTT com.
 # 2020.4.27 Code from lenggl
// v 2.0
// 缓存最后一条消息数据，每次有订阅者发布当前池子里面的最新一条消息
// 可以避免新开的ui没有数据的尴尬局面
使用注意事项：正常1对1，多对多谨慎使用，多数据类型获取分别订阅数据
// v 2.1 
// 添加种子数据验证
// 第一个商用版本
todo 再次简化封装到项目

 */
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bnyx.AI;
using UniRx;

namespace Bnyx.Messager {
    public class BnyxMessager {
        // 同步消息，第一时间会收到，先发先到，有序消息列表
        // 异步消息，无序，会收到

        private bool mNeedDistinct = true;

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
        public IObservable<T> Receive<T> (MessageFixed fixedType, T seek = null) where T : IMessage, new()
        {
            MessageBroker broker = GetFixedBroker(fixedType);
            var value = new T();
            if (seek != null)
            {
                var vaild = seek.SEEK;
                if (vaild == true)
                {
                    value = seek;
                    seek.SEEK = false;
                }
                else
                {
                    throw new BnyxMessageException("Fatal：无效的种子数据或已经存在种子数据");
                }
            }
            return broker.Receive<T>(value);
        }
        
        /// <summary>
        /// 订阅消息组，游戏初始化
        /// </summary>
        /// <param name="multiType">作用域</param>
        /// <param name="seek">种子数据，第一次有效</param>
        /// <typeparam name="T">种子数据类型</typeparam>
        /// todo 种子数据不能通过指定更新，只能通过publish来更新
        /// <returns>可观察的结果</returns>
        public IObservable<T> Receive<T> (MessageVer2 multiType, T seek) where T : IMessage, new()
        {
            var query = mProvider.Provider(multiType);
            IObservable<T>[] array = new IObservable<T>[query.Count]; 
            
            var value = default(T);
            for (byte i = 0; i < array.Length; i++)
            {
                var entity = query[i];
                if (entity.Valid == true)
                {
                    if (seek != null)
                    {
                        var vaild = seek.SEEK;
                        if (vaild == true)
                        {
                            value = seek;
                            seek.SEEK = false;
                        }
                        else
                        {
                            throw new BnyxMessageException("Fatal：无效的种子数据或已经存在种子数据");
                        }
                    }
                    var receive = entity.Broker.Receive<T>(value);
                    array[i] = receive; 
                }
            }

            var merge = Observable.Merge(array);
            
            if (mNeedDistinct)
            {
               return merge.Distinct();
            }
            else
            {
                return merge;
            }
            // throw new BnyxMessageException($"当前接受的消息类型组不存在{multiType}");
        }

        /// <summary>
        /// 正常使用接口
        /// </summary>
        /// <param name="multiType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IObservable<T> Receive<T>(MessageVer2 multiType) where T : IMessage, new()
        {
            return Receive<T>(multiType, null);
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

        public void Public<T>(MessageVer2 multiType, T value, bool sync = true) where T : IMessage, new()
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