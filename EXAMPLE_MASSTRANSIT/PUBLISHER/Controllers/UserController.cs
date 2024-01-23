using MassTransit;
using Microsoft.AspNetCore.Mvc;
using SHARED.Models;

namespace PUBLISHER.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // Usamos a interface IBus configurada no arquivo Program e injetamos o objeto IBus na controller
        private readonly IBus _bus;

        public UserController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            if(user != null)
            {
                user.DtCreated = DateTime.Now;

                // Definimos o nome da fila como orderUserQueue e criamos uma nova url
                Uri uri = new Uri("rabbitmq://localhost/orderUserQueue");

                // Obtém um endpoint para o qual vamos enviar o objeto User (Shared.Models)
                var endpoint = await _bus.GetSendEndpoint(uri);

                // Envia a mensagem para a fila do RabbitMQ
                await endpoint.Send(user);

                return Ok();
            }

            return BadRequest();
        }
    }
}
