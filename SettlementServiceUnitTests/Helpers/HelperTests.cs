using SettlementService.Helpers;

namespace SettlementServiceUnitTests.Helpers
{
    public class HelperTests
    {

        [Theory]
        [TestCase("09:30")]
        [TestCase("15:32")]
        public void ConvertStringToTime_OnValidInput_ReturnsSuccess(string time)
        {
            // Arrange
            var expectedResult = DateTime.ParseExact(time, "HH:mm", null);

            // Act
            var result = Conversion.ConvertStringToTime(time);

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Theory]
        [TestCase("")]
        [TestCase(":")]
        [TestCase(":10")]
        [TestCase("00:80")]
        [TestCase("000:30")]
        [TestCase("09:301")]
        [TestCase("09:")]
        [TestCase("25:01")]
        public void ConvertStringToTime_OnInvalidInput_ReturnsException(string time)
        {
            // Act
            var result = Conversion.ConvertStringToTime(time);

            // Assert
            Assert.IsNull(result);
        }
    }
}
