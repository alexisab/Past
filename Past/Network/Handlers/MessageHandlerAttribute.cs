using System;

namespace Past.Network.Handlers
{
    public class MessageHandlerAttribute : Attribute
    {
        public uint MessageId { get; private set; }

        public MessageHandlerAttribute(uint id)
        {
            MessageId = id;
        }
    }
}
