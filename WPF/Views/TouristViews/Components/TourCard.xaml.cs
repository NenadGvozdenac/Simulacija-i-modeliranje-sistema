using BookingApp.Domain.Models;
using BookingApp.WPF.Views.TouristViews;
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
    /// Interaction logic for TourCard.xaml
    /// </summary>
    public partial class TourCard : UserControl
    {
        public TourCard()
        {
            InitializeComponent();
        }

        private void SeeTour_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is Tour tour)
            {
                int tourId = tour.Id;

                Window parentWindow = Window.GetWindow(this);

                if (parentWindow is TouristMainWindow mainWindow)
                {
                    mainWindow.ShowTourDetails(tourId);
                }
            }
        }

    }
}
