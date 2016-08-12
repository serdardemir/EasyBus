using System;
using System.Linq;

namespace EasyBus.Abstraction.Contracts
{
    public interface IResponseMessageHandler
    {
        IResponseMessage Handle(IRequestMessage message);
    }
}
