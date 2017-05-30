using EasyBus.Shared.Helpers;
using EasyBus.Types.MessageTypes;
using System;

namespace EasyBus.Consumer.Handlers
{
	public class LogMessageHandler : MessageHandler<LogMessage>
	{
		protected override void Handle(LogMessage message)
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
			get { return new QueueInfo<LogMessage>(this).ToString(); }
		}
	}
}