using EasyBus.Abstraction;
using EasyBus.Abstraction.Contracts;
using EasyBus.RabbitMQIntegration;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;

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
            
            #region Queue IoC

            container.RegisterSingle<IPublisher>(new Publisher(container));

            container.RegisterSingle(new MessageEmitter(container));

            container.RegisterSingle(new RabbitMQIntegrationModule(container));

            #endregion

            container.Verify();
            return container;
        }
    }
}