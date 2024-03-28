using BookingApp.Model.MutualModels;
using BookingApp.Model.OwnerModels;
using BookingApp.Repository.OwnerRepositories;
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

namespace BookingApp.View.OwnerViews;

public partial class SettingsAndProfile : Page
{
    private SettingsViewModel _settingsViewModel;
    public SettingsAndProfile(User user)
    {
        _settingsViewModel = new SettingsViewModel(user);
        DataContext = _settingsViewModel;

        InitializeComponent();
    }

    private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void DarkModeRadioButton_Checked(object sender, RoutedEventArgs e)
    {

    }

    private void LightModeRadioButton_Checked(object sender, RoutedEventArgs e)
    {

    }

    private void BackButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if(NavigationService.CanGoBack)
        {
            NavigationService.GoBack();
        }
    }
}
