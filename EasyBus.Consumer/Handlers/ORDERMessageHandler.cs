using EasyBus.Abstraction;
using EasyBus.Abstraction.Contracts;
using EasyBus.Shared.Helpers;
using EasyBus.Types.MessageTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBus.Consumer.Handlers
{
    public class ORDERMessageHandler : MessageHandler<ORDERMessage>
    {

        protected override void Handle(ORDERMessage message)
        {
            Policy.Execute<OperationResult>(() =>
            {
                OperationResult operationResult = new OperationResult();

                try
                {
                    //.....

                }
                catch (Exception exc)
                {
                    operationResult.HasError = true;
                    operationResult.Exception = exc;
                }
                return operationResult;
            }, message);
        }

        public override string QueueName
        {
            get { return "ORDERMESSAGE"; }
        }
    }
}
