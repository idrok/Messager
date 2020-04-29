using System;
using System.Runtime.Serialization;

namespace Bnyx.Messager
{
    /// <summary>
    ///  base class message for game
    /// </summary>
    public abstract class IMessage : IDisposable
    {
        private string _uuid;
        
        public string MSG_ID
        { 
            get => _uuid;
            private set => _uuid = System.Guid.NewGuid().ToString("N");
        }

        public void Dispose()
        {
            
        }
    }

}