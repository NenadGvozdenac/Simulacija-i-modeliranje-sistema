using BookingApp.Domain.Models;
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

namespace BookingApp.WPF.Views.TouristViews
{
    /// <summary>
    /// Interaction logic for ComplexTourRequests.xaml
    /// </summary>
    public partial class ComplexTourRequests : UserControl
    {
        public ComplexTourRequests()
        {
            InitializeComponent();
        }

        public void Add_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);

            if (parentWindow is TouristMainWindow mainWindow)
            {
                //mainWindow.ShowReservationReview(user, selectedTour, guestNumber, tourists, tourVoucher, tourStartTime);
            }
        }
    }
}
