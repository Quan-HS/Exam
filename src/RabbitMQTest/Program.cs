using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //new Worker().Producer();
            //new ExchangeProducer().Product_Faout();

            new ExchangeProducer().Product_Direct(args);

        }
    }
}
