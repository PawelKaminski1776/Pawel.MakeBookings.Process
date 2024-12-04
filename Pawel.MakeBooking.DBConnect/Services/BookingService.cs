using BowlingSys.DBConnect;
using System.Data.SqlClient;
using System.Data;
using Npgsql;
using AutoMapper;
using System;
using BowlingSys.Contracts.BookingDtos;
using System.Text.Json;
using BowlingSys.Entities.BookingResultDBEntities;

namespace BowlingSys.Services.BookingService
{
    public class BookingService
    {
        private DBConnect.DBConnect _DBConnect;

        public BookingService( DBConnect.DBConnect dBConnect)
        {
            _DBConnect = dBConnect;
        }



        public async Task<BookingResultMessage> CallAddNewBooking_SP(MakeBookingDto message)
        {
            try
            {
                var parameters = new[]
                {
                    new NpgsqlParameter("p_booking_date", NpgsqlTypes.NpgsqlDbType.Date) { Value = message.BookingDate },
                    new NpgsqlParameter("p_booking_time", NpgsqlTypes.NpgsqlDbType.Char) { Value = message.BookingTime },
                    new NpgsqlParameter("p_booking_status", NpgsqlTypes.NpgsqlDbType.Char) { Value = message.BookingStatus },
                    new NpgsqlParameter("p_numofshoes", NpgsqlTypes.NpgsqlDbType.Numeric) { Value = message.NumOfShoes },
                    new NpgsqlParameter("p_booking_cost", NpgsqlTypes.NpgsqlDbType.Numeric) { Value = message.BookingCost },
                    new NpgsqlParameter("p_lane_id", NpgsqlTypes.NpgsqlDbType.Numeric) { Value = message.LaneId },
                    new NpgsqlParameter("p_user_id", NpgsqlTypes.NpgsqlDbType.Uuid) { Value = message.UserId }
                };

                string storedProcedure = "dbo.insert_booking";

                await _DBConnect.ExecuteInsertStoredProcedure(storedProcedure, parameters);

                return new BookingResultMessage
                {
                    message = "Successfully Added Booking"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while processing the message: {ex.Message}");
                return new BookingResultMessage
                {
                    message = "unsuccessful: " + ex.Message
                };
            }
        }

    }
}
