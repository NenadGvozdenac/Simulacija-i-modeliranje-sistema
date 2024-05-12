using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.WPF.Views.GuideViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.GuideViewModels
{
    public class TourRequestControlViewModel
    {
        public TourRequestControl tourRequestControl {  get; set; }

        public ObservableCollection<TourRequest> requests { get; set; }

        public User user { get; set; }

        public TourRequestControlViewModel(TourRequestControl _tourRequestControl, User _user)
        {
            tourRequestControl = _tourRequestControl;
            user = _user;
            requests = new ObservableCollection<TourRequest>();
            Update();
        
        }


        public void Update()
        {
            requests.Clear();
            foreach (TourRequest r in TourRequestService.GetInstance().GetAll()) 
            {
                TourRequest request_tmp = new TourRequest();
                request_tmp.Id = r.Id;
                request_tmp.UserId = r.UserId;
                request_tmp.BeginDate = r.BeginDate;
                request_tmp.EndDate = r.EndDate;   
                request_tmp.LanguageId = r.LanguageId;
                request_tmp.LocationId = r.LocationId;
                request_tmp.Language = LanguageService.GetInstance().GetById(r.LanguageId);
                request_tmp.Location = LocationService.GetInstance().GetById(r.LocationId);
                request_tmp.Description = r.Description;
                request_tmp.UserId = user.Id;
                requests.Add(request_tmp);
            }
        }





    }
}
