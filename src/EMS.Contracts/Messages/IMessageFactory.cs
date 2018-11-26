using EMS.Contracts;

namespace EMS.Queues.RabbitMQ
{
    public interface IMessageFactory<T> where T : IMessage
    {
        T Create(MessageData data);
    }

}