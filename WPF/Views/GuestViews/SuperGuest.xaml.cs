using BookingApp.Application.UseCases;
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

namespace BookingApp.WPF.Views.GuestViews;

/// <summary>
/// Interaction logic for SuperGuest.xaml
/// </summary>
public partial class SuperGuest : UserControl
{
    public SuperGuest(User _user)
    {
        InitializeComponent();

        if (GuestService.GetInstance().GetByGuestId(_user.Id).IsSuperGuest)
        {            
            pointsTextBlock.Text = "Remaining points for this year: " + GuestService.GetInstance().GetByGuestId(_user.Id).NumberOfPoints;
        }
        else
        {
            congratulationsTextBlock.Text = "Sadly, you are still not super guest! Keep up the number of reservations to become one!";
            pointsTextBlock.Visibility = Visibility.Hidden;
        }
    }


}
