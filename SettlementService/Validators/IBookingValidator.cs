namespace SettlementService.Validators
{
    public interface IBookingValidator
    {
        Task<bool> ValidateBooking(string startTime, string name);
    }
}
