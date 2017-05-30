using EasyBus.Abstraction.Contracts;

namespace EasyBus.Types.MessageTypes
{
	public class OrderMessage : IMessage
	{
		public int OrderId { get; set; }
		public decimal Price { get; set; }
		public string OperationName { get; set; }

		public string CorrelationId { get; set; }
	}
}