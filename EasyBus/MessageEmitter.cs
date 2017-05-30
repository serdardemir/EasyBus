using EasyBus.Contracts;
using System;

namespace EasyBus
{
	public class MessageEmitter
	{
		private readonly global::SimpleInjector.Container container;

		public MessageEmitter(global::SimpleInjector.Container container)
		{
			this.container = container;
		}

		public void Emit<T>(T message) where T : IMessage
		{
			var publisher = container.GetInstance<IPublisher>();
			publisher.Publish(message);
		}

		public IResponseMessage Request<TRequest, TResponse>(TRequest request, Action<TResponse> onResponse)
			where TRequest : class
			where TResponse : class
		{
			var publisher = container.GetInstance<IPublisher>();
			return publisher.Request<TRequest, TResponse>(request, onResponse);
		}
	}
}