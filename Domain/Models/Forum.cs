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
    private ForumStatus open;

    public int Id { get; set; }
    public int LocationId { get; set; }
    public Location Location { get; set; }
    public ForumStatus ForumStatus { get; set; }
    public DateOnly CreationDate { get; set; }
    public int UserId { get; set; }
    public List<ForumComment> Comments { get; set; }
    public Forum()
    {
        
    }

    public Forum(int userId, int locationId, ForumStatus forumStatus)
    {
        LocationId = locationId;
        ForumStatus = forumStatus;
        UserId = userId;
        CreationDate = DateOnly.FromDateTime(DateTime.Now);
    }


    public string[] ToCSV()
    {
        return new string[] { Id.ToString(), LocationId.ToString(), ForumStatus.ToString(), CreationDate.ToString(), UserId.ToString() };
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        LocationId = int.Parse(values[1]);
        ForumStatus = Enum.Parse<ForumStatus>(values[2]);
        CreationDate = DateOnly.Parse(values[3]);
        UserId = int.Parse(values[4]);
    }
}
