namespace ProjektAPI.Producer
{
    public interface IMessageProducer
    {
        public void SendMessage<T>(T message);
    }
}
