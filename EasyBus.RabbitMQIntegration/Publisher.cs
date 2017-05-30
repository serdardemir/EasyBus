using EasyNetQ;
using Newtonsoft.Json;
using System.Configuration;

namespace EasyBus.RabbitMQIntegration
{
	public class Publisher : EasyBus.Contracts.IPublisher
	{
		private SimpleInjector.Container container;

		public string ExchangeType { get; set; }

		public Publisher(SimpleInjector.Container container)
		{
			this.container = container;
			ExchangeType = ConfigurationManager.AppSettings.Get("ExchangeType");
		}

		public void Publish(EasyBus.Contracts.IMessage message)
		{
			var bus = container.GetInstance<IBus>();
			var exchangeName = message.GetType().Name;
			var exchange = bus.Advanced.ExchangeDeclare(exchangeName, ExchangeType, passive: true);
			var json = JsonConvert.SerializeObject(message);
			var rabbitMessage = new Message<string>(json);
			rabbitMessage.Properties.DeliveryMode = MessageDeliveryMode.Persistent;

			bus.Advanced.Publish(exchange, exchangeName, false, false, rabbitMessage);
		}

		public EasyBus.Contracts.IResponseMessage Request<TRequest, TResponse>(TRequest request, System.Action<TResponse> onResponse)
			where TRequest : class
			where TResponse : class
		{
			var bus = container.GetInstance<IRabbitBus>().Bus;

			return bus.Request<TRequest, TResponse>(request) as EasyBus.Contracts.IResponseMessage;
		}
	}
}