using NSubstitute;
using SettlementService.Models;
using SettlementService.Repository;

namespace SettlementServiceUnitTests.Repository
{
    public class RepositoryTests
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly IBookingRepository _bookingRepository;

        public RepositoryTests()
        {
            _guidGenerator = Substitute.For<IGuidGenerator>();
            _bookingRepository = new BookingRepository(_guidGenerator);
        }

        [Theory]
        // 4 bookings at the same time
        [TestCase("10:30", true)]
        [TestCase("10:30", true)]
        [TestCase("10:30", true)]
        [TestCase("10:30", true)]

        [TestCase("09:30", true)]
        [TestCase("11:30", true)]
        [TestCase("10:30", false)]
        [TestCase("09:31", false)]
        [TestCase("11:29", false)]

        // 2 bookings 30 minutes away from another 2 bookings
        [TestCase("13:00", true)]
        [TestCase("13:00", true)]
        [TestCase("13:30", true)]
        [TestCase("13:30", true)]

        [TestCase("12:59", false)]
        [TestCase("13:20", false)]
        [TestCase("13:30", false)]
        [TestCase("13:35", false)]
        [TestCase("13:59", false)]
        [TestCase("14:00", true)]

        public async Task AddBooking_OnSuccessOrConflict_ReturnsGuidOrNull(string time, bool isSuccess)
        {
            // Arrange
            string? expectedResult = null;
            if (isSuccess)
            {
                Guid guid = Guid.NewGuid();
                _guidGenerator.GenerateGuid().Returns(guid);
                expectedResult = guid.ToString();
            }
            
            // Act
            var result = await _bookingRepository.AddBooking(time, "John Smith");

            // Assert
            Assert.That(expectedResult, Is.EqualTo(result));
        }
    }
}
