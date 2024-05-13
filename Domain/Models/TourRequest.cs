using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Miscellaneous;


namespace BookingApp.Domain.Models
{
    public class TourRequest : ISerializable
    {
        public int Id {  get; set; }
        public int UserId { get; set; }
        public Location Location { get; set; }
        public int LocationId { get; set; }
        public string Description {  get; set; }
        public Language Language { get; set; }
        public int LanguageId {  get; set; }
        public List<Tourist> Tourists { get; set; }
        public DateTime BeginDate {  get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RequestDate {  get; set; }
        public string Status {  get; set; }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            UserId = Convert.ToInt32(values[1]);
            LocationId = Convert.ToInt32(values[2]);
            Description = values[3];
            LanguageId = Convert.ToInt32(values[4]);
            BeginDate = Convert.ToDateTime(values[5]);
            EndDate = Convert.ToDateTime(values[6]);
            RequestDate = Convert.ToDateTime(values[7]);
            Status = values[8];
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), UserId.ToString(), LocationId.ToString(), Description, LanguageId.ToString(), BeginDate.ToString(), EndDate.ToString(), RequestDate.ToString(), Status };
            return csvValues;
        }
    }
}
