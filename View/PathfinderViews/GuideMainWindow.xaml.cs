using BookingApp.Model.MutualModels;
using BookingApp.View.GuestViews;
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
        public GuideMainWindow(User user)
        {
            InitializeComponent();
            _user = user;
            //accommodation.username.Content = _user.Username;
        }

        public void ScheduleTourClick(object sender, RoutedEventArgs e)
        {
            AddTourWindow tourWindow = new AddTourWindow();
            tourWindow.Show();

        }












    }
}
