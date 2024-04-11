using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModels.OwnerViewModels;
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

namespace BookingApp.View.OwnerViews;

public partial class DetailedGuestFeedbackPage : Page
{
    public DetailedGuestFeedbackPage(DetailedGuestFeedbackViewModel detailedGuestFeedbackViewModel)
    {
        DataContext = detailedGuestFeedbackViewModel;
        InitializeComponent();
        detailedGuestFeedbackViewModel.LoadImages(images_StackPanel);
    }

    private void BackArrowClick(object sender, MouseButtonEventArgs e)
    {
        if(NavigationService.CanGoBack)
        {
            NavigationService.GoBack();
        }
    }
}
