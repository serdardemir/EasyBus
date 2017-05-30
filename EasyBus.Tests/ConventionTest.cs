using EasyBus.Consumer.Handlers;
using Shouldly;
using Xunit;
using static EasyBus.Consumer.Handlers.OrderMessageHandler;

namespace EasyBus.Tests
{
	public class ConventionTest
	{
		[Fact]
		public void Should_GetQueueInfo_From_MessageHandler()
		{
			OrderMessageHandler orderHandler = new OrderMessageHandler();
			orderHandler.QueueName.ShouldBe("Order");

			LogMessageHandler logHandler = new LogMessageHandler();
			logHandler.QueueName.ShouldBe("Log");
		}

		[Fact]
		public void Should_GetQueueInfo_From_Error_MessageHandler()
		{
			OrderErrorHandler errorHandler = new OrderErrorHandler();
			errorHandler.QueueName.ShouldBe("Error_Order");
		}
	}
}