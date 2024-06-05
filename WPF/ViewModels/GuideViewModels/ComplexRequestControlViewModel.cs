using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.WPF.Views.GuideViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.GuideViewModels
{
    public class ComplexRequestControlViewModel
    {

        public ObservableCollection<ComplexTourRequest> complexRequests {  get; set; }
        public ConplexRequestsControl complexRequestControl {  get; set; }

        public User user { get; set; }

        public ComplexRequestControlViewModel(ConplexRequestsControl _complexRequestControl, User _user)
        {
            complexRequestControl = _complexRequestControl;
            user = _user;
            complexRequests = new ObservableCollection<ComplexTourRequest>();
            Update();
        }

        public void Update()
        {
            int test = 0;

            foreach(ComplexTourRequest request in ComplexTourRequestService.GetInstance().GetAll())
            {
                request.tourist = UserService.GetInstance().GetById(request.UserId).Username;
                request.guideId = user.Id;

                int counter = 0;
                foreach(TourRequest r in TourRequestService.GetInstance().GetAll())
                    {
                        if(r.ComplexRequestId == request.Id)
                        counter++;

                    if (r.GuideId == user.Id)
                        test++;
                    }

                request.numberOfParts = counter;

                if(test == 0)
                 complexRequests.Add(request);

                test = 0;
            }
        }








    }
}
