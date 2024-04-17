using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Miscellaneous;

public class DateSpan
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int Days { get => (End - Start).Days; }

    public DateSpan() { }

    public DateSpan(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    }

    public override string ToString()
    {
        return $"{DateParser.ToString(Start)} - {DateParser.ToString(End)}";
    }

    public static DateSpan Parse(string value)
    {
        string[] parts = value.Split(" - ");
        return new DateSpan(DateParser.Parse(parts[0]), DateParser.Parse(parts[1]));
    }

    public bool Overlaps(DateSpan wantedDatespan)
    {
        return Start < wantedDatespan.End && End > wantedDatespan.Start;
    }

    public static bool IsDatespanStartOverlapping(DateSpan firstDateSpan, DateSpan secondDateSpan)
    {
        return secondDateSpan.Start >= firstDateSpan.Start && secondDateSpan.Start <= firstDateSpan.End;
    }
    public static bool IsDatespanEndOverlapping(DateSpan firstDateSpan, DateSpan secondDateSpan)
    {
        return secondDateSpan.End >= firstDateSpan.Start && secondDateSpan.End <= firstDateSpan.End;
    }

    public static bool IsDatespanInMiddle(DateSpan firstDateSpan, DateSpan secondDateSpan)
    {
        return secondDateSpan.Start <= firstDateSpan.Start && secondDateSpan.End >= firstDateSpan.End;
    }

    public static bool IsOverlapping(DateSpan firstDateSpan, DateSpan secondDateSpan)
    {
        return IsDatespanStartOverlapping(firstDateSpan, secondDateSpan) || IsDatespanEndOverlapping(firstDateSpan, secondDateSpan) || IsDatespanInMiddle(firstDateSpan, secondDateSpan);
    }
}
