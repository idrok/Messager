namespace Bnyx.Messager
{
    public static class MessageExtensions
    {
        public static Message Build(this Message entity, params Message[] multi)
        {
            Message empty = default(Message);
            foreach (var type in multi)
            {
                empty |= type;
            }

            return empty;
        }

        public static bool Contain(this Message entity, Message type)
        {
            var result = (entity & type) == type;
            return result;
        }
        
        public static Message Add(this Message entity, params Message[] multi)
        {
            foreach (var type in multi)
            {
                entity = entity |= type;
            }

            return entity;
        }

        public static Message Remove(this Message entity, Message type)
        {
            var result = entity ^= type;
            return result;
        }
    }
}