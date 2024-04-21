using System.Globalization;

namespace SettlementService.Helpers
{
    public class Conversion
    {
        public static DateTime? ConvertStringToTime(string timeString)
        {
            DateTime startDateTime;
            DateTime.TryParseExact(timeString, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDateTime);
            if (startDateTime == DateTime.MinValue) return null;
            return startDateTime;
        }
    }
}
