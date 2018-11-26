using System;

namespace EMS.Contracts
{
    public interface IMessageReceiver<T> where T : IMessage
    {
        event EventHandler<T> Received;
    }
}