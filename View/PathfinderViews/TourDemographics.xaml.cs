using BookingApp.Model.MutualModels;
using BookingApp.Model.PathfinderModels;
using BookingApp.Repository.MutualRepositories;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace BookingApp.View.PathfinderViews
{
    /// <summary>
    /// Interaction logic for TourDemographics.xaml
    /// </summary>
    public partial class TourDemographics : Window
    {
        public int Sub18 {  get; set; }

        public int Middle {  get; set; }

        public int Above50 { get; set; }

        public TourStartTimeRepository timeRepository { get; set; }

        public TouristReservationRepository reservationRepository { get; set; }

        public TouristRepository touristRepository { get; set; }
        public TourDemographics()
        {
            InitializeComponent();
            DataContext = this;

            timeRepository = new TourStartTimeRepository();
            reservationRepository = new TouristReservationRepository();
            touristRepository = new TouristRepository();

            Sub18 = FindSub18(FindMostReserved());
            Middle = FindMiddle(FindMostReserved());
            Above50 = FindAbove50(FindMostReserved());
            Update();
        }

        public void Update()
        {

        }


        public TourStartTime FindMostReserved()
        {
            List<TourStartTime> dates = new List<TourStartTime>();
            dates = timeRepository.GetAll().Where(a => a.Status == "passed").ToList();
            TourStartTime time = dates[0];
            int touristNumber = 0;
            int touristNumberTemp = 0;

            foreach (TourStartTime date in dates)
            {
                foreach (TouristReservation reservation in reservationRepository.GetAll()){
                        if(reservation.Id_TourTime == date.Id)
                            {
                                touristNumberTemp++;
                            }
                        }
                if (touristNumberTemp > touristNumber) {
                    touristNumber = touristNumberTemp;
                    time = date;
                }
                touristNumberTemp = 0;
            }
            return time;
        }

        public List<Tourist> GroupTourists(TourStartTime date) {

            TourStartTime time = new TourStartTime();
            time = date;

            List<TouristReservation> reservations = reservationRepository.GetByTimeId(time.Id);

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


        public int FindSub18(TourStartTime time){

            List<Tourist> tourists = GroupTourists(time);
            int sub18 = 0;
            foreach (Tourist tourist in tourists) {
                if(tourist.Age < 18)
                    sub18++;
            }
            return sub18;
        }

        public int FindMiddle(TourStartTime time)
        {

            List<Tourist> tourists = GroupTourists(time);
            int middle = 0;
            foreach (Tourist tourist in tourists)
            {
                if (tourist.Age >= 18 && tourist.Age < 50)
                    middle++;
            }
            return middle;
        }

        public int FindAbove50(TourStartTime time)
        {

            List<Tourist> tourists = GroupTourists(time);
            int above50 = 0;
            foreach (Tourist tourist in tourists)
            {
                if (tourist.Age >= 50)
                    above50++;
            }
            return above50;
        }





    }
}
