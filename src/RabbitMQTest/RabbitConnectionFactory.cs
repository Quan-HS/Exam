using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQTest
{
    public class RabbitConnectionFactory
    {
        public static ConnectionFactory GetConnectionFactory()
        {
            return new ConnectionFactory
            {
                HostName = GlobalConfigution.HostName,
                Port = GlobalConfigution.Port,
                UserName = GlobalConfigution.UserName,
                Password = GlobalConfigution.Password
            };
        }
    }
}
