namespace SettlementService.Repository
{
    public interface IBookingRepository
    {
        Task<string?> AddBooking(string startTime, string name);
    }
}
