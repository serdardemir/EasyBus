using EasyBus.Contracts;
using System;
using System.Collections.Generic;

namespace EasyBus.Types.MessageTypes
{
	public class LogMessage : IMessage
	{
		public string CorrelationId { get; set; }

		public string OperationName { get; set; }

		public string ErrorId { get; set; }

		public string SourceId { get; set; }

		public string InfoUrl { get; set; }

		public string Error { get; set; }

		public string Host { get; set; }

		public string Type { get; set; }

		public string Message { get; set; }

		public string MachineName { get; set; }

		public string Detail { get; set; }

		public string User { get; set; }

		public string StatusCode { get; set; }

		public DateTime Time { get; set; }

		public string WebHostHtmlMessage { get; set; }

		public string Source { get; set; }

		public IDictionary<string, string> ServerVariables { get; set; }

		public IDictionary<string, string> Form { get; set; }

		public IDictionary<string, object> Cookies { get; set; }
	}
}