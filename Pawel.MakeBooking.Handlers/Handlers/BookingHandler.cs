using NServiceBus;
using BowlingSys.Contracts.BookingDtos;
using BowlingSys.Services.BookingService;
using BowlingSys.Entities.BookingResultDBEntities;
using System;

namespace BowlingSys.Handlers.Handlers
{
    public class MyMessageHandler : IHandleMessages<MakeBookingDto>
    {
        private readonly BookingService _bookingService;

        public MyMessageHandler(BookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task Handle(MakeBookingDto message, IMessageHandlerContext context)
        {
            try { 

                var Result = await _bookingService.CallAddNewBooking_SP(message);

                await context.Reply(Result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while processing the message: {ex.Message}");

                throw; 
            }
        }

       


    }
}
