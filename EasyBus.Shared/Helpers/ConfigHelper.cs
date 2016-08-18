using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyBus.Shared.Helpers
{
    public static class ConfigHelper
    {

        public static string ConnectionString = "host=localhost;virtualHost=/;username=guest;password=guest;timeout=10"; //AppConfig.Read<string>("RabbitMqConnectionString", string.Empty);

    }
}
