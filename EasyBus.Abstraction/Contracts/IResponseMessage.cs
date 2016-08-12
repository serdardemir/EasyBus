using System;
using System.Linq;

namespace EasyBus.Abstraction.Contracts
{
    public interface IResponseMessage
    {
        bool Confirmed { get; set; }

        string Message { get; set; }
    }
}