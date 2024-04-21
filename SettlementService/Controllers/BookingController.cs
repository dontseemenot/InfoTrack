using Microsoft.AspNetCore.Mvc;
using SettlementService.Models;
using SettlementService.Services;

namespace SettlementService.Controllers
{
    [ApiController]
    [Route("bookings/")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        [Route("addBooking")]

        public async Task<IActionResult> Book(BookingInput bookingInput)
        {
            return await _bookingService.Book(bookingInput);
        }
    }
}
