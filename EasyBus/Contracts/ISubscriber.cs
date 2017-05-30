namespace EasyBus.Contracts
{
	public interface ISubscriber
	{
		void Subscribe(IMessageHandler handler);

		void Subscribe(IResponse response);

		void Response<TRequest, TResponse>(IResponseMessageHandler obj)
			where TRequest : class
			where TResponse : class;
	}
}