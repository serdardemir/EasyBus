using System;
using System.Runtime.Serialization;

namespace EasyBus.Abstraction.Exceptions
{
	[Serializable]
	public class ConventionException : EasyBusException
	{
		public ConventionException()
		{
		}

		public ConventionException(string message)
			: base(message)
		{
		}

		public ConventionException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected ConventionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}