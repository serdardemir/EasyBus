using EasyBus.Shared.Helpers;
using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyBus.RabbitMQIntegration
{
    public class QueueLogManager : IEasyNetQLogger
    {
        private readonly ConsoleWriter writer;

        public QueueLogManager()
        {
            writer = new ConsoleWriter();
        }

        public void DebugWrite(string format, params object[] args)
        {
            writer.WriteLine(String.Format(format, args), ConsoleColor.DarkYellow);
        }

        public void InfoWrite(string format, params object[] args)
        {
            writer.WriteLine(String.Format(format, args), ConsoleColor.Green);
        }

        public void ErrorWrite(string format, params object[] args)
        {
            // writer.WriteLine(String.Format(format, args), ConsoleColor.Red);
        }

        public void ErrorWrite(Exception exception)
        {
            Write(string.Format("Error Message: {0} \n\n Stack Trace: {1}", exception.Message, exception.StackTrace));
        }

        private static void Write(string format, object[] args = null)
        {
            try
            {
                //if (args != null && args.Length > 0)
                //Providers.LogManager.GetLogger().Write(string.Format(format, args), "QueueInit");
            }
            catch (Exception exc)
            {
                throw;
            }
        }
    }
}