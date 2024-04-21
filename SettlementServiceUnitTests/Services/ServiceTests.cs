using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using SettlementService.Controllers;
using SettlementService.Models;
using SettlementService.Repository;
using SettlementService.Services;
using SettlementService.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementServiceUnitTests.Services
{
    public class ServiceTests
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IBookingValidator _bookingValidator;
        private readonly BookingService _bookingService;

        public ServiceTests()
        {
            _bookingRepository = Substitute.For<IBookingRepository>();
            _bookingValidator = Substitute.For<IBookingValidator>();
            _bookingService = new BookingService(_bookingRepository, _bookingValidator);
        }

        
        [Test]
        public async Task Book_OnValidInput_ReturnsOk()
        {
            // Arrange
            BookingInput input = new BookingInput { bookingTime = "9:30", name = "John Smith" };
            _bookingValidator.ValidateBooking(input.bookingTime, input.name).Returns(true);
            _bookingRepository.AddBooking(input.bookingTime, input.name).Returns("d90f8c55-90a5-4537-a99d-c68242a6012b");

            // Act
            var result = await _bookingService.Book(input);

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task Book_OnInvalidInput_ReturnsBadRequest()
        {
            // Arrange
            BookingInput input = new BookingInput { bookingTime = "123456", name = "" };
            _bookingValidator.ValidateBooking(input.bookingTime, input.name).Returns(false);

            // Act
            var result = await _bookingService.Book(input);

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task Book_OnConflict_ReturnsConflict()
        {
            // Arrange
            BookingInput input = new BookingInput { bookingTime = "9:30", name = "John Smith" };
            _bookingValidator.ValidateBooking(input.bookingTime, input.name).Returns(true);
            _bookingRepository.AddBooking(input.bookingTime, input.name).ReturnsNull();

            // Act
            var result = await _bookingService.Book(input);

            // Assert
            Assert.That(result, Is.TypeOf<ConflictObjectResult>());
        }
    }
}
