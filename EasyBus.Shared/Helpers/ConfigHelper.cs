using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyBus.Shared.Helpers
{
    public static class ConfigHelper
    {

        public static string ConnectionString = "Your_RabbitMq_ConnectionString"; //AppConfig.Read<string>("RabbitMqConnectionString", string.Empty);

    }
}
