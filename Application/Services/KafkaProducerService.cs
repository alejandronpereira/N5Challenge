using Application.Dto;
using Application.Interfaces;
using Confluent.Kafka;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class KafkaProducerService : IKafkaProducerService
    {
        private readonly ProducerConfig _producerConfig;

        public KafkaProducerService()
        {
            _producerConfig = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };
        }    

        public async Task ProduceMessageAsync(string topic, string operation)
        {
            using var producer = new ProducerBuilder<Null, string>(_producerConfig).Build();

            var kafkaMessageDto = new KafkaMessageDto
            {
                Id = Guid.NewGuid(),
                NameOperation = operation
            };

            try
            {
                var serializedMessage = JsonConvert.SerializeObject(kafkaMessageDto);
                var message = new Message<Null, string> { Value = serializedMessage };
                var deliveryReport = await producer.ProduceAsync(topic, message);
                Console.WriteLine($"Message sent: {deliveryReport.Topic}, Partition: {deliveryReport.Partition}, Offset: {deliveryReport.Offset}");
            }
            catch (ProduceException<Null, string> e)
            {
                Console.WriteLine($"Delivery failed: {e.Error.Reason}");
            }
        }
    }
}

