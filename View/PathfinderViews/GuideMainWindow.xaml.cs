using BookingApp.Model.MutualModels;
using BookingApp.Repository;
using BookingApp.Repository.MutualRepositories;
using BookingApp.View.GuestViews;
using BookingApp.View.OwnerViews.Components;
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
    /// Interaction logic for GuideMainWindow.xaml
    /// </summary>
    public partial class GuideMainWindow : Window
    {
        private readonly User _user;
        private TourRepository _tourRepository;
        private TourImageRepository _tourImageRepository;
        public GuideMainWindow(User user)
        {
            InitializeComponent();
            _user = user;
            _tourImageRepository = new TourImageRepository();
            _tourRepository = new TourRepository();

            Update();
        }

        public void ScheduleTourClick(object sender, RoutedEventArgs e)
        {
            AddTourWindow tourWindow = new AddTourWindow(_user, _tourRepository, _tourImageRepository);
            tourWindow.ShowDialog();

        }

        public void DailyToursClick(object sender, RoutedEventArgs e) {

            DailyToursWindow dailyWindow = new DailyToursWindow();
            dailyWindow.ShowDialog();
        }

        public void Update()
        {
            
        }










    }
}
