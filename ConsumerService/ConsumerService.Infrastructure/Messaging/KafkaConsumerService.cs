using Confluent.Kafka;
using ConsumerService.Application.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace ConsumerService.Infrastructure.Messaging
{
    public class KafkaConsumerService : BackgroundService
    {
        private readonly IConsumer<Null, string> _consumer;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public KafkaConsumerService(ConsumerConfig config, IServiceScopeFactory serviceScopeFactory)
        {
            _consumer = new ConsumerBuilder<Null, string>(config).Build();
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.Subscribe("employee-topic");

            while (!stoppingToken.IsCancellationRequested)
            {
                var consumeResult = _consumer.Consume(stoppingToken);
                if (consumeResult != null)
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                        await mediator.Send(new ConsumeEmployeeCommand(consumeResult.Message.Value));
                    }
                }
            }
        }

        public override void Dispose()
        {
            _consumer.Close();
            _consumer.Dispose();
            base.Dispose();
        }
    }
}
