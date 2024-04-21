using static SettlementService.Constants.Constants;
using static SettlementService.Helpers.Conversion;

namespace SettlementService.Validators
{
    public class BookingValidator : IBookingValidator
    {
        public Task<bool> ValidateBooking(string timeString, string name)
        {
            try
            {
                var startDateTime = ConvertStringToTime(timeString);
                if (startDateTime == null) return Task.FromResult(false);

                // Check booking time is between 09:00 and 16:00
                if (startDateTime < ConvertStringToTime(BookingEarliestTime) || startDateTime > ConvertStringToTime(BookingLatestTime)) return Task.FromResult(false);
            } catch
            {
                return Task.FromResult(false);
            }

            if (string.IsNullOrWhiteSpace(name)) return Task.FromResult(false);

            return Task.FromResult(true);
        }
    }
}
