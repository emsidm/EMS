namespace EMS.Contracts
{
    public interface IMessageSender<T> where T : IMessage
    {
        void Send(T message);
    }
}