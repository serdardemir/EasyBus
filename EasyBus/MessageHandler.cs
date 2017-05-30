using EasyBus.Contracts;

namespace EasyBus
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