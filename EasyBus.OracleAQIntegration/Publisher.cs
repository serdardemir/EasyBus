using System;
using System.Linq;
using EasyBus.Abstraction.Contracts;
using EasyBus.Shared.Helpers;
using EasyBus.Types.MessageTypes;
using Oracle.DataAccess.Client;

namespace EasyBus.OracleAQIntegration
{
    public class Publisher : IPublisher
    {
        private readonly SimpleInjector.Container container;
        
        public Publisher(SimpleInjector.Container container)
        {
            this.container = container;
        }

        public void Publish(IMessage message)
        {
            var oracleIntegrationModule = container.GetInstance<OracleAQIntegrationModule>();
            var queue = oracleIntegrationModule.GetOracleQueue(message.GetType().Name);
            OracleAQMessage aqMessage = new OracleAQMessage(SerializationHelper.SerializeObjectAsXml(typeof(ORDERMessage), message));

            queue.Enqueue(aqMessage);
        }

        public IResponseMessage Request<TRequest, TResponse>(TRequest request, Action<TResponse> onResponse)
            where TRequest : class
            where TResponse : class
        {
            throw new NotImplementedException();
        }
    }
}