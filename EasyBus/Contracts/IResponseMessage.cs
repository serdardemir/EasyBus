namespace EasyBus.Contracts
{
	public interface IResponseMessage
	{
		bool Confirmed { get; set; }

		string Message { get; set; }
	}
}