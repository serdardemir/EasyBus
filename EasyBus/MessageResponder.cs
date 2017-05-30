using EasyBus.Contracts;
using System;

namespace EasyBus
{
	public abstract class MessageResponder<TRequest, TResponse> : IResponseMessageHandler, IResponse
		where TRequest : IRequestMessage
		where TResponse : IResponseMessage
	{
		public abstract void Response<TRequest, TResponse>(Func<TRequest, TResponse> onResponse, IResponse responder);

		protected abstract IResponseMessage Handle(TRequest message);

		public abstract void StartRespond(ISubscriber sub);

		public void CreateResponse(IRequestMessage message)
		{
			var typedMessage = (TRequest)message;
			CreateResponse(typedMessage);
		}

		public IResponseMessage Handle(IRequestMessage message)
		{
			var typedMessage = (TRequest)message;
			return Handle(typedMessage);
		}
	}
}