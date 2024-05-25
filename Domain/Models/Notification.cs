using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Models;

public class Notification
{
    public string Header { get; set; }
    public string Content { get; set; }
    public string Footer { get; set; }
    public DateOnly Date { get; set; }
    public Notification(string v1, string v2, string v3, DateOnly creationDate)
    {
        Header = v1;
        Content = v2;
        Footer = v3;
        Date = creationDate;
    }

    public Notification(string v1, string v2, string v3)
    {
        Header = v1;
        Content = v2;
        Footer = v3;
        Date = DateOnly.FromDateTime(DateTime.Now);
    }
}

public class Notification<T> : Notification where T : class
{
    public T Value { get; }

    public Notification(string v1, string v2, string v3, DateOnly creationDate, T value) : base(v1, v2, v3, creationDate)
    {
        Value = value;
    }

    public Notification(string v1, string v2, string v3, T value) : base(v1, v2, v3)
    {
        Value = value;
    }
}