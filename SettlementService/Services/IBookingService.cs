using Microsoft.AspNetCore.Mvc;
using SettlementService.Models;

namespace SettlementService.Services
{
    public interface IBookingService
    {
        public Task<IActionResult> Book(BookingInput bookingInput);
    }
}
