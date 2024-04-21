using static SettlementService.Constants.Constants;
using static SettlementService.Helpers.Helpers;

namespace SettlementService.Validators
{
    public class BookingValidator : IBookingValidator
    {
        public Task<bool> ValidateBooking(string timeString, string name)
        {
            try
            {
                var startDateTime = ConvertStringToTime(timeString);
                // Check booking time is between 09:00 and 16:00
                if (startDateTime < ConvertStringToTime(BookingEarliestTime) || startDateTime > ConvertStringToTime(BookingLatestTime)) return Task.FromResult(false);
            } catch (Exception ex)
            {
                return Task.FromResult(false);
            }

            if (string.IsNullOrWhiteSpace(name)) return Task.FromResult(false);

            return Task.FromResult(true);
        }
    }
}
