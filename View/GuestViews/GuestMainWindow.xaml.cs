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

namespace BookingApp.View.GuestViews;


public partial class GuestMainWindow : Window
{
    private readonly User _user;
    public AccommodationRepository accomodationrepository { get; set; }
    public Accommodations AccommodationsUserControl;
    public MyReservations MyReservationsUserControl;

    public GuestMainWindow(User user)
    {
        InitializeComponent();       
        accomodationrepository = new AccommodationRepository();
        _user = user;
        Update(_user);
        AccommodationsUserControl = new Accommodations(user);
        GuestWindowFrame.Content = AccommodationsUserControl;
    }
    private void Accommodations_Click(object sender, RoutedEventArgs e)
    {
        GuestWindowFrame.Content = AccommodationsUserControl;
    }
    private void MyReservations_Click(object sender, RoutedEventArgs e)
    {
        GuestWindowFrame.Content = MyReservationsUserControl;
    }
    public void ShowAccommodationDetails(int accommodationId)
    {
        Accommodation detailedAccommodation = accomodationrepository.GetById(accommodationId);
        GuestWindowFrame.Content = new AccommodationDetails(detailedAccommodation, _user);
    }
    private void Update(User user)
    {
        AccommodationsUserControl = new Accommodations(user);
        MyReservationsUserControl = new MyReservations();
    }
}
