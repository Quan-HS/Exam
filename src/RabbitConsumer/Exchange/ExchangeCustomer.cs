using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitConsumer
{
    class ExchangeCustomer
    {
        public void Received_Fanout()
        {
            int random = new Random().Next(1, 1000);
            Console.WriteLine($"random key :{random}");

            using (IConnection conn = RabbitConnectionFactory.GetConnectionFactory().CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    string exchangeName = "exchange1";

                    channel.ExchangeDeclare(
                        exchange: exchangeName,
                        type: "fanout");

                    string queueName = $"{exchangeName}_{random}";

                    channel.QueueDeclare(
                        queue: queueName,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                        );


                    channel.QueueBind(
                        queue: queueName,
                        exchange: exchangeName,
                        routingKey: ""
                        );


                    channel.BasicQos(0, 1, false);

                    var consumer = new EventingBasicConsumer(channel);

                    //接收事件
                    consumer.Received += (model, ea) =>
                    {
                        byte[] message = ea.Body;//接收到的消息
                        Console.WriteLine("接收到信息为:" + Encoding.UTF8.GetString(message));
                        //返回消息确认
                        channel.BasicAck(ea.DeliveryTag, true);
                    };
                    //开启监听
                    channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
                    Console.ReadKey();
                }
            }            
        }
        public void Received_Direct(string[] args)
        {
            if (args.Length == 0)
                throw new ArgumentException("args");

            int random = new Random().Next(1, 1000);

            Console.WriteLine($"random={random}");

            using (IConnection connection = RabbitConnectionFactory.GetConnectionFactory().CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    var exchangeName = "exchange2";

                    channel.ExchangeDeclare(exchange: exchangeName, type: "direct");

                    string queueName = exchangeName + "_" + random.ToString();

                    channel.QueueDeclare(
                        queue: queueName, 
                        durable: false, 
                        exclusive: false, 
                        autoDelete: false, 
                        arguments: null);

                    //将队列与交换机进行绑定
                    foreach (var routeKey in args)
                    {//匹配多个路由
                        channel.QueueBind(
                            queue: queueName, 
                            exchange: exchangeName, 
                            routingKey: routeKey);
                    }
                    //声明为手动确认
                    channel.BasicQos(0, 1, false);

                    var consumer = new EventingBasicConsumer(channel);
             
                    consumer.Received += (model, ea) =>
                    {
                        byte[] message = ea.Body;//接收到的消息
                        Console.WriteLine("接收到信息为:" + Encoding.UTF8.GetString(message));
    
                        channel.BasicAck(ea.DeliveryTag, true);
                    };
                    //开启监听
                    channel.BasicConsume(
                        queue: queueName, 
                        autoAck: false, 
                        consumer: consumer);
                    Console.ReadKey();
                }
            }
        }

        public void Received_Topic(string[] args)
        {
            if (args.Length == 0)
                throw new ArgumentException("args");

            int random = new Random().Next(1, 1000);

            Console.WriteLine($"random={random}");

            using (IConnection connection = RabbitConnectionFactory.GetConnectionFactory().CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    var exchangeName = "exchange3";

                    channel.ExchangeDeclare(
                        exchange: exchangeName, 
                        type: "topic");

                    string queueName = exchangeName + "_" + random.ToString();

                    channel.QueueDeclare(
                        queue: queueName,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    //将队列与交换机进行绑定
                    foreach (var routeKey in args)
                    {//匹配多个路由
                        channel.QueueBind(
                            queue: queueName,
                            exchange: exchangeName,
                            routingKey: routeKey);
                    }
                    //声明为手动确认
                    channel.BasicQos(0, 1, false);

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        byte[] message = ea.Body;//接收到的消息
                        Console.WriteLine("接收到信息为:" + Encoding.UTF8.GetString(message));

                        channel.BasicAck(ea.DeliveryTag, true);
                    };
                    //开启监听
                    channel.BasicConsume(
                        queue: queueName,
                        autoAck: false,
                        consumer: consumer);
                    Console.ReadKey();
                }
            }
        }
    }
}
