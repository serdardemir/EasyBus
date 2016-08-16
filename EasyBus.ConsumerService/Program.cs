using EasyBus.Abstraction.Contracts;
using EasyBus.Consumer.Handlers;
using EasyBus.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBus.ConsumerService
{
    class Program
    {
        static void Main(string[] args)
        {
            var console = new ConsoleWriter();
            var container = IocBootstrapper.Instance;

            try
            {
                console.WriteLine("IoC types registered.", ConsoleColor.Yellow);

                // Find subsriber. (RabbitMQ, ActiveMQ etc.)
                var subscriber = container.GetInstance<ISubscriber>();

                // Find all message handlers in running process
                var gets = container.GetAllInstances<IMessageHandler>();
                var responders = container.GetAllInstances<IResponse>();

                console.WriteLine(gets.Count() + " message handlers found.", ConsoleColor.Green);

                console.WriteLine(responders.Count() + " responder found", ConsoleColor.Magenta);

                //Start to subscribe (async) for all message handlers
                foreach (var item in gets)
                {
                    subscriber.Subscribe(item);
                }

                foreach (var responder in responders)
                {
                    responder.StartRespond(subscriber);
                }
            }
            catch (Exception exc)
            {
                console.Write(exc.Message, ConsoleColor.Red);

                Console.ReadKey();
                //send email 
            }
        }
    }
}