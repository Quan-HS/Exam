using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQTest
{
    class Worker
    {
        public void Producer()
        {
            Console.WriteLine("RabbitMQ start");


            using (IConnection conn = RabbitConnectionFactory.GetConnectionFactory().CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    Console.WriteLine("create channel success");

                    string queueName = "Worker";

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
                            exchange: "",
                            routingKey: queueName,
                            basicProperties: null,
                            body: body
                            );

                        Console.WriteLine("send message success:" + message);
                    }
                }
            }
        }
    }
}
