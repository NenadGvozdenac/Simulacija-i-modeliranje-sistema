using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using BookingApp.View.OwnerViews.Components;
using BookingApp.WPF.ViewModels.OwnerViewModels;
using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.ViewModels.OwnerViewModels.WrapperViewModels.MainWindowWrapperViewModels;

namespace BookingApp.View.OwnerViews.MainWindowWrappers;

public partial class AccommodationWrapper : UserControl
{
    public AccommodationsWrapperViewModel WrapperViewModel;
    public AccommodationWrapper(MainPageViewModel mainPageViewModel)
    {
        InitializeComponent();

        WrapperViewModel = new AccommodationsWrapperViewModel(this, mainPageViewModel);
    }
}
