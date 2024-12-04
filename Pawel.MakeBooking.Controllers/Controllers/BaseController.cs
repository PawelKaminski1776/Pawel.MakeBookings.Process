using Microsoft.AspNetCore.Mvc;
using NServiceBus;
using System.Threading.Tasks;

namespace BowlingSys.Process.Controllers
{
    public class BaseController : ControllerBase
    {
        public readonly IMessageSession _messageSession;

        public BaseController(IMessageSession messageSession)
        {
            _messageSession = messageSession;
        }

        protected async Task<IActionResult> HandleMessage<TMessage>(TMessage message)
        {
            try
            {
                await _messageSession.Send("NServiceBusHandlers", message);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error while processing the message.");
            }
        }
    }
}
