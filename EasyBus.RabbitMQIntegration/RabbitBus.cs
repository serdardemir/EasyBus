using EasyBus.RabbitMQIntegration;
using EasyBus.Shared.Helpers;
using EasyNetQ;
using EasyNetQ.Consumer;
using System;
using System.Configuration;
using System.Linq;

namespace EasyBus.RabbitMQIntegration
{
    public interface IRabbitBus
    {
        IBus Bus { get; }
    }

    public class RabbitBus : IRabbitBus
    {


        private IBus bus = null;

        public IBus Bus
        {
            get
            {
                if (bus == null)
                {
                    var handler = DefineBusHandler();


                    bus = RabbitHutch.CreateBus(ConfigHelper.ConnectionString, handler, serviceRegister => serviceRegister
                    .Register<IEasyNetQLogger, QueueLogManager>()
                    .Register<IConsumerErrorStrategy, ErrorStrategy>()
                    );
                }

                return bus;
            }
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
