using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace EasyBus.Shared.Helpers
{
    public static class ConfigHelper
    {
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("RabbitMqConnectionString");
            }
        }


        public static int RetryCount
        {
            get
            {
                return Convert.ToInt16(ConfigurationManager.AppSettings.Get("RetryCount"));
            }

        }

        public static int RetryInterval
        {
            get
            {
                return Convert.ToInt16(ConfigurationManager.AppSettings.Get("RetryInterval"));
            }

        }

        public static int MaxThreads
        {
            get
            {
                int maxThreads = Convert.ToInt16(ConfigurationManager.AppSettings.Get("MaxThreads"));
                return maxThreads > 20 ? 20 : maxThreads;
            }
        }


    }
}
