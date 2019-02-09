using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQTest
{
    class ExchangeProducer
    {
        //发布订阅模式
        public void Product_Faout()
        {
            Console.WriteLine("exchange start");

            using (IConnection conn = RabbitConnectionFactory.GetConnectionFactory().CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    string exchangeName = "exchange1";

                    channel.ExchangeDeclare(
                        exchange: exchangeName,
                        type: "fanout"
                        );

                    while (true)
                    {
                        Console.WriteLine("please input message:");
                        string message = Console.ReadLine();

                        byte[] body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(
                            exchange: exchangeName,
                            routingKey: "",
                            basicProperties: null,
                            body: body
                            );
                        Console.WriteLine("publish success");
                    }
                }
            }

        }

        public void Product_Direct(string [] args)
        {
            if (args.Length == 0)
                throw new ArgumentException("args");

            Console.WriteLine("exchange direct start ");

            using (IConnection connection = RabbitConnectionFactory.GetConnectionFactory().CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    string exchangeName = "exchange2";

                    string routeKey = args[0];

                    channel.ExchangeDeclare(
                        exchange: exchangeName,
                        type: "direct"
                        );

                    while (true)
                    {
                        Console.WriteLine("please input message:");
                        string message = Console.ReadLine();

                        byte[] body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(
                            exchange: exchangeName,
                            routingKey: routeKey,
                            basicProperties: null,
                            body: body);

                        Console.WriteLine("publish success");
                    }


                }
            }
        }

        public void Product_Topic(string[] args)
        {
            if (args.Length == 0)
                throw new ArgumentException("args");

            Console.WriteLine("exchange direct start ");

            using (IConnection connection = RabbitConnectionFactory.GetConnectionFactory().CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    string exchangeName = "exchange3";

                    string routeKey = args[0];

                    channel.ExchangeDeclare(
                        exchange: exchangeName,
                        type: "topic"
                        );

                    while (true)
                    {
                        Console.WriteLine("please input message:");
                        string message = Console.ReadLine();

                        byte[] body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(
                            exchange: exchangeName,
                            routingKey: routeKey,
                            basicProperties: null,
                            body: body);

                        Console.WriteLine("publish success");
                    }


                }
            }
        }
    }
}
