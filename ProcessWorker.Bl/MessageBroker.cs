using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProcessWorker.Bl.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ProcessWorker.Bl
{
    public class MessageBroker : IMessageBroker
    {
        private readonly ILogger<MessageBroker> _logger;
        private const string QUEUE_NAME = "processes";
        private const string POST_QUEUE_NAME = "processed";
        private readonly QueueOptions _options;

        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _chanel;

        public MessageBroker(IOptions<QueueOptions> options, ILogger<MessageBroker> logger)
        {
            _logger = logger;
            _options = options.Value;

            _factory = new ConnectionFactory
            {
                HostName = _options.HostName,
                UserName = _options.Username,
                Password = _options.Password
            };

            _connection = _factory.CreateConnection();
            _chanel = _connection.CreateModel();
        }

        public void SendMessage(int processId)
        {
            _chanel.QueueDeclare(QUEUE_NAME, true, false, false, null);
            var body = Encoding.UTF8.GetBytes(processId.ToString());
            var props = _chanel.CreateBasicProperties();
            props.Persistent = true;
            _chanel.BasicPublish(string.Empty, QUEUE_NAME, props, body);
        }

        public void ProcessMessage()
        {
            _chanel.QueueDeclare(QUEUE_NAME, true, false, false, null);
            var consumer = new EventingBasicConsumer(_chanel);
            consumer.Received += (model, ea) =>
            {
                var seconds = new Random().Next(30);
                _logger.Log(LogLevel.Information,
                    $"process message received. Will be processed in {seconds} seconds");
                Task.Delay(TimeSpan.FromSeconds(seconds)).ContinueWith(_ =>
                {
                    try
                    {
                        var body = ea.Body.ToArray();
                        if (body.Length == 0)
                        {
                            _chanel.BasicAck(ea.DeliveryTag, false);
                            throw new Exception("Wrong message in Queue");
                        }
                        _chanel.QueueDeclare(POST_QUEUE_NAME, true, false, false, null);
                        var props = _chanel.CreateBasicProperties();
                        props.Persistent = true;
                        _chanel.BasicPublish(string.Empty, POST_QUEUE_NAME, props, body);
                        _logger.Log(LogLevel.Information,
                            $"Starting processing an app process with id {Encoding.UTF8.GetString(body)}");
                        _chanel.BasicAck(ea.DeliveryTag, false);
                    }
                    catch (Exception e)
                    {
                        _logger.Log(LogLevel.Error, e.Message);
                        throw;
                    }
                });
            };
            _chanel.BasicConsume(QUEUE_NAME, false, consumer);
        }
    }
}
