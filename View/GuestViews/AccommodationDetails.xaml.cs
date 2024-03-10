using BookingApp.Model.MutualModels;
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
        public AccommodationDetails()
        {
            InitializeComponent();
        }

        public void SetAccommodation(Accommodation accommodation)
        {
            accommodationName.Text = accommodation.Name;
            accomodationAverageReviewScore.Text = accommodation.AverageReviewScore.ToString() + "/10";
        }
    }
}
