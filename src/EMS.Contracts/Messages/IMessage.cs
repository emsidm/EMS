using System;

namespace EMS.Contracts
{
    public interface IMessage
    {
        MessageData Data { get; set; }
    }

    public class MessageData
    {
        public MessageData(byte[] content)
        {
            this.Content = content;
        }

        public byte[] Content { get; set; }
    }
}