using CONSUMER.Consumer;
using MassTransit;
using SHARED;

namespace CONSUMER
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMassTransit(x =>
            {
                // Incluimos um Consumer chamado UserConsumer que vai implementar a interface IConsumer<User> 
                x.AddConsumer<UserConsumer>();

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    // Configuramos a comunicação com o RabbitMQ definindo a Url e passando os parâmetros para user e password
                    cfg.Host("rabbitmq://localhost", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    // Configuramos o endpoint do Consumer para consumir a mensagem definindo o nome da fila orderUserQueue
                    cfg.ReceiveEndpoint("orderUserQueue", ep =>
                    {
                        ep.PrefetchCount = 10;
                        ep.UseMessageRetry(r => r.Interval(2, 100));
                        ep.ConfigureConsumer<UserConsumer>(provider);
                    });
                }));
            });

            // Incluimos o serviço hosted do MassTransit que inicia e para de forma automática o serviço do bus
            builder.Services.AddHostedService<Worker>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}