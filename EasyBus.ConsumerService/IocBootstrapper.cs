using EasyBus.Abstraction;
using EasyBus.Abstraction.Contracts;
using EasyBus.RabbitMQIntegration;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyBus.Shared.Helpers;
using EasyBus.Consumer;

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
            
            #region Queue IoC

            //auto search all message handlers and register
            container.RegisterSingle<ISubscriber>(new Subscriber(container));

            Activator.CreateInstance(typeof(Integration));

            var assembly = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains("EasyBus.Consumer")).FirstOrDefault();
            if (assembly == null)
                Environment.FailFast("Opps..");

            var handlers = assembly.GetExportedTypes().Where(x => x.IsMessageHandler(typeof(MessageHandler<>))).ToList();

            var responders = assembly.GetExportedTypes().Where(x => x.IsMessageHandler(typeof(MessageResponder<,>))).ToList();

            container.RegisterAll<IMessageHandler>(handlers);
            container.RegisterAll<IResponse>(responders);
            container.RegisterSingle<IPublisher>(new Publisher(container));

            container.RegisterSingle(new MessageEmitter(container));

            container.RegisterSingle(new RabbitMQIntegrationModule(container));

            #endregion

            container.Verify();
            return container;
        }
    }
}