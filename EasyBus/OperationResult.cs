using System;

namespace EasyBus
{
	public class OperationResult
	{
		#region Properties

		public object Request { get; set; }

		public object Response { get; set; }

		public int State { get; set; }

		public bool HasError { get; set; }

		public Exception Exception { get; set; }

		#endregion Properties

		public OperationResult()
		{
			//Set default value
			this.State = 0;
		}
	}
}