using BookingApp.Domain.Miscellaneous;
using BookingApp.WPF.Views.GuestViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Models
{
    public class GuideInfo : ISerializable
    {
        public int Id { get; set; }
        public int GuideId { get; set; }
        public bool IsSuperGuide { get; set; }
        public int NumberOfToursThisYear { get; set; }
        public DateOnly DateOfBecomingSuperGuide { get; set; }
        public string Language { get; set; }

        public GuideInfo()
        {
        }

        public GuideInfo(int id, int guideId, bool isSuperGuide, int numberOfToursThisYear, DateOnly dateOfBecomingSuperGuide, string language)
        {
            Id = id;
            GuideId = guideId;
            IsSuperGuide = isSuperGuide;
            NumberOfToursThisYear = numberOfToursThisYear;
            DateOfBecomingSuperGuide = dateOfBecomingSuperGuide;
            Language = language;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            GuideId = Convert.ToInt32(values[1]);
            IsSuperGuide = Convert.ToBoolean(values[2]);
            NumberOfToursThisYear = Convert.ToInt32(values[3]);
            DateOfBecomingSuperGuide = DateOnly.Parse(values[4]);
            Language = values[5];
        }

        public string[] ToCSV()
        {
            return new string[] { Id.ToString(), GuideId.ToString(), IsSuperGuide.ToString(), NumberOfToursThisYear.ToString(), DateOfBecomingSuperGuide.ToString(), Language};
        }




    }
}
