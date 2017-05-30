using EasyBus.Contracts;
using Newtonsoft.Json;
using System;
using System.Threading;

namespace EasyBus.Shared.Helpers
{
	public class Policy
	{
		#region Variables

		public static TimeSpan RetryInterval = TimeSpan.FromSeconds(ConfigHelper.RetryInterval);
		public static int RetryCount = ConfigHelper.RetryCount;

		#endregion Variables

		#region Methods

		public static void Execute(Action action, IMessage message)
		{
			Execute<object>(() =>
			{
				action();

				return null;
			}, message);
		}

		public static void Execute<T>(Func<OperationResult> action, IMessage message)
		{
			OperationResult result = new OperationResult();
			for (int retry = 0; retry < RetryCount; retry++)
			{
				try
				{
					result = action();

					if (result.HasError)
						throw result.Exception;

					CreateOrUpdateLog(message, result);
					return;
				}
				catch (Exception ex)
				{
					result.Exception = ex;
					Thread.Sleep(RetryInterval);
					//write error log to db
				}
			}
			CreateOrUpdateLog(message, result);

			/// Throws exception in order to make error strategy work
			throw new Exception(result.Exception.Message);
		}

		private static void CreateOrUpdateLog(IMessage message, OperationResult result)
		{
			//write error log to db
		}

		private static string Json(object value)
		{
			if (value == null)
				return string.Empty;
			else
				return JsonConvert.SerializeObject(value);
		}

		#endregion Methods
	}
}