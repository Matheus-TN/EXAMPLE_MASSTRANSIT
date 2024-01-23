using MassTransit;
using SHARED;

namespace PUBLISHER
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Inclui o serviço do MassTransit no contêiner da ASP.NET Core
            builder.Services.AddMassTransit(x =>
            {
                // Cria um novo service bus usando o RabbitMQ e definindo a conexão, informando os parametros de usuário e senha
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });

            // Inclui o serviço de hosted do MassTransit que inicia e para de forma automática o serviço do bus
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