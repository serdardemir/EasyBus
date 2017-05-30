namespace EasyBus.Contracts
{
	public interface IResponseMessageHandler
	{
		IResponseMessage Handle(IRequestMessage message);
	}
}