using EasyBus.Consumer;
using EasyBus.Contracts;
using EasyBus.RabbitMQIntegration;
using EasyBus.Shared.Helpers;
using SimpleInjector;
using System;
using System.Linq;

namespace EasyBus.ConsumerService
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

			#region Validator

			Validator.NotNullOrEmpty(ConfigHelper.ConnectionString, "ConnectionString may not be null or empty.");
			Validator.NotNullOrEmpty(ConfigHelper.MaxThreads.ToString(), "Max Threads may not be null or empty.");
			Validator.NotNullOrEmpty(ConfigHelper.RetryCount.ToString(), "Retry Count may not be null or empty.");
			Validator.NotNullOrEmpty(ConfigHelper.RetryInterval.ToString(), "Retry Interval  may not be null or empty.");

			#endregion Validator

			#region Queue IoC

			//auto search all message handlers and register
			container.RegisterSingle<ISubscriber>(new Subscriber(container));

			Activator.CreateInstance(typeof(Integration));

			var assembly = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.StartsWith("EasyBus.Consumer")).FirstOrDefault();
			if (assembly == null)
				Environment.FailFast("Opps..");

			var handlers = assembly.GetExportedTypes().Where(x => x.IsMessageHandler(typeof(MessageHandler<>))).ToList();

			var responders = assembly.GetExportedTypes().Where(x => x.IsMessageHandler(typeof(MessageResponder<,>))).ToList();

			container.RegisterAll<IMessageHandler>(handlers);
			container.RegisterAll<IResponse>(responders);
			container.RegisterSingle<IPublisher>(new Publisher(container));

			container.RegisterSingle(new MessageEmitter(container));

			container.RegisterSingle(new RabbitMQIntegrationModule(container));

			#endregion Queue IoC

			container.Verify();
			return container;
		}
	}
}