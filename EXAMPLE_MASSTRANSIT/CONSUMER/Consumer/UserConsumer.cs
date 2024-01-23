using MassTransit;
using SHARED.Models;

namespace CONSUMER.Consumer
{
    // Definimos um Consumer que implementa a interface IConsumer<T> da classe MassTransit
    public class UserConsumer : IConsumer<User>
    {
        private readonly ILogger<UserConsumer> _logger;

        public UserConsumer(ILogger<UserConsumer> logger) 
        { 
            _logger = logger;
        }

        // Qualquer mensagem com a assinatura do Modelo User que for enviada à fila orderUserQueue será recebida por este consumidor
        public async Task Consume(ConsumeContext<User> context)
        {
            await Console.Out.WriteLineAsync(context.Message.Name);

            _logger.LogInformation($"Nova mensagem recebida: {context.Message.Name} {context.Message.Age} {context.Message.DtCreated}");
        }
    }
}
