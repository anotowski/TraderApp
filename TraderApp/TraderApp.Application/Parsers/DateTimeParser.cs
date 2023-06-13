using System.Globalization;

namespace TraderApp.Application.Parsers;

public class DateTimeParser : IDateTimeParser
{
    public const string DateTimeFormat = "yyyy-MM-dd-HH-mm-ss";

    public DateTime Parse(string date)
    {
        if (string.IsNullOrWhiteSpace(date))
        {
            throw new ArgumentNullException(nameof(date));
        }
        
        if (!DateTime.TryParseExact(date, DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var parsedDate))
        {
            throw new ArgumentException(
                $"Incorrect format for: '{nameof(date)}': '{date}'.",
                nameof(date));
        }

        return parsedDate;
    }
}
