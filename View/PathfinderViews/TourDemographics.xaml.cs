using BookingApp.Model.MutualModels;
using BookingApp.Model.PathfinderModels;
using BookingApp.Repository.MutualRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.View.PathfinderViews
{
    /// <summary>
    /// Interaction logic for TourDemographics.xaml
    /// </summary>
    public partial class TourDemographics : Window, INotifyPropertyChanged
    {
        private int sub18;
        public int Sub18
        {
            get => sub18;
            set
            {
                if (value != sub18)
                {
                    sub18 = value;
                    OnPropertyChanged();
                }
            }
        }

        private int middle;
        public int Middle
        {
            get => middle;
            set
            {
                if (value != middle)
                {
                    middle = value;
                    OnPropertyChanged();
                }
            }
        }

        private int above50;
        public int Above50
        {
            get => above50;
            set
            {
                if (value != above50)
                {
                    above50 = value;
                    OnPropertyChanged();
                }
            }
        }

       
        public List<int> times { get; set; }
        public TourStartTimeRepository timeRepository { get; set; }

        public TouristReservationRepository reservationRepository { get; set; }

        public TouristRepository touristRepository { get; set; }

        public TourRepository tourRepository { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

 

        // The method to invoke the property changed event
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public TourDemographics()
        {
            InitializeComponent();
            DataContext = this;

            timeRepository = new TourStartTimeRepository();
            reservationRepository = new TouristReservationRepository();
            touristRepository = new TouristRepository();
            tourRepository = new TourRepository();

            Sub18 = FindSub18(FindMostReserved());
            Middle = FindMiddle(FindMostReserved());
            Above50 = FindAbove50(FindMostReserved());

            times = FindYears();

            demographicsControl.StatsButtonClickedControl += (s, e) => OnStatsButtonClicked_Handler(s,e);
            Update();
        }

        public void Update()
        {

        }


        public Tour FindMostReserved()
        {
            List<TourStartTime> dates = new List<TourStartTime>();
            List<Tour> tours = new List<Tour>();
            tours = tourRepository.GetAll();
            Tour tour = tours[0];
            int touristNumber = 0;
            int touristNumberTemp = 0;

            foreach (Tour t in tours)
            {
                dates = timeRepository.GetByTourId(t.Id).Where(a => a.Status == "passed").ToList();

                foreach (TourStartTime date in dates)
                {
                    foreach (TouristReservation reservation in reservationRepository.GetAll())
                    {
                        if (reservation.Id_TourTime == date.Id)
                        {
                            touristNumberTemp++;
                        }
                    }
                  
                }
                if (touristNumberTemp > touristNumber)
                {
                    touristNumber = touristNumberTemp;
                    tour = t;
                }
                touristNumberTemp = 0;
            }
            return tour;
        }

        public List<Tourist> GroupTourists(Tour t) {

            Tour tour = new Tour();
            tour = t;
            List<TourStartTime> dates = new List<TourStartTime>();
            dates = timeRepository.GetByTourId(t.Id).Where(a => a.Status == "passed").ToList();
            List<TouristReservation> reservations = new List<TouristReservation>();
            
            foreach(TourStartTime time in dates)
            {
                reservations.AddRange(reservationRepository.GetByTimeId(time.Id));
            }

          

            List<Tourist> tourists = new List<Tourist>();

            foreach (TouristReservation reservation in reservations) {
                Tourist tourist = new Tourist();
                tourist.Name = touristRepository.GetById(reservation.Id_Tourist).Name;
                tourist.Surname = touristRepository.GetById(reservation.Id_Tourist).Surname;
                tourist.Age = touristRepository.GetById(reservation.Id_Tourist).Age;
                tourists.Add(tourist);
            }

            return tourists;
        
        }


        public int FindSub18(Tour t){

            List<Tourist> tourists = GroupTourists(t);
            int sub18 = 0;
            foreach (Tourist tourist in tourists) {
                if(tourist.Age < 18)
                    sub18++;
            }
            return sub18;
        }

        public int FindMiddle(Tour t)
        {

            List<Tourist> tourists = GroupTourists(t);
            int middle = 0;
            foreach (Tourist tourist in tourists)
            {
                if (tourist.Age >= 18 && tourist.Age < 50)
                    middle++;
            }
            return middle;
        }

        public int FindAbove50(Tour t)
        {

            List<Tourist> tourists = GroupTourists(t);
            int above50 = 0;
            foreach (Tourist tourist in tourists)
            {
                if (tourist.Age >= 50)
                    above50++;
            }
            return above50;
        }

        public List<int> FindYears()
        {
            List<int> times = new List<int>();

            foreach (TourStartTime time in timeRepository.GetAll()) 
            {
                if(!times.Contains(time.Time.Year) && time.Status == "passed")
                    times.Add(time.Time.Year);
            }
            return times;
        }

        public void OnStatsButtonClicked_Handler(object sender, BeginButtonClickedEventArgs e)
        {
            OnStatsButtonClicked(new BeginButtonClickedEventArgs(e.TourId, e.StartTime));
         
        }

        public void OnStatsButtonClicked(BeginButtonClickedEventArgs e)
        {
            Tour t = tourRepository.GetById(e.TourId);
            Sub18 = FindSub18(t);
            
            Middle = FindMiddle(t);
            
            Above50 = FindAbove50(t);
            
           
        }

        private void AllTime_Click(object sender, RoutedEventArgs e)
        {
            Sub18 = FindSub18(FindMostReserved());
            Middle = FindMiddle(FindMostReserved());
            Above50 = FindAbove50(FindMostReserved());
        }

        private void yearSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int year = (int)YearBox.SelectedItem;

            Sub18 = FindSub18(FindMostReservedForYear(year));
            Middle = FindMiddle(FindMostReservedForYear(year));
            Above50 = FindAbove50(FindMostReservedForYear(year));
        }


        public Tour FindMostReservedForYear(int i)
        {
            List<TourStartTime> dates = new List<TourStartTime>();
            List<Tour> tours = new List<Tour>();
            tours = tourRepository.GetAll();
            Tour tour = tours[0];
            int touristNumber = 0;
            int touristNumberTemp = 0;

            foreach (Tour t in tours)
            {
                dates = timeRepository.GetByTourId(t.Id).Where(a => a.Status == "passed" && a.Time.Year == i).ToList();

                foreach (TourStartTime date in dates)
                {
                    foreach (TouristReservation reservation in reservationRepository.GetAll())
                    {
                        if (reservation.Id_TourTime == date.Id)
                        {
                            touristNumberTemp++;
                        }
                    }

                }
                if (touristNumberTemp > touristNumber)
                {
                    touristNumber = touristNumberTemp;
                    tour = t;
                }
                touristNumberTemp = 0;
            }
            return tour;
        }






    }
}
