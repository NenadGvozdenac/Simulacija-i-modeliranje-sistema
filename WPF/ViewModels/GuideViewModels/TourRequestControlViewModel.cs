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
                if (r.Status == "Pending") {
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
                    request_tmp.TouristNumber = RequestedTouristService.GetInstance().GetAll().Where(t => t.RequestId == request_tmp.Id).ToList().Count();
                    requests.Add(request_tmp);
                }
            }
        }

        public void SearchByLocation(string city)
        {
            if (city != null)
            {
                List<TourRequest> removed_requests = new List<TourRequest>();
                removed_requests = requests.Where(r => r.Location.City !=  city).ToList();
                foreach(TourRequest request in removed_requests)
                    requests.Remove(request);
                
            }
        }

        public void SearchByLanguage(string language) 
        {
            if (language != null)
            {
                List<TourRequest> removed_requests = new List<TourRequest>();
                removed_requests = requests.Where(r => r.Language.Name != language).ToList();
                foreach (TourRequest request in removed_requests)
                    requests.Remove(request);
            }
        }

        public void SearchByCapacity(int capacity)
        {
            if(capacity > 0)
            {
                List<TourRequest> removed_requests = new List<TourRequest>();
                removed_requests = requests.Where(r => r.TouristNumber != capacity).ToList();
                foreach (TourRequest request in removed_requests)
                    requests.Remove(request);


            }
        }

        public void SearchByStartDate(DateTime date)
        {
            if (date != new DateTime())
            {
                List<TourRequest> removed_requests = new List<TourRequest>();
                removed_requests = requests.Where(r => r.BeginDate < date).ToList();
                foreach (TourRequest request in removed_requests)
                    requests.Remove(request);
            }
        }

        public void SearchByEndDate(DateTime date) 
        {
            if (date != new DateTime())
            {
                List<TourRequest> removed_requests = new List<TourRequest>();
                removed_requests = requests.Where(r => r.BeginDate > date).ToList();
                foreach (TourRequest request in removed_requests)
                    requests.Remove(request);
            }
        }

        public void SearchByCriterias(string city, string language,int capacity ,DateTime date1, DateTime date2)
        {
            Update();
            SearchByLocation(city);
            SearchByLanguage(language);
            SearchByCapacity(capacity);
            SearchByStartDate(date1);
            SearchByEndDate(date2);
        }

        public void Request_AcceptClick(BeginButtonClickedEventArgs e)
        {
            foreach(TourRequest request in requests)
            {
                if(request.Id == e.TourId)
                {
                    requests.Remove(request);
                    request.Status = "Valid";
                    TourRequestService.GetInstance().Update(request);
                    break;
                }
            }
        }
    
    
    
    
    
    
    
    }
}
