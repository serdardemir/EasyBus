using EasyBus.Abstraction.Contracts;

namespace EasyBus.Abstraction
{
    public abstract class MessageHandler<T> : IMessageHandler where T : IMessage
    {
        protected abstract void Handle(T message);

        public abstract string QueueName { get; }

        public void Handle(IMessage message)
        {
            var typedMessage = (T)message;
            Handle(typedMessage);
        }
    }
}