using System;
using System.Linq;

namespace EasyBus.Abstraction.Contracts
{
    public interface IMessage
    {
        string OperationName { get; set; }

        string CorrelationId { get; set; }
    }
}
