using EasyBus.Abstraction;
using EasyBus.Abstraction.Contracts;
using EasyBus.Consumer.Handlers;
using EasyBus.Types.MessageTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EasyBus.Tests.AQIntegration
{
    public class AQIntegrationTests
    {
        [Fact]
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

        [Fact]
        public void Should_SubscribeOracleQueue()
        {
            var container = IocBootstrapper.Instance;
            var subscriber = container.GetInstance<ISubscriber>();
            var handler=new ORDERMessageHandler();
            subscriber.Subscribe(handler);
        }
    }
}
