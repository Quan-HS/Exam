using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace RabbitConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            //new Worker().Received();
            //new ExchangeCustomer().Received_Fanout();

            new ExchangeCustomer().Received_Direct(args);
        }
    }
}
