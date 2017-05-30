using EasyBus.Contracts;

namespace EasyBus.Shared.Types
{
	public class OrderMessage : IMessage
	{
		public int OrderId { get; set; }
		public decimal Price { get; set; }
		public string OperationName { get; set; }

		public string CorrelationId { get; set; }
	}
}