using System;
using System.Text.RegularExpressions;

namespace BookingApp.Miscellaneous;

public class DateParser
{
    private static readonly string format = "dd.MM.yyyy.";

    public static DateTime Parse(string date)
    {
        Regex regex = new Regex(@"^(\d{1,2})\.(\d{1,2})\.(\d{4})\.$");

        Match match = regex.Match(date);

        int days = int.Parse(match.Groups[1].Value);
        int months = int.Parse(match.Groups[2].Value);
        int years = int.Parse(match.Groups[3].Value);

        return new DateTime(years, months, days);
    }

    public static string ToString(DateTime date)
    {
        return date.ToString(format);
    }
}