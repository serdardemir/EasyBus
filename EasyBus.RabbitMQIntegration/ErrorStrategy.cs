using EasyNetQ;
using System;

namespace EasyBus.RabbitMQIntegration
{
    public class ErrorStrategy : ConsumerErrorStrategy
    {
        public ErrorStrategy(IConnectionFactory connectionFactory, ISerializer serializer, IEasyNetQLogger logger, IConventions conventions, ITypeNameSerializer typeNameSerializer)
            : base(connectionFactory, serializer, logger, conventions, typeNameSerializer)
        {
        }
        public override EasyNetQ.Consumer.AckStrategy HandleConsumerError(EasyNetQ.Consumer.ConsumerExecutionContext context, Exception exception)
        {
            return base.HandleConsumerError(context, exception);
        }
    }
}