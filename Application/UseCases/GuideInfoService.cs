using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class GuideService
    {
        private IGuideInfoRepository _guideInfoRepository;
        private IUserRepository _userRepository;

        public GuideService(IGuideInfoRepository guideInfoRepository, IUserRepository userRepository)
        {
            _guideInfoRepository = guideInfoRepository;
            _userRepository = userRepository;
        }

        public static GuideService GetInstance()
        {
            return App.ServiceProvider.GetRequiredService<GuideService>();
        }

        public void UpdateGuideInfo(GuideInfo guideInfo)
        {
            _guideInfoRepository.Update(guideInfo);
        }

        public void AddGuide(User user)
        {
            GuideInfo guestInfo = new(-1, user.Id, false, 0, DateOnly.MinValue, "");
            _guideInfoRepository.Add(guestInfo);
            _userRepository.Add(user);
        }

        public void DeleteGuide(int guideId)
        {
            _guideInfoRepository.Delete(guideId);
            _userRepository.Delete(guideId);
        }

        public void UpdateGuide(GuideInfo guideInfo, User user)
        {
            _guideInfoRepository.Update(guideInfo);
            _userRepository.Update(user);
        }

        public void Add((GuideInfo, User) entity)
        {
            _guideInfoRepository.Add(entity.Item1);
            _userRepository.Add(entity.Item2);
        }

        public void Add(User user)
        {
            GuideInfo guestInfo = new(-1, user.Id, false, 0, DateOnly.FromDateTime(DateTime.UtcNow), "");
            _guideInfoRepository.Add(guestInfo);
        }

        public void Delete((GuideInfo, User) entity)
        {
            _guideInfoRepository.Delete(entity.Item1.GuideId);
            _userRepository.Delete(entity.Item2.Id);
        }

        public GuideInfo GetByGuideId(int guestId)
        {
            return _guideInfoRepository.GetByGuideId(guestId);
        }


    }
}
