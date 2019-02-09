using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Consumer start");

            IConnectionFactory connectionFactory = new ConnectionFactory
            {
                HostName = "120.79.179.19",
                Port = 5672,
                UserName = "yyq1",
                Password = "yuntong810"
            };

            using (IConnection conn = connectionFactory.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    string queueName = string.Empty;

                    if (args.Length > 0)
                        queueName = args[0];
                    else
                        queueName = "queue1";

                    //声明一个队列
                    channel.QueueDeclare(
                      queue: queueName,//消息队列名称
                      durable: false,//是否缓存
                      exclusive: false,
                      autoDelete: false,
                      arguments: null
                       );


                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, em) =>
                    {
                        byte[] message = em.Body;
                        Console.WriteLine("received message:" + Encoding.UTF8.GetString(message));


                    };

                   channel.BasicConsume(
                       queue: queueName,
                       autoAck: true,
                       consumer: consumer
                       );

                    Console.ReadKey();

                }
            }

        }
    }
}
