using System;
using System.Collections.Concurrent;
using System.Text;
using EasyNetQ.SystemMessages;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using EasyNetQ;
using EasyNetQ.Consumer;
using System.Configuration;

namespace EasyBus.RabbitMQIntegration
{
    public class ConsumerErrorStrategy : IConsumerErrorStrategy
    {
        private readonly EasyNetQ.IConnectionFactory connectionFactory;
        private readonly ISerializer serializer;
        private readonly IEasyNetQLogger logger;
        private readonly IConventions conventions;
        private readonly ITypeNameSerializer typeNameSerializer;
        private readonly object syncLock = new object();

        private IConnection connection;
        private bool errorQueueDeclared;
        private readonly ConcurrentDictionary<string, string> errorExchanges = new ConcurrentDictionary<string, string>();
        

        public ConsumerErrorStrategy(
            EasyNetQ.IConnectionFactory connectionFactory,
            ISerializer serializer,
            IEasyNetQLogger logger,
            IConventions conventions,
            ITypeNameSerializer typeNameSerializer)
        {
            this.connectionFactory = connectionFactory;
            this.serializer = serializer;
            this.logger = logger;
            this.conventions = conventions;
            this.typeNameSerializer = typeNameSerializer;
        }

        private void Connect()
        {
            if (connection == null || !connection.IsOpen)
            {
                lock (syncLock)
                {
                    if (connection == null || !connection.IsOpen)
                    {
                        if (connection != null)
                        {
                            try
                            {
                                connection.Dispose();
                            }
                            catch
                            {
                                if (connection.CloseReason != null)
                                {
                                    this.logger.InfoWrite("Connection '{0}' has shutdown. Reason: '{1}'",
                                        connection, connection.CloseReason.Cause);
                                }
                                else
                                {
                                    throw;
                                }
                            }
                        }

                        connection = connectionFactory.CreateConnection();
                    }
                }
            }
        }

        private void DeclareDefaultErrorQueue(IModel model)
        {
            if (!errorQueueDeclared)
            {
                model.QueueDeclare(
                    queue: conventions.ErrorQueueNamingConvention(),
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
                errorQueueDeclared = true;
            }
        }

        private string DeclareErrorExchangeAndBindToDefaultErrorQueue(IModel model, ConsumerExecutionContext context)
        {
            var originalRoutingKey = context.Info.RoutingKey;

            return errorExchanges.GetOrAdd(originalRoutingKey, _ =>
            {
                var exchangeName = conventions.ErrorExchangeNamingConvention(context.Info);
                model.ExchangeDeclare(exchangeName, ExchangeType.Direct, durable: true);
                model.QueueBind(conventions.ErrorQueueNamingConvention(), exchangeName, originalRoutingKey);
                return exchangeName;
            });
        }

        private string DeclareErrorExchangeQueueStructure(IModel model, ConsumerExecutionContext context)
        {

            return "ErrorExchange_" + context.Info.RoutingKey;
            //   DeclareDefaultErrorQueue(model);
            return DeclareErrorExchangeAndBindToDefaultErrorQueue(model, context);
        }

        public virtual AckStrategy HandleConsumerError(ConsumerExecutionContext context, Exception exception)
        {
            try
            {
                Connect();

                using (var model = connection.CreateModel())
                {
                    var errorExchange = DeclareErrorExchangeQueueStructure(model, context);
                    var connectionString = ""; //ConfigHelper.ConnectionString;
                    //Get from IoC Container
                    var bus = RabbitHutch.CreateBus(connectionString);
                    var exchange = bus.Advanced.ExchangeDeclare(errorExchange, "direct", passive: true);
                    // Send 
                    bus.Advanced.Publish(exchange, errorExchange, false, false, context.Properties, context.Body);
                }
            }
            catch (BrokerUnreachableException)
            {
                // thrown if the broker is unreachable during initial creation.
                logger.ErrorWrite("EasyNetQ Consumer Error Handler cannot connect to Broker\n" +
                                  CreateConnectionCheckMessage());
            }
            catch (OperationInterruptedException interruptedException)
            {
                // thrown if the broker connection is broken during declare or publish.
                logger.ErrorWrite("EasyNetQ Consumer Error Handler: Broker connection was closed while attempting to publish Error message.\n" +
                                  string.Format("Message was: '{0}'\n", interruptedException.Message) +
                                  CreateConnectionCheckMessage());
            }
            catch (Exception unexpectedException)
            {
                // Something else unexpected has gone wrong :(
                logger.ErrorWrite("EasyNetQ Consumer Error Handler: Failed to publish error message\nException is:\n" +
                                  unexpectedException);
            }
            return AckStrategies.Ack;
        }

        public AckStrategy HandleConsumerCancelled(ConsumerExecutionContext context)
        {
            return AckStrategies.Ack;
        }

        private byte[] CreateErrorMessage(ConsumerExecutionContext context, Exception exception)
        {
            var messageAsString = Encoding.UTF8.GetString(context.Body);
            var error = new Error
            {
                RoutingKey = context.Info.RoutingKey,
                Exchange = context.Info.Exchange,
                Exception = exception.ToString(),
                Message = messageAsString,
                DateTime = DateTime.UtcNow,
                BasicProperties = context.Properties
            };

            return serializer.MessageToBytes(error);
        }

        private string CreateConnectionCheckMessage()
        {
            return
            "Please check EasyNetQ connection information and that the RabbitMQ Service is running at the specified endpoint.\n";
        }

        private bool disposed = false;

        public virtual void Dispose()
        {
            if (disposed)
                return;

            if (connection != null)
                connection.Dispose();

            disposed = true;
        }
    }
}