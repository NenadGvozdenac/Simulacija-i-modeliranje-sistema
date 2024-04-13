using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.Repositories;
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
public partial class AccommodationCard : UserControl
{
    public AccommodationCard()
    {
        InitializeComponent();
    }

    public void SeeMore_Click(object sender, RoutedEventArgs e)
    {
        if (this.DataContext is Accommodation accommodation)
        {
            int accommodationId = accommodation.Id;

            Window parentWindow = Window.GetWindow(this);

            if (parentWindow is GuestMainWindow mainWindow)
            {
                mainWindow.GuestViewModel.ShowAccommodationDetails(accommodationId);
            }
        }
    }

    private void UpdateBorder(object sender, DependencyPropertyChangedEventArgs e)
    {

        Accommodation accommodation = (Accommodation)DataContext;

        if (OwnerService.GetInstance().GetById(accommodation.OwnerId).Item1.IsSuperOwner == true)
        {
            MainBorder.BorderBrush = Brushes.Orange;
            SideBorder.BorderBrush = Brushes.Orange;
        }
    }
}
