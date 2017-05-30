using EasyBus.Contracts;
using EasyBus.Shared.Helpers;
using System.Linq;
using Topshelf;
using Topshelf.Logging;

namespace EasyBus.ConsumerService
{
	public class ConsumerService : ServiceControl
	{
		private readonly LogWriter logger;

		public ConsumerService()
		{
			logger = HostLogger.Get<ConsumerService>();
		}

		public LogWriter Logger
		{
			get
			{
				return this.logger;
			}
		}

		public bool Start(HostControl hostControl)
		{
			var container = IocBootstrapper.Instance;
			var subscriber = container.GetInstance<ISubscriber>();

			// Find all message handlers in running process
			var handlers = container.GetAllInstances<IMessageHandler>();
			var responders = container.GetAllInstances<IResponse>();

			logger.Info(handlers.Count() + " message handlers found. Listening...");

			logger.Info(responders.Count() + " responder found. Listening...");

			//Start to subscribe (async) for all message handlers
			foreach (var handler in handlers)
			{
				for (int i = 0; i < ConfigHelper.MaxThreads; i++)
				{
					subscriber.Subscribe(handler);
				}
			}

			foreach (var responder in responders)
			{
				for (int i = 0; i < ConfigHelper.MaxThreads; i++)
				{
					responder.StartRespond(subscriber);
				}
			}

			return true;
		}

		public bool Pause()
		{
			return true;
		}

		public bool Continue()
		{
			return true;
		}

		public bool Stop(HostControl hostControl)
		{
			//dispose
			logger.Info("Consumer Service stoppped.");
			return true;
		}
	}
}