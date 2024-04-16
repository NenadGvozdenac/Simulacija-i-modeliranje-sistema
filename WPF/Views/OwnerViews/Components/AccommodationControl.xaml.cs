using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.ViewModels.OwnerViewModels.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
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

namespace BookingApp.WPF.Views.OwnerViews.Components;

public partial class AccommodationControl : UserControl
{
    public AccommodationCardViewModel AccommodationCardViewModel { get; set; }
    public AccommodationControl(Accommodation accommodation)
    {
        InitializeComponent();
        AccommodationCardViewModel = new AccommodationCardViewModel(this, accommodation);
        DataContext = AccommodationCardViewModel;
    }

    private void AccommodationClick_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        AccommodationCardViewModel.NavigateToDetails();
    }
}
