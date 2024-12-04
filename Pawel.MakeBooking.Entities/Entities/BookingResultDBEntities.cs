using NServiceBus;
namespace BowlingSys.Entities.BookingResultDBEntities
{
    public class BookingResultMessage : IMessage
    {
        public string message { get; set; }
    }
}
