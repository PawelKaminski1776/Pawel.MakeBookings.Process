using Microsoft.AspNetCore.Mvc;
using NServiceBus;
using BowlingSys.Contracts.BookingDtos;
using BowlingSys.Entities.BookingResultDBEntities;
using System.Threading.Tasks;

namespace BowlingSys.Process.Controllers
{
    [ApiController]
    [Route("Api/MakeBooking")]
    public class BowlingSysController : BaseController
    {
        public BowlingSysController(IMessageSession messageSession) : base(messageSession)
        {
        }


        [HttpPost("SendBooking")]
        public async Task<IActionResult> AddBooking([FromBody] MakeBookingDto dto)
        {
            
            try
            {
                var response = await _messageSession.Request<BookingResultMessage>(dto);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error while processing the request: {ex.Message}");
            }
        }
    }
}
