using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IGuideInfoRepository
    {
        void Add(GuideInfo guideinfo);
        void Delete(int id);
        List<GuideInfo> GetAll();
        GuideInfo GetById(int id);
        void Update(GuideInfo guideInfo);
        GuideInfo GetByGuideId(int guideId);


    }
}
