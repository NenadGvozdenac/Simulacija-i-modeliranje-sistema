using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.View.PathfinderViews;
using BookingApp.WPF.Views.GuideViews;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.GuideViewModels
{
    public class ComplexTourPartsWindowViewModel
    {
        public ComplexTourPartsWindow complexTourPartsWindow {  get; set; }

        public ComplexTourPartsControl complexTourPartsControl { get; set; }

        public ObservableCollection<DateTime> dates { get; set; }

        public TourRequest request { get; set; }

        public User user { get; set; }

        public EventHandler<EventArgs> disableView_Handler { get; set; }

        public ICommand confirmCommand { get; }

        public ComplexTourPartsWindowViewModel(ComplexTourPartsWindow _complexTourPartsWindow, int id, User _user)
        {
            complexTourPartsWindow = _complexTourPartsWindow;
            user = _user;
            complexTourPartsControl = new ComplexTourPartsControl(id);
            dates = new ObservableCollection<DateTime>();
            request = new TourRequest();
            confirmCommand = new RelayCommand(confirmClick);
            complexTourPartsWindow.container.Children.Add(complexTourPartsControl);
            complexTourPartsControl.complexTourPartsControlViewModel.BeginButtonClickedControl += (s, e) => AceptHandler(s, e);

        }

        private void AceptHandler(object s, BeginButtonClickedEventArgs e)
        {
            dates.Clear();

            complexTourPartsWindow.dateList.Visibility = Visibility.Visible;
            complexTourPartsWindow.confirmButton.Visibility = Visibility.Visible;
            complexTourPartsWindow.chooseBlock.Visibility = Visibility.Visible;

            request = TourRequestService.GetInstance().GetById(e.TourId);
            
            DateTime start = request.BeginDate;
            DateTime end = request.EndDate;
            int verify = 0;

            for (DateTime date = start; date <= end; date = date.AddDays(1))
            {
                foreach(TourStartTime time in TourStartTimeService.GetInstance().GetAll())
                {
                    if(time.Time.Date == date.Date)
                        verify++;
                }

                if(verify == 0)
                    dates.Add(date);

                verify = 0;

            }

            

        }
        internal void confirmClick()
        {
           
            AddTourWindow addTourWindow = new AddTourWindow(user);
            addTourWindow.addTourWindowViewModel.Country = LocationService.GetInstance().GetById(request.LocationId).Country;
            addTourWindow.addTourWindowViewModel.City = LocationService.GetInstance().GetById(request.LocationId).City;
            addTourWindow.addTourWindowViewModel.Language = LanguageService.GetInstance().GetById(request.LanguageId).Name;
            addTourWindow.addTourWindowViewModel.RequestId = request.Id;
            addTourWindow.addTourWindowViewModel.TouristId = request.UserId;

            TourStartTime startTime = new TourStartTime();
            startTime.Time = Convert.ToDateTime(complexTourPartsWindow.dateList.SelectedItem);
            startTime.Guests = 0;
            addTourWindow.addTourWindowViewModel.TourDates.Add(startTime);
            addTourWindow.dateButton.IsEnabled = false;
            request.Status = "Valid";
            request.GuideId = user.Id;
            TourRequestService.GetInstance().Update(request);
            complexTourPartsWindow.Close();
            disableView_Handler?.Invoke(this, new EventArgs());
            addTourWindow.Show();

        }
 
    
    
    
    
    
    
    
    
    }
}
