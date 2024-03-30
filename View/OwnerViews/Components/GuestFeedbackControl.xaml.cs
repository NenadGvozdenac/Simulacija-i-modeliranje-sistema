using BookingApp.Miscellaneous;
using BookingApp.Model.MutualModels;
using BookingApp.ViewModel.OwnerViewModels;
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

namespace BookingApp.View.OwnerViews.Components;

public partial class GuestFeedbackControl : UserControl
{
    public DetailedGuestFeedbackViewModel DetailedGuestFeedbackViewModel;
    public GuestFeedbackControl(AccommodationReview accommodationReview)
    {
        DetailedGuestFeedbackViewModel = new DetailedGuestFeedbackViewModel(accommodationReview);
        DataContext = DetailedGuestFeedbackViewModel;
        InitializeComponent();
    }

    private void Card_MouseDown(object sender, MouseButtonEventArgs e)
    {
        DetailedGuestFeedbackPage detailedGuestFeedbackPage = new DetailedGuestFeedbackPage(DetailedGuestFeedbackViewModel);
        NavigationService.GetNavigationService(this).Navigate(detailedGuestFeedbackPage);
    }
}
