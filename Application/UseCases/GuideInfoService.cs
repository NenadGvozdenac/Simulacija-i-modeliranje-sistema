using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model.MutualModels;
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
            GuideInfo guestInfo = new(-1, user.Id, false, 0, 0, DateOnly.MinValue, "");
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
            GuideInfo guestInfo = new(-1, user.Id, false, 0, 0,DateOnly.FromDateTime(DateTime.UtcNow), "");
            _guideInfoRepository.Add(guestInfo);
        }

        public void Delete((GuideInfo, User) entity)
        {
            _guideInfoRepository.Delete(entity.Item1.GuideId);
            _userRepository.Delete(entity.Item2.Id);
        }

        public GuideInfo GetByGuideId(int guideId)
        {
            return _guideInfoRepository.GetByGuideId(guideId);
        }

        public void FindDominantLanguage(int guideId)
        {
            List<Language> languages = LanguageService.GetInstance().GetAll();
            List<TourStartTime> dates = TourStartTimeService.GetInstance().GetAll();

            Language language = languages[0];
            int datesNumber = 0;
            int datesNumber_tmp = 0;

            foreach(Language l in languages)
            {
                foreach(TourStartTime t in dates)
                {
                    if(t.Status == "passed" && TourService.GetInstance().GetById(t.TourId).OwnerId == guideId && TourService.GetInstance().GetById(t.TourId).LanguageId == l.Id)
                    {
                        datesNumber_tmp++;
                    }
                }
                if(datesNumber_tmp > datesNumber)
                {
                    language = l;
                    datesNumber = datesNumber_tmp;
                }
                datesNumber_tmp = 0;
            }

            GuideInfo info = GetByGuideId(guideId);
            info.Language = language.Name;
            info.NumberOfToursThisYear = datesNumber;
            info.AvrageGrade = FindAvgGrade(guideId, language);
            if(FindAvgGrade(guideId, language) > 4 && datesNumber >=20)
                info.IsSuperGuide = true;
            _guideInfoRepository.Update(info);

        }

        public float FindAvgGrade(int guideId, Language language) 
        {
            List<TourReview> reviews = TourReviewService.GetInstance().GetAll();
            float avgGrade = 0;
            int numberOfReviews = 0;
            int GradeSum = 0;
            foreach(TourReview review in reviews)
            {
                if(TourService.GetInstance().GetById(review.TourId).OwnerId == guideId && TourService.GetInstance().GetById(review.TourId).LanguageId == language.Id)
                {
                    GradeSum += (review.TourInterestingness + review.GuideLanguage + review.GuideKnowledge)/3;
                    numberOfReviews++;
                }
            }
            if(GradeSum != 0 && numberOfReviews != 0)
                 avgGrade = GradeSum / numberOfReviews;
           
            return avgGrade;
        }






    }
}
