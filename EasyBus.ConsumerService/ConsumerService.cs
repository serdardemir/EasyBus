using System;
using System.Linq;
using EasyBus.Abstraction.Contracts;
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
            var gets = container.GetAllInstances<IMessageHandler>();
            var responders = container.GetAllInstances<IResponse>();

            logger.Info(gets.Count() + " message handlers found. Listening...");

            logger.Info(responders.Count() + " responder found. Listening...");

            //Start to subscribe (async) for all message handlers
            foreach (var item in gets)
            {
                subscriber.Subscribe(item);
            }

            foreach (var responder in responders)
            {
                responder.StartRespond(subscriber);
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