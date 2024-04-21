using System.Globalization;

namespace SettlementService.Helpers
{
    public class Helpers
    {
        public static DateTime ConvertStringToTime(string timeString)
        {
            try
            {
                DateTime startDateTime;
                DateTime.TryParseExact(timeString, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDateTime);
                return startDateTime;
            }
            catch (Exception ex)
            {
                throw new Exception("Input time does not follow format HH:mm", ex);
            }
        }
    }
}
