using System;
using System.Linq;
using EasyBus.Abstraction;
using EasyBus.Types.MessageTypes;
using EasyBus.Shared.Helpers;

namespace EasyBus.Consumer.Handlers
{
    public class LOGMessageHandler : MessageHandler<LOGMessage>
    {
        protected override void Handle(LOGMessage message)
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
            get
            {
                return "LOG";
            }
        }
    }
}