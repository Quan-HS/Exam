using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("RabbitMQ start");




            IConnectionFactory connFactory = new ConnectionFactory
            {
                HostName = "120.79.179.19",
                Port = 5672,
                UserName = "yyq",
                Password = "yuntong810"
            };

            using (IConnection conn = connFactory.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    Console.WriteLine("create channel success");

                    string queueName = string.Empty;

                    if (args.Length > 0)
                    {
                        queueName = args[0];
                    }
                    else
                        queueName = "queue1";

                    channel.QueueDeclare(
                        queue: queueName,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                        );

                    while (true)
                    {
                        Console.WriteLine("message contant");

                        string message = Console.ReadLine();

                        byte[] body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(
                            exchange:"",
                            routingKey :queueName ,
                            basicProperties : null,
                            body :body
                            );

                        Console.WriteLine("send message success:" + message);
                    }
                }
            }

        }
    }
}
