using BookingApp.Domain.Miscellaneous;
using BookingApp.Resources.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Models;

public class Forum : ISerializable
{
    public int Id { get; set; }
    public int LocationId { get; set; }
    public Location Location { get; set; }
    public ForumStatus ForumStatus { get; set; }
    public DateOnly CreationDate { get; set; }
    public List<ForumComment> Comments { get; set; }
    public Forum()
    {
        
    }

    public Forum(int locationId, ForumStatus forumStatus)
    {
        LocationId = locationId;
        ForumStatus = forumStatus;
        CreationDate = DateOnly.FromDateTime(DateTime.Now);
    }

    public Forum(int locationId) : this(locationId, ForumStatus.Open)
    {
        
    }

    public string[] ToCSV()
    {
        return new string[] { Id.ToString(), LocationId.ToString(), ForumStatus.ToString(), CreationDate.ToString() };
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        LocationId = int.Parse(values[1]);
        ForumStatus = Enum.Parse<ForumStatus>(values[2]);
        CreationDate = DateOnly.Parse(values[3]);
    }
}
