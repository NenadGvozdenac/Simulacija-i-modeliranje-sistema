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
using BookingApp.Model.MutualModels;
using BookingApp.Repository;
using BookingApp.Repository.MutualRepositories;
using BookingApp.View.GuestViews;

namespace BookingApp.View.TouristViews
{
    /// <summary>
    /// Interaction logic for TouristMainWindow.xaml
    /// </summary>
    public partial class TouristMainWindow : Window
    {
        private readonly User _user;
        public TourRepository tourRepository { get; set; }
        public Tours ToursUserControl { get; set; }


        public TouristMainWindow(User user)
        {
            InitializeComponent();
            tourRepository = new TourRepository();
            _user = user;
            Update(_user);
            ToursUserControl = new Tours(user);
            TouristWindowFrame.Content = ToursUserControl;
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            TouristWindowFrame.Content = ToursUserControl;
        }
        public void ShowTourDetails(int tourId)
        {
            Tour detailedTour = tourRepository.GetById(tourId);
            TouristWindowFrame.Content = new TouristDetails(detailedTour, _user);
        }

        private void Update(User user)
        {
            ToursUserControl = new Tours(user);

        }
    }
}
