using EasyBus.Contracts;
using EasyBus.RabbitMQIntegration;
using SimpleInjector;

namespace EasyBus.Tests
{
	public static class IocBootstrapper
	{
		private static object syncObj = new object();

		private static Container rootFactory = null;

		public static Container Instance
		{
			get
			{
				if (rootFactory == null)
				{
					lock (syncObj)
					{
						if (rootFactory == null)
						{
							rootFactory = RegisterTypes();
						}
					}
				}

				return rootFactory;
			}
		}

		public static Container RegisterTypes()
		{
			Container container = new Container();

			#region Queue IoC RabbitMQ

			//container.RegisterSingle<IPublisher>(new Publisher(container));

			//container.RegisterSingle(new MessageEmitter(container));

			//container.RegisterSingle(new RabbitMQIntegrationModule(container));

			#endregion Queue IoC RabbitMQ

			#region Queue IoC OracleAQ

			container.RegisterSingle<IPublisher>(new OracleAQIntegration.Publisher(container));

			container.RegisterSingle<ISubscriber>(new OracleAQIntegration.Subscriber());

			container.RegisterSingle(new MessageEmitter(container));

			container.RegisterSingle(new RabbitMQIntegrationModule(container));

			#endregion Queue IoC OracleAQ

			container.Verify();
			return container;
		}
	}
}