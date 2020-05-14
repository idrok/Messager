using System;
using System.Runtime.Serialization;

namespace Bnyx.Messager
{
    /// <summary>
    ///  base class message for game
    /// </summary>
    public abstract class IMessage : IDisposable
    {
        /// <summary>
        /// 数据ID，以后也许用到
        /// </summary>
        private string _uuid;
        
        /// <summary>
        /// 该数据是否为种子数据
        /// </summary>
        private bool _seek;

        public bool SEEK
        {
            get => _seek;
            set => _seek = value;
        }
        
        public string MSG_ID
        { 
            get => _uuid;
            private set => _uuid = System.Guid.NewGuid().ToString("N");
        }

        public virtual void Dispose()
        {
            
        }
    }

}