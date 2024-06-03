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

    public class ComplexTourPartsControlViewModel
    {
        public ComplexTourPartsControl complexTourPartsControl {  get; set; }

        public ObservableCollection<TourRequest> requests { get; set; }

        public EventHandler<BeginButtonClickedEventArgs> BeginButtonClickedControl { get; set; }

        public int id { get; set; }

        public ComplexTourPartsControlViewModel(ComplexTourPartsControl _complexTourPartsControl, int _id)
        {

            complexTourPartsControl = _complexTourPartsControl;
            requests = new ObservableCollection<TourRequest>();
            id = _id;
            Update();
            
        }

        public void Update()
        {
            foreach(TourRequest request in TourRequestService.GetInstance().GetAll()) 
            {
                if (request.ComplexRequestId == id)
                {
                    request.Language = LanguageService.GetInstance().GetById(request.LanguageId);
                    request.Location = LocationService.GetInstance().GetById(request.LocationId);
                    requests.Add(request);
                }
            
            }
        }

        internal void ComplexCard_BeginButtonClicked(object sender, BeginButtonClickedEventArgs e)
        {

            OnBeginButtonClicked(sender, e);


        }

        private void OnBeginButtonClicked(object sender, BeginButtonClickedEventArgs e)
        {
            BeginButtonClickedControl?.Invoke(this, e);
        }
    }
}
