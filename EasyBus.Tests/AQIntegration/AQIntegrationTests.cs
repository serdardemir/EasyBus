using System;
using System.Linq;
using EasyBus.Abstraction;
using EasyBus.Abstraction.Contracts;
using EasyBus.Consumer.Handlers;
using EasyBus.Types.MessageTypes;
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
            ORDERMessage orderMessage = new ORDERMessage()
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
            var handler = new ORDERMessageHandler();
            subscriber.Subscribe(handler);
        }
    }
}