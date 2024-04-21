using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using SettlementService.Controllers;
using SettlementService.Models;
using SettlementService.Services;

namespace SettlementServiceUnitTests.Controller
{
    public class ControllerTests
    {

        private readonly BookingController _bookingController;
        private readonly IBookingService _bookingService;

        public ControllerTests()
        {
            _bookingService = Substitute.For<IBookingService>();
            _bookingController = new BookingController(_bookingService);
        }

        [Test]
        public async Task Book_OnValidInput_ReturnsOk()
        {
            // Arrange
            BookingInput bookingInput = new BookingInput { bookingTime = "9:30", name = "John Smith" };
            _bookingService.Book(Arg.Any<BookingInput>()).Returns(new OkObjectResult("Ok"));

            // Act
            var result = await _bookingController.Book(bookingInput);

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task Book_On_ValidInput_ReturnsConflict()
        {
            // Arrange
            BookingInput bookingInput = new BookingInput { bookingTime = "9:30", name = "John Smith" };
            _bookingService.Book(Arg.Any<BookingInput>()).Returns(new ConflictObjectResult("Conflict"));

            // Act
            var result = await _bookingController.Book(bookingInput);

            // Assert
            Assert.That(result, Is.TypeOf<ConflictObjectResult>());
        }

        [Test]
        public async Task Book_OnInvalidInput_ReturnsBadRequest()
        {
            // Arrange
            BookingInput bookingInput = new BookingInput { bookingTime = "", name = "" };
            _bookingService.Book(Arg.Any<BookingInput>()).Returns(new BadRequestObjectResult("BadRequest"));

            // Act
            var result = await _bookingController.Book(bookingInput);

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        //[Theory]
        //[TestCase(null, null)]
        //[TestCase("", "")]
        //[TestCase(null, "")]
        //[TestCase(null, "John Smith")]
        //[TestCase("9:30", null)]
        //[TestCase("9:30", "")]
        //public async Task Book_OnInValidInput_ReturnsBadRequest(string? bookingTime, string? name)
        //{
        //    // Arrange
        //    BookingInput bookingInput = new BookingInput { bookingTime = bookingTime, name = name };
        //    _bookingService.Book(Arg.Any<BookingInput>()).Returns(new OkResult());

        //    // Act
        //    var result = await _bookingController.Book(bookingInput);

        //    // Assert
        //    Assert.That(result, Is.TypeOf<BadRequestResult>());
        //}
    }
}
