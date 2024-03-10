using BookingApp.Model.MutualModels;
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

namespace BookingApp.View.GuestViews.Components
{
    /// <summary>
    /// Interaction logic for AccommodationCard.xaml
    /// </summary>
    public partial class AccommodationCard : UserControl
    {
        public AccommodationCard()
        {
            InitializeComponent();
            
        }

        private void SeeMore_Click(object sender, RoutedEventArgs e)
        {
            // Get the DataContext (AccommodationViewModel) of the AccommodationCard
            if (this.DataContext is Accommodation accommodation)
            {
                int accommodationId = accommodation.Id;

                // Find the nearest parent window
                Window parentWindow = Window.GetWindow(this);

                // Cast the parent window to GuestMainWindow
                if (parentWindow is GuestMainWindow mainWindow)
                {
                    // Show the details UserControl passing the selected accommodation
                    mainWindow.ShowAccommodationDetails(accommodationId);
                }
            }
        }

    }
}
