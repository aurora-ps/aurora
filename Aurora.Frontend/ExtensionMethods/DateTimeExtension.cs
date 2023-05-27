using System.Reflection;

namespace Aurora.Frontend.ExtensionMethods
{
    public static class DateTimeExtension
    {
        public static string FormattedDateTime(this DateTime? dateTime)
        {
            if (dateTime == null)
            {
                return string.Empty;
            }
            return $"{dateTime.Value:MM/dd/yyyy} {dateTime.Value.Hour:00}:{dateTime.Value.Minute:00}";
        }
    }
}
