using UniRx;

namespace Bnyx.Messager
{
    public class FilterEntity
    {
        private bool mValid;
        private MessageBroker mBroker;

        public bool Valid 
        { 
            get => mValid;
            set => mValid = value;
        }

        public MessageBroker Broker 
        {
            get => mBroker;
            set => mBroker = value;
        }
    }
}