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
using BookingApp.Domain.Models;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repositories;
using BookingApp.WPF.ViewModels.GuestViewModels;

namespace BookingApp.WPF.Views.GuestViews;


public partial class GuestMainWindow : Window
{
    public GuestMainWindowViewModel GuestViewModel { get; set; }
    public GuestMainWindow(User user)
    {
        InitializeComponent();
        GuestViewModel = new GuestMainWindowViewModel(this, user, GuestWindowFrame);
        DataContext = GuestViewModel;    
    
    }

    public void Accommodations_Click(object sender, RoutedEventArgs e)
    {
        GuestViewModel.Accommodations_Click();
    }
    public void MyReservations_Click(object sender, RoutedEventArgs e)
    {
        GuestViewModel.MyReservations_Click();
    }
    private void SeeMore_Click(object sender, RoutedEventArgs e)
    {
        GuestViewModel.SeeMore_Click();
    }

    private void Logout_Click(object sender, RoutedEventArgs e)
    {
        GuestViewModel.Logout_Click();
    }
}
