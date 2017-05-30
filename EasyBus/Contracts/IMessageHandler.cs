namespace EasyBus.Contracts
{
	public interface IMessageHandler
	{
		void Handle(IMessage message);

		string QueueName { get; }
	}
}