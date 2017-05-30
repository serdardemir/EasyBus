using EasyBus.Abstraction.Contracts;
using EasyBus.Abstraction.Exceptions;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace EasyBus.Abstraction
{
	public class QueueInfo<T> where T : IMessage
	{
		private readonly MessageHandler<T> handler;
		private readonly TypeInfo typeInfo;

		public QueueInfo(MessageHandler<T> handler)
		{
			this.handler = handler;
			this.typeInfo = handler.GetType().GetTypeInfo();
		}

		public string GetQueueName()
		{
			string name = GetBaseTypeName(typeInfo);

			if (!typeInfo.ImplementedInterfaces.Any(x => x == typeof(IMessageHandler)))
				throw new ConventionException($"Message handler '{typeInfo.Name}' should implement IMessageHandler.");

			if (!typeInfo.IsNested)
				return typeInfo.Name.Replace(name, string.Empty);

			if (typeInfo.IsNested && IsErrorHandler)
			{
				string handlerName = typeInfo.ImplementedInterfaces.FirstOrDefault(x => x == typeof(ErrorHandler)).Name;
				return string.Concat(ErrorQueueNamingConvention, typeInfo.Name.Replace(handlerName, string.Empty));
			}

			return string.Empty;
		}

		private static string GetBaseTypeName(TypeInfo type)
		{
			var name = type.BaseType.GetGenericTypeDefinition().Name;

			//remove `1
			int index = name.IndexOf('`');
			if (index > 0)
				name = name.Remove(index);
			return name;
		}

		public bool IsErrorHandler
		{
			get
			{
				return typeInfo.ImplementedInterfaces.Any(x => x == typeof(ErrorHandler));
			}
		}

		public override string ToString()
		{
			return this.GetQueueName();
		}

		public string ErrorQueueNamingConvention
		{
			get
			{
				return ConfigurationManager.AppSettings.Get("ErrorQueueNamingConvention");
			}
		}
	}
}