using SettlementService.Models;

namespace SettlementService.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private static List<Booking> _bookings = new List<Booking>();
        private readonly IGuidGenerator _guidGenerator;

        public BookingRepository(IGuidGenerator guidGenerator)
        {
            _guidGenerator = guidGenerator;
        }

        public Task<string?> AddBooking(string startTime, string name)
        {
            DateTime newBookingStartTime = (DateTime)Helpers.Conversion.ConvertStringToTime(startTime)!;    // startTime already validated, should never be null

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

            Guid guid = _guidGenerator.GenerateGuid();

            Booking booking = new Booking()
            {
                BookingId = guid,
                StartTime = newBookingStartTime,
                EndTime = newBookingEndTime,
                Name = name
            };
            _bookings.Add(booking);
            return Task.FromResult<string?>(guid.ToString());
        }

        private static bool HasExceededMaxBookings(int num)
        {
            return num >= 4;
        }
    }
}
