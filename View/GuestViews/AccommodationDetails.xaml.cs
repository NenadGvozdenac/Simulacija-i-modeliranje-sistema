using BookingApp.Model.MutualModels;
using BookingApp.Repository;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.View.GuestViews
{
    /// <summary>
    /// Interaction logic for AccommodationDetails.xaml
    /// </summary>
    public partial class AccommodationDetails : UserControl
    {
        int minvalueDaysOfStay = -1,
        maxvalueDaysOfStay = 100,
        startvalueDaysOfStay = 1;

        public Accommodation selectedAccommodation { get; set; }
        public AccommodationReservationRepository accomodationreservationrepository { get; set; }

        public AccommodationDetails()
        {
            InitializeComponent();
            accomodationreservationrepository = new AccommodationReservationRepository();
        }

        public void SetAccommodation(Accommodation accommodation)
        {
            selectedAccommodation = accommodation;
            firstDate.Text = string.Empty;
            lastDate.Text = string.Empty;
            lastDate.IsEnabled = false;
            freedatescheck.IsEnabled = false;
            minvalueDaysOfStay = accommodation.MinReservationDays;
            DaysOfStay.Text = accommodation.MinReservationDays.ToString();           
            MinDaysofStayTextBlock.Text = "Choose how long would you like to stay here (minimum of " + accommodation.MinReservationDays + " days):";          
            
                       
            accommodationName.Text = accommodation.Name;
            accomodationAverageReviewScore.Text = accommodation.AverageReviewScore.ToString() + "/10";
        }
       
        private void FirstFreeDatesCheck_Click(object sender, RoutedEventArgs e)
        {
            List<DateTime> takenDates = new List<DateTime>();
            takenDates = accomodationreservationrepository.FindTakenDates(selectedAccommodation.Id);

            DateTime? tempDate = firstDate.SelectedDate;
            int freeDaysInRowCounter = 0;
            DateTime? firstAvailableDate = null;
            DateTime? lastAvailableDate = null;
            while (tempDate != lastDate.SelectedDate.Value.AddDays(1))
            {
                if (freeDaysInRowCounter == 0) 
                {
                    firstAvailableDate = (DateTime)tempDate;
                }

                if (takenDates.Contains((DateTime)tempDate))
                {
                    freeDaysInRowCounter = 0;
                }
                else
                {
                    freeDaysInRowCounter++;
                }

                //PROVERI KAKO SU ZAMISLILI OVO SA DATUMIMA
                //if (freeDaysInRowCounter == Convert.ToInt32(DaysOfStay.Text) && !takenDates.Contains((DateTime)tempDate.Value.AddDays(1)) && tempDate != lastDate.SelectedDate)
                //{
                //    lastAvailableDate = tempDate.Value.AddDays(1);
                //    break;
                //}
                if (freeDaysInRowCounter == Convert.ToInt32(DaysOfStay.Text))
                {
                    lastAvailableDate = tempDate;
                    break;
                }
                tempDate = tempDate.Value.AddDays(1);

            }

            if (firstAvailableDate.HasValue && lastAvailableDate.HasValue)
            {
                SetActiveUserControl(firstAvailableDate, lastAvailableDate);
            }
        }

        public void SetActiveUserControl(DateTime? firstDate, DateTime? lastDate)
        {


            freedates.first.Text = DateOnly.FromDateTime((DateTime)firstDate).ToString();
            freedates.last.Text = DateOnly.FromDateTime((DateTime)lastDate).ToString();

            freedates.Visibility = Visibility.Visible;
        } 
        
        //Design Functions
        private void DatePickerCantWrite(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void FirstDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? endDate = firstDate.SelectedDate;

            if (endDate.HasValue)
            {
                lastDate.DisplayDateStart = endDate.Value.AddDays(1);
                lastDate.IsEnabled = true;
            }
        }

        private void LastDateChanged(object sender, SelectionChangedEventArgs e)
        {
            freedatescheck.IsEnabled = true;
        }

        private void DaysOfStayUp_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (DaysOfStay.Text != "") number = Convert.ToInt32(DaysOfStay.Text);
            else number = 0;
            if (number < maxvalueDaysOfStay)
                DaysOfStay.Text = Convert.ToString(number + 1);
        }

        private void DaysOfStayDown_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (DaysOfStay.Text != "") number = Convert.ToInt32(DaysOfStay.Text);
            else number = 0;
            if (number > minvalueDaysOfStay)
                DaysOfStay.Text = Convert.ToString(number - 1);
        }

        private void DaysOfStay_TextChanged(object sender, TextChangedEventArgs e)
        {
            int number = 0;
            if (DaysOfStay.Text != "")
                if (!int.TryParse(DaysOfStay.Text, out number)) DaysOfStay.Text = startvalueDaysOfStay.ToString();
            if (number > maxvalueDaysOfStay) DaysOfStay.Text = maxvalueDaysOfStay.ToString();
            if (number < minvalueDaysOfStay) DaysOfStay.Text = minvalueDaysOfStay.ToString();
            DaysOfStay.SelectionStart = DaysOfStay.Text.Length;
        }
    }
}
