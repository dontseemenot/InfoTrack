namespace SettlementService.Models
{
    public class Booking
    {
        public Guid BookingId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public required string Name { get; set; }
    }
}


