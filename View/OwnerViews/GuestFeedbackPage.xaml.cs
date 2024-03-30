using BookingApp.Model.MutualModels;
using System;
using System.Windows.Controls;

namespace BookingApp.View.OwnerViews;
public partial class GuestFeedbackPage : Page
{
    public GuestFeedbackPage(User user)
    {
        InitializeComponent();
    }

    private void BackButton_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if(NavigationService.CanGoBack)
        {
            NavigationService.GoBack();
        }
    }
}
