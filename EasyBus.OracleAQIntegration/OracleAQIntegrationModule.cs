using System;
using System.Linq;
using Oracle.DataAccess.Client;

namespace EasyBus.OracleAQIntegration
{
    //Todo :Need refatoring
    public class OracleAQIntegrationModule
    {
        public OracleAQQueue GetOracleQueue(string queueName)
        {
            OracleConnection connection = new OracleConnection();
            connection.Open();

            OracleAQQueue queue = new OracleAQQueue(queueName, connection);
            queue.MessageType = OracleAQMessageType.Xml;
            queue.EnqueueOptions.Visibility = OracleAQVisibilityMode.Immediate;

            return queue;
        }
    }
}