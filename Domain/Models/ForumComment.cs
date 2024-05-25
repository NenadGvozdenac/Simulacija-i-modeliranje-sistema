using BookingApp.Domain.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Models;

public class ForumComment : ISerializable
{
    public int Id { get; set; }
    public int ForumId { get; set; }
    public Forum Forum { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public string Content { get; set; }
    public DateTime CreationDateTime { get; set; }

    public ForumComment()
    {
        
    }

    public string[] ToCSV()
    {
        return new string[] { Id.ToString(), ForumId.ToString(), UserId.ToString(), Content, DateParser.ToString(CreationDateTime) };
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        ForumId = int.Parse(values[1]);
        UserId = int.Parse(values[2]);
        Content = values[3];
        CreationDateTime = DateParser.Parse(values[4]);
    }
}
