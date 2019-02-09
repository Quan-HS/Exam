using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RabbitConsumer
{
    public class Worker
    {
        public void Received()
        {
            Console.WriteLine("Consumer start");            

            using (IConnection conn = RabbitConnectionFactory.GetConnectionFactory().CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    string queueName = "Worker";

                    //声明一个队列
                    channel.QueueDeclare(
                      queue: queueName,//消息队列名称
                      durable: false,//是否缓存
                      exclusive: false,
                      autoDelete: false,
                      arguments: null
                       );

                    channel.BasicQos(0, 1, false);

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, em) =>
                    {

                        Thread.Sleep((new Random().Next(1, 6) * 1000));

                        byte[] message = em.Body;
                        Console.WriteLine("received message:" + Encoding.UTF8.GetString(message));

                        channel.BasicAck(em.DeliveryTag, true);

                    };

                    channel.BasicConsume(
                        queue: queueName,
                        //autoAck: true,
                        autoAck: false,
                        consumer: consumer
                        );

                    Console.ReadKey();

                }
            }
        }
    }
}
