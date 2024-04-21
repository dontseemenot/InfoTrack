using SettlementService.Models;

namespace SettlementService.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private static List<Booking> _bookings = new List<Booking>();

        public Task<string?> AddBooking(string startTime, string name)
        {
            DateTime newBookingStartTime = Helpers.Helpers.ConvertStringToTime(startTime);
            DateTime newBookingEndTime = newBookingStartTime.AddMinutes(59);

            int numSimultaneousBookings = 0;
            foreach (var existingBooking in _bookings)
            {
                // Check if booking overlaps with any existing bookings
                if (
                    (existingBooking.StartTime <= newBookingStartTime && newBookingStartTime <= existingBooking.EndTime)
                    || (existingBooking.StartTime <= newBookingEndTime && newBookingEndTime <= existingBooking.EndTime))
                {
                    numSimultaneousBookings += 1;
                    if (HasExceededMaxBookings(numSimultaneousBookings)) return Task.FromResult<string?>(null);
                }
            }
            if (HasExceededMaxBookings(numSimultaneousBookings)) return Task.FromResult<string?>(null);

            Booking booking = new Booking()
            {
                BookingId = Guid.NewGuid(),
                StartTime = newBookingStartTime,
                EndTime = newBookingEndTime,
                Name = name
            };
            _bookings.Add(booking);
            return Task.FromResult<string?>(booking.BookingId.ToString());
        }

        private static bool HasExceededMaxBookings(int num)
        {
            return num >= 4;
        }
    }
}
