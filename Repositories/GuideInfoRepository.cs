using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repositories
{
    public class GuideInfoRepository : BaseRepository<GuideInfo>, IGuideInfoRepository
    {
        public GuideInfoRepository() : base("../../../Resources/Data/guide_infos.csv", "Id") { }

        public GuideInfo GetByGuideId(int guideId)
        {
            return GetAll().Find(guideInfo => guideInfo.GuideId == guideId);
        }

    }
}
