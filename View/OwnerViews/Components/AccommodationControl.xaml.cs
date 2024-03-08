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

namespace BookingApp.View.OwnerViews.Components
{
    /// <summary>
    /// Interaction logic for AccommodationControl.xaml
    /// </summary>
    public partial class AccommodationControl : UserControl
    {
        public Accommodation Accommodation { get; set; }
        public AccommodationControl(Accommodation accommodation)
        {
            InitializeComponent();
            Accommodation = accommodation;
            SetupAccommodation();
        }

        private void SetupAccommodation()
        {
            AccommodationName.Content = Accommodation.Name;
            //TODO: AccommodationLocation.Content = Accommodation.Location;
            AccommodationType.Content = Accommodation.Type;
        }
    }
}
