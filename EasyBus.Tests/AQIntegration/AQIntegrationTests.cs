using EasyBus.Consumer.Handlers;
using EasyBus.Contracts;
using EasyBus.Shared.Types;
using Xunit;

namespace EasyBus.Tests.AQIntegration
{
	public class AQIntegrationTests
	{
		[Fact(Skip = "Need Oracle DataAccess compability dll for prevent BadImage Exception")]
		public void Should_Publish_OracleQueue()
		{
			var container = IocBootstrapper.Instance;
			var messageEmitter = container.GetInstance<MessageEmitter>();
			OrderMessage orderMessage = new OrderMessage()
			{
				OrderId = 1,
				Price = 100
			};

			messageEmitter.Emit(orderMessage);
		}

		[Fact(Skip = "Need Oracle DataAccess compability dll for prevent BadImage Exception")]
		public void Should_SubscribeOracleQueue()
		{
			var container = IocBootstrapper.Instance;
			var subscriber = container.GetInstance<ISubscriber>();
			var handler = new OrderMessageHandler();
			subscriber.Subscribe(handler);
		}
	}
}