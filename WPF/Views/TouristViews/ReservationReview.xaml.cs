using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.Views.TouristViews
{
    /// <summary>
    /// Interaction logic for ReservationReview.xaml
    /// </summary>
    public partial class ReservationReview : UserControl
    {
        private Tour selectedTour { get; set; }
        private int guestNumber { get; set; }
        public List<TourStartTime> tourStartTimes { get; set; }
        public TourStartTime selectedTourStartTime { get; set; }
        public List<Tourist> tourists { get; set; }
        public User user { get; set; }
        public TourVoucher tourVoucher { get; set; }
        public ObservableCollection<Tourist> _tourists { get; set; }

        public ReservationReview(User user, Tour detailedTour, int guestNumber, List<Tourist> tourists, TourVoucher voucher, TourStartTime tourStartTime)
        {
            InitializeComponent();
            DataContext = this;
            this.user = user;
            selectedTour = detailedTour;
            tourStartTimes = new List<TourStartTime>();
            selectedTourStartTime = tourStartTime;
            this.guestNumber = guestNumber;
            this.tourists = tourists;
            tourVoucher = voucher;
            _tourists = new ObservableCollection<Tourist>();

            TourText.Text = selectedTour.Name;
            LocationText.Text = LocationService.GetInstance().GetById(selectedTour.LocationId).Country.ToString();
            LanguageText.Text = LanguageService.GetInstance().GetById(selectedTour.LanguageId).ToString();
            DateText.Text = selectedTourStartTime.Time.ToString();
            FindTourists();

        }

        public void FindTourists()
        {
            foreach(Tourist tourist in tourists)
            {
                _tourists.Add(new Tourist { Name = tourist.Name, Surname = tourist.Surname, Age = tourist.Age });
            }
        }
        public void Confirm_Click(object sender, RoutedEventArgs e)
        {
            List<Tourist> touristList = new List<Tourist>();
            foreach (Tourist t in tourists)
            {
                touristList.Add(t);
            }
            foreach (Tourist tourist in touristList)
            {
                tourist.Id = TouristService.GetInstance().NextId();
                AddTourist(tourist);

                CreateReservation(tourist, selectedTourStartTime);

                UpdateTourStartTime(selectedTourStartTime);
            }
            MessageBox.Show("Tour succesfully reserved!");
        }

        public void UpdateTourStartTime(TourStartTime tourStartTime)
        {
            tourStartTime.Guests += tourists.Count();
            TourStartTimeService.GetInstance().Update(tourStartTime);
            tourists.Clear();
        }
        public void CreateReservation(Tourist tourist, TourStartTime tourStartTime)
        {
            TouristReservation touristReservation = new TouristReservation();
            touristReservation.Id = TourReservationService.GetInstance().NextId();
            touristReservation.Id_Tourist = tourist.Id;
            touristReservation.Id_TourTime = tourStartTime.Id;
            touristReservation.CheckpointId = -1;
            touristReservation.UserId = user.Id;
            TourReservationService.GetInstance().Add(touristReservation);
            if (tourVoucher != null)
            {
                UseVoucher();
            }
        }

        public void UseVoucher()
        {
            TourVoucherService.GetInstance().Delete(tourVoucher.Id);
        }
        public void AddTourist(Tourist tourist)
        {
            foreach (Tourist t in TouristService.GetInstance().GetAll())
            {
                if (t.Id == tourist.Id)
                {
                    return;
                }
            }
            TouristService.GetInstance().Add(tourist);
        }

    }
}
