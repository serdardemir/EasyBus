using System;
using System.Linq;
using EasyBus.Abstraction.Contracts;
using Oracle.DataAccess.Client;
using EasyBus.Shared.Helpers;

namespace EasyBus.OracleAQIntegration
{
    public class Subscriber : ISubscriber
    {
        //Todo :Ioc intergation for OracleAQIntegrationModule
        public void Subscribe(IMessageHandler handler)
        {
            string queueName = handler.QueueName;

            var module = new OracleAQIntegrationModule();
            var queue = module.GetOracleQueue(queueName);
            var message = Consume(queue);
            handler.Handle(message);

        }

        private IMessage Consume(OracleAQQueue queue)
        {
            OracleAQMessage aqMessage = null;
            try
            {
                //Deserialize payload 
                aqMessage = queue.Dequeue();
                
            }
            catch (OracleException ex)
            {

            }
            return default(IMessage);
        }

        public void Subscribe(IResponse response)
        {
            throw new NotImplementedException();
        }

        public void Response<TRequest, TResponse>(IResponseMessageHandler obj)
            where TRequest : class
            where TResponse : class
        {
            throw new NotImplementedException();
        }
    }
}