using System;

namespace Bnyx.AI
{
    public class BnyxMessageException : InvalidOperationException
    {
        public BnyxMessageException(string msg) : base(msg)
        {
            
        }
    }
}