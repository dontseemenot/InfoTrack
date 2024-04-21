using Microsoft.AspNetCore.Mvc;
using SettlementService.Models;
using SettlementService.Repository;
using SettlementService.Validators;

namespace SettlementService.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IBookingValidator _bookingValidator;

        public BookingService(IBookingRepository bookingRepository, IBookingValidator bookingValidator)
        {
            _bookingRepository = bookingRepository;
            _bookingValidator = bookingValidator;
        }

        public async Task<IActionResult> Book(BookingInput input)
        {
            // Validate booking
            if (!await _bookingValidator.ValidateBooking(input.bookingTime, input.name))
            {
                return new BadRequestObjectResult("Invalid booking request - please verify the name and booking time is between 09:00 and 16:00");
            }

            // Add booking
            var guid = await _bookingRepository.AddBooking(input.bookingTime, input.name);
            if (String.IsNullOrEmpty(guid))
            {
                return new ConflictObjectResult($"Booking time {input.bookingTime} is already reserved.");
            }
            return new OkObjectResult(new BookingResponse { bookingId = guid });
        }
    }
}
