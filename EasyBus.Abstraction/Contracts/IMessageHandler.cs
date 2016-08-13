using System;
using System.Linq;

namespace EasyBus.Abstraction.Contracts
{
    public interface IMessageHandler
    {
        void Handle(IMessage message);

        string QueueName { get; }
    }
}