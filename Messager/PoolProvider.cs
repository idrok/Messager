using System;
using System.Collections.Generic;
using Bnyx.AI;
using UniRx;
using UnityEngine;

namespace Bnyx.Messager
{
    public class PoolProvider
    {
        // 提供可以调度的消息池子，池子1，池子2，池子N 
        // 具体划分为 ui ai global activity 
        // 可以新建消息组别
        // 提供异步池子和同步池子entity
        
        // 资源通知
        public static readonly MessageBroker ASSERT = new MessageBroker();
        
        // 全局通知
        public static readonly MessageBroker GLOBAL = new MessageBroker();
        
        // 活动通知
        public static readonly MessageBroker ACTIVITY = new MessageBroker();

        // 充值通知
        public static readonly MessageBroker PAYPAL = new MessageBroker();

        // 商城通知
        public static readonly MessageBroker STORE = new MessageBroker();

        public static Dictionary<Message, MessageBroker> mMessageDic;

        private byte mLimits = 30;
        private static bool mInitialized = false;

        public PoolProvider()
        {
            if (mInitialized == false)
            {
                InitPoolable();
                mInitialized = true;
                Debug.LogFormat("Initialized once.");
            }
        }

        private void InitPoolable()
        {
            // limit pools max up to [127]
            mMessageDic = new Dictionary<Message, MessageBroker>(sbyte.MaxValue);
            var values = Enum.GetValues(typeof(Message));
            var count = values.Length;
            if (count < mLimits)
            {
                foreach (var value in values)
                {
                    Debug.LogFormat($"---------------name:{value.ToString()}---value:{(uint) value}");
                    var broker = new MessageBroker();
                    var type = (Message) value;
                    mMessageDic.Add(type, broker);
                }
            }
            else
            {
                throw new BnyxMessageException($"Fatal:最大池子数量超过限制{mLimits}");
            }
        }

        // 从池子里面取出需要的消息池子对象
        // 可以取出多个
        public List<FilterEntity> Provider(Message multiType)
        {
            var result = Query(multiType);

            return result;

        }

        private List<FilterEntity> Query(Message multiType)
        {
            var cache = new List<FilterEntity>();
            var values = Enum.GetValues(typeof(Message));
            foreach (var value in values)
            {
                var type = (Message) value;
                if ((multiType & type) == type) 
                {
                    var broker = mMessageDic[type];
                    var entity = new FilterEntity();
                    entity.Broker = broker;
                    entity.Valid = true;
                    cache.Add(entity);
                    // Debug.LogFormat($"---------------name:{value.ToString()}---value:{(uint) value}"); 
                }
                
            }

            return cache;
        }

    }
}