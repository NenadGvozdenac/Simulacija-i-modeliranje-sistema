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

namespace BookingApp.View.GuestViews.Components;
public partial class AccommodationCard : UserControl
{
    public AccommodationCard()
    {
        InitializeComponent();
    }

    private void SeeMore_Click(object sender, RoutedEventArgs e)
    {
        if (this.DataContext is Accommodation accommodation)
        {
            int accommodationId = accommodation.Id;

            Window parentWindow = Window.GetWindow(this);

            if (parentWindow is GuestMainWindow mainWindow)
            {
                mainWindow.ShowAccommodationDetails(accommodationId);
            }
        }
    }

}
