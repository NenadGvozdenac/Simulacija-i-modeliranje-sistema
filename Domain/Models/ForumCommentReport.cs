using BookingApp.Domain.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Models;

public class ForumCommentReport : ISerializable
{
    public int Id { get; set; }
    public int ForumCommentId { get; set; }
    public ForumComment ForumComment { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }

    public ForumCommentReport()
    {
        
    }
    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        ForumCommentId = int.Parse(values[1]);
        UserId = int.Parse(values[2]);
    }

    public string[] ToCSV()
    {
        return new string[] { Id.ToString(), ForumCommentId.ToString(), UserId.ToString() };
    }
}
