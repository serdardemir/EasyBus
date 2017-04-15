using EasyBus.Abstraction.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBus.Types.MessageTypes
{
    public class ORDERMessage  : IMessage
    {
        public int OrderId { get; set; }
        public decimal Price { get; set; }
        public string OperationName { get; set; }

        public string CorrelationId { get; set; }
    }
}
