using System;
using System.Collections;
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
using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.View.PathfinderViews;

namespace BookingApp.View.TouristViews
{
    /// <summary>
    /// Interaction logic for TourDates.xaml
    /// </summary>
    public partial class TourDates : Window
    {
        public Tour selectedTour {  get; set; }
        public TourStartTimeRepository tourStartTimeRepository { get; set; }
        public List<TourStartTime> tourStartTimes { get; set; }
        public TourDates(Tour detailedTour) 
        {
            InitializeComponent();
            selectedTour = detailedTour;
            tourStartTimeRepository = new TourStartTimeRepository();
            tourStartTimes = new List<TourStartTime>();

            // Popunite listu tourStartTimes
            findTourDates();

            // Postavite listu kao izvor podataka za DataGrid
            tourDatesDataGrid.ItemsSource = tourStartTimes;
        }
        public void findTourDates()
        {
            foreach(TourStartTime tourStartTime in tourStartTimeRepository.GetAll())
            {
                if (tourStartTime.TourId == selectedTour.Id)
                {
                    tourStartTimes.Add(tourStartTime);
                }
            }
        }



    }
}
