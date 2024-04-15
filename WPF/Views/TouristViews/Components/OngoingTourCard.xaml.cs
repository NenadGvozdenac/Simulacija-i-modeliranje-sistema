using BookingApp.Domain.Models;
using BookingApp.Repositories;
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

namespace BookingApp.WPF.Views.TouristViews.Components
{
    /// <summary>
    /// Interaction logic for OngoingTourCard.xaml
    /// </summary>
    public partial class OngoingTourCard : UserControl
    {
        public User _user {  get; set; }
        public OngoingTourCard()
        {
            InitializeComponent();
        }
        public void SetUser(User user)
        {
            _user = user;
        }

        private void SeeCheckpoints_Click(object sender, MouseButtonEventArgs e)
        {
            if (this.DataContext is Tour tour)
            {
                int tourId = tour.Id;

                Window parentWindow = Window.GetWindow(this);

                if (parentWindow is TouristMainWindow mainWindow)
                {
                    mainWindow.SeeCheckpoints(_user);
                }
            }
        }
    }
}
