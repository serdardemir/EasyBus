using System;
using System.Linq;
using EasyBus.Shared.Helpers;
using EasyNetQ;
using EasyNetQ.Consumer;

namespace EasyBus.RabbitMQIntegration
{
    public class RabbitMQIntegrationModule
    {
        private global::SimpleInjector.Container container;

        public RabbitMQIntegrationModule(global::SimpleInjector.Container container)
        {
            container.Register<IRabbitBus, RabbitBus>(SimpleInjector.Lifestyle.Transient);

            var handler = DefineBusHandler();
            
            IBus bus = RabbitHutch.CreateBus(ConfigHelper.ConnectionString, handler, serviceRegister => serviceRegister
                     .Register<IEasyNetQLogger, QueueLogManager>()
                     .Register<IConsumerErrorStrategy, ErrorStrategy>());

            container.RegisterSingle<IBus>(bus);

            this.container = container;
        }


        private static AdvancedBusEventHandlers DefineBusHandler()
        {
            var handler = new AdvancedBusEventHandlers(
                connected: (s, e) =>
                {

                },
                disconnected: (s, e) =>
                {


                },
                messageReturned: (s, e) =>
                {

                },
                unblocked: (s, e) =>
                {

                },
                blocked: (s, e) =>
                {

                });
            return handler;
        }

    }
}