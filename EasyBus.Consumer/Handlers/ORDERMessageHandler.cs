using EasyBus.Contracts;
using EasyBus.Shared.Helpers;
using EasyBus.Shared.Types;
using System;

namespace EasyBus.Consumer.Handlers
{
	public class OrderMessageHandler : MessageHandler<OrderMessage>
	{
		protected override void Handle(OrderMessage message)
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
			get { return new QueueInfo<OrderMessage>(this).ToString(); }
		}

		public class OrderErrorHandler : MessageHandler<OrderErrorMessage>, ErrorHandler
		{
			public override string QueueName
			{
				get { return new QueueInfo<OrderErrorMessage>(this).ToString(); }
			}

			protected override void Handle(OrderErrorMessage message)
			{
				throw new NotImplementedException();
			}
		}
	}
}