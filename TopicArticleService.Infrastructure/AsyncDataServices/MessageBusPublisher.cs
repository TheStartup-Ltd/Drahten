﻿using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using TopicArticleService.Application.Dtos.SearchService;

namespace TopicArticleService.Infrastructure.AsyncDataServices
{
    internal sealed class MessageBusPublisher : IMessageBusPublisher
    {
        private readonly IConfiguration _configuration;
        private IConnection _connection;
        private IModel _channel;
        private string _queueName;

        public MessageBusPublisher(IConfiguration configuration)
        {
            _configuration = configuration;
            InitializeRabbitMq();
        }

        private void InitializeRabbitMq()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"])
            };

            _connection = factory.CreateConnection();

            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: "search_service", type: ExchangeType.Direct);

            _connection.ConnectionShutdown += RabbitMqConnectionShutDown;
        }

        private void RabbitMqConnectionShutDown(object sender, ShutdownEventArgs args)
        {
            Console.WriteLine("--> Connection shutdown.");
        }


        //TODO:
        //Maybe some class that will encapsulate this data (the message, exchange etc.). This way the method will be generic.
        private void SendMessage(string message)
        {
            var messageBody = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "search_service",
                                  routingKey: "search_service.smc",
                                  body: messageBody);

            Console.WriteLine($"--> TopicArticleService have sent a message: {message}!");

            //TODO: Log the message.
        }

        public void PublishNewDocument(DocumentDto documentDto)
        {
            var message = JsonSerializer.Serialize(documentDto);

            if (_connection.IsOpen)
            {
               //TODO: Log the message.

                SendMessage(message);
            }
            else
            {
                //TODO: Retrying if the connection is not available.

                //TODO: Log the message.

                Console.WriteLine("--> UserService RabbitMQ connection is CLOSED!");
            }
        }


        public void Dispose()
        {
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }
    }
}