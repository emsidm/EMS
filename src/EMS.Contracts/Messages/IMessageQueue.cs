using System.Collections.Generic;

namespace EMS.Contracts
{
    public interface IMessageQueue<TMessage> where TMessage : IMessage
    {
        string Id { get; set; }
        IList<TMessage> Messages { get; set; }
    }
}