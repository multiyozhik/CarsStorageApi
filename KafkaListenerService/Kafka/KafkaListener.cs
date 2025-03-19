using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace KafkaListenerService.Kafka
{
	/// <summary>
	/// ����� consumer, ������ �������������� ������� ��������� �������� �� Kafka �������.
	/// </summary>
	/// <typeparam name="TMessage">������ ����������� ���������.</typeparam>
	public class KafkaListener<TMessage> : BackgroundService
    {
        /// <summary>
        /// �������� ���� ������ ���������.
        /// </summary>
        private readonly string topic;

		/// <summary>
		/// �������� ���� consumer.
		/// </summary>
		private readonly IConsumer<string, TMessage> consumer;

        /// <summary>
        /// �������� ���� ����������� ��� ��������� ���������.
        /// </summary>
        private readonly IMessageHandler<TMessage> messageHandler;


		/// <summary>
		/// ����������� ������ consumer, ��������������� ������� ��������� �������� �� Kafka �������.
		/// </summary>
		/// <param name="kafkaConfig">������ ������������ Kafka.</param>
		/// <param name="messageHandler">������ ����������� ��� ��������� ���������.</param>
		public KafkaListener(IOptions<KafkaConfig> kafkaConfig, IMessageHandler<TMessage> messageHandler)
        {
            var config = new ConsumerConfig()
            {
                BootstrapServers = kafkaConfig.Value.BootstrapService,
                GroupId = kafkaConfig.Value.GroupId
            };
            this.messageHandler = messageHandler;
            topic = kafkaConfig.Value.Topic;
            consumer = new ConsumerBuilder<string, TMessage>(new List<KeyValuePair<string, string>>(config))
                .SetValueDeserializer(new KafkaValueDeserializer<TMessage>())
                .Build();
        }


        /// <summary>
        /// ����� ��� ���������� �������� �������������.
        /// </summary>
        /// <param name="stoppingToken">������ ������ ������.</param>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(async () => { await ConsumeAsync(stoppingToken); }, stoppingToken);
        }

		/// <summary>
		/// ����� ��� �������� ������ � ��������� ���������.
		/// </summary>
		/// <param name="stoppingToken">������ ������ ������.</param>
		private async Task ConsumeAsync(CancellationToken stoppingToken)
        {
            consumer.Subscribe(topic);
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var result = consumer.Consume(stoppingToken);
                    await messageHandler.HandleAsync(result.Message.Value, stoppingToken);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

		/// <summary>
		/// ����� ��� ��������� �������� ������������� ������ ���������.
		/// </summary>
		/// <param name="cancellationToken">������ ������ ������.</param>
		/// <returns></returns>
		public override Task StopAsync(CancellationToken cancellationToken)
        {
            consumer.Close();
            return base.StopAsync(cancellationToken);
        }
    }
}
