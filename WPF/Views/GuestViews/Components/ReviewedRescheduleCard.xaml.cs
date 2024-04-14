using BookingApp.Resources.Types;
using BookingApp.WPF.DTOs.GuestDTOs;
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

namespace BookingApp.WPF.Views.GuestViews.Components;

public partial class ReviewedRescheduleCard : UserControl
{
    public ReviewedRescheduleCard()
    {
        InitializeComponent();
    }

    private void UpdateStatus(object sender, DependencyPropertyChangedEventArgs e)
    {
        AccommodationMovingDTO moving = (AccommodationMovingDTO)DataContext;

        if (moving.Status == ReschedulingStatus.Accepted)
        {
            Status_TextBlock.Text = "Approved";
            Status_TextBlock.Foreground = Brushes.LightGreen;
        }
        else if (moving.Status == ReschedulingStatus.TimedOut)
        {
            Status_TextBlock.Text = "Timed Out";
            Status_TextBlock.Foreground = Brushes.Purple;

        }
        else 
        {
            Status_TextBlock.Text = "Rejected";
            Status_TextBlock.Foreground = Brushes.Red;
        }
    }
}
