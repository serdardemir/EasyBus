using System;
using System.Linq;

namespace EasyBus.Abstraction.Contracts
{
    public interface IRequestMessage
    {
        string Body { get; set; }
    }
}