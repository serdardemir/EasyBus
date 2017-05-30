using System;
using System.Runtime.Serialization;

namespace EasyBus.Exceptions
{
	[Serializable]
	public class EasyBusException :
	   Exception
	{
		public EasyBusException()
		{
		}

		public EasyBusException(string message)
			: base(message)
		{
		}

		public EasyBusException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected EasyBusException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}