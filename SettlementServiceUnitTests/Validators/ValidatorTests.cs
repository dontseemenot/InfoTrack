using SettlementService.Validators;

namespace SettlementServiceUnitTests.Validators
{
    public class ValidatorTests
    {

        private readonly IBookingValidator _bookingValidator;
        
        public ValidatorTests()
        {
            _bookingValidator = new BookingValidator();
        }

        [Theory]
        [TestCase("09:30", "John Smith")]
        [TestCase("09:00", "John Smith")]
        [TestCase("16:00", "John Smith")]
        public async Task ValidateBooking_OnValidInput_ReturnsTrue(string timeString, string name)
        {
            // Act
            var result = await _bookingValidator.ValidateBooking(timeString, name);

            // Assert
            Assert.IsTrue(result);
        }

        [Theory]

        // Time invalid
        [TestCase("", "John Smith")]
        [TestCase("123456", "John Smith")]
        [TestCase("9:30", "John Smith")]
        [TestCase("09:60", "John Smith")]
        [TestCase("24:00", "John Smith")]
        [TestCase("09:100", "John Smith")]
        [TestCase("09:0", "John Smith")]
        [TestCase("090:0", "John Smith")]

        // Name invalid
        [TestCase("9:30", "")]
        [TestCase("9:30", " ")]

        // Beyond business start/end times
        [TestCase("08:59", "John Smith")]
        [TestCase("16:01", "John Smith")]

        public async Task ValidateBooking_OnInvalidInput_ReturnsFalse(string timeString, string name)
        {
            // Act
            var result = await _bookingValidator.ValidateBooking(timeString, name);

            // Assert
            Assert.IsFalse(result);
        }
    }
}

