using EasyBus.Abstraction;
using EasyBus.Abstraction.Contracts;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Linq;
using System.Threading;


namespace EasyBus.Shared.Helpers
{
    public class Policy
    {
        #region Variables

        public static TimeSpan retryInterval;
        public static int retryCount;

        #endregion

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
            ReadConfiguration();
            OperationResult result = new OperationResult();
            for (int retry = 0; retry < retryCount; retry++)
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
                    Thread.Sleep(retryInterval);
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

        public static void ReadConfiguration()
        {
            retryCount = Convert.ToInt16(ConfigurationManager.AppSettings.Get("RetryCount"));
            retryInterval = TimeSpan.FromSeconds(Convert.ToDouble(ConfigurationManager.AppSettings.Get("RetryInterval")));
        }

        #endregion
    }
}