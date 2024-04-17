using BookingApp.WPF.ViewModels.OwnerViewModels;
using BookingApp.WPF.ViewModels.OwnerViewModels.WrapperViewModels.MainWindowWrapperViewModels;
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

namespace BookingApp.WPF.Views.OwnerViews.MainWindowWrappers;

public partial class AccommodationRenovationWrapper : UserControl
{
    public RenovationsWrapperViewModel WrapperViewModel { get; set; }

    public AccommodationRenovationWrapper(MainPageViewModel mainPageViewModel)
    {
        InitializeComponent();
        WrapperViewModel = new RenovationsWrapperViewModel(this, mainPageViewModel);
    }
}
