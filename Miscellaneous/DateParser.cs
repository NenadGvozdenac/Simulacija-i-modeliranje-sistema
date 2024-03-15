using System;

namespace BookingApp.Miscellaneous;

public class DateParser
{
    private static readonly string format = "dd.MM.yyyy";

    public static DateTime Parse(string date)
    {
        return DateTime.ParseExact(date, format, System.Globalization.CultureInfo.InvariantCulture);
    }

    public static string ToString(DateTime date)
    {
        return date.ToString(format);
    }
}
