using NServiceBus;

namespace BowlingSys.Contracts.BookingDtos
{

    public class MakeBookingDto : IMessage
    {
        public DateTime BookingDate { get; set; }
        public string BookingTime { get; set; }
        public string BookingStatus { get; set; } 
        public int NumOfShoes { get; set; } 
        public decimal BookingCost { get; set; } 
        public int LaneId { get; set; } 
        public Guid UserId { get; set; } 
    }

}
