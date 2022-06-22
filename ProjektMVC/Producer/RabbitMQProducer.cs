using ProjektAPI.Models;
using System;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text;

namespace ProjektAPI.Producer
{
    public class RabbitMQProducer : IMessageProducer
    {
        public void SendMessage<T>(T message)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("message", exclusive:false);

            var json = JsonSerializer.Serialize(message);

            var publishJson = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: "message", body: publishJson);   
        }
    }
}
