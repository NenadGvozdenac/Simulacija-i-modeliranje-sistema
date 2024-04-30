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
using Xceed.Wpf.AvalonDock.Themes;

namespace BookingApp.WPF.Views.OwnerViews;

public partial class SettingsAndProfile : Page
{
    private SettingsViewModel _settingsViewModel;
    public SettingsAndProfile(User user)
    {
        _settingsViewModel = new SettingsViewModel(user);
        DataContext = _settingsViewModel;

        InitializeComponent();
        SetActiveTheme();
    }

    private void SetActiveTheme()
    {
        if (_settingsViewModel.OwnerUserDTO.OwnerInfo.PrefferedTheme == "Dark")
        {
            DarkModeRadioButton.IsChecked = true;
        }
        else
        {
            LightModeRadioButton.IsChecked = true;
        }
    }

    private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _settingsViewModel.ChangeLanguage();
    }

    private void DarkModeRadioButton_Checked(object sender, RoutedEventArgs e)
    {
        _settingsViewModel.SetTheme("Dark");
        SetDarkTheme();
    }

    private void LightModeRadioButton_Checked(object sender, RoutedEventArgs e)
    {
        _settingsViewModel.SetTheme("Light");
        SetLightTheme();
    }
    private void SetDarkTheme()
    {
        ResourceDictionary darkTheme = new ResourceDictionary
        {
            Source = new Uri("../../../WPF/Views/OwnerViews/Themes/Dark.xaml", UriKind.Relative)
        };
        // Clear all themes
        App.Current.Resources.MergedDictionaries.Clear();
        App.Current.Resources.MergedDictionaries.Add(darkTheme);
    }

    private void SetLightTheme()
    {
        ResourceDictionary lightTheme = new ResourceDictionary
        {
            Source = new Uri("../../../WPF/Views/OwnerViews/Themes/Light.xaml", UriKind.Relative)
        };
        // Clear all themes
        App.Current.Resources.MergedDictionaries.Clear();
        App.Current.Resources.MergedDictionaries.Add(lightTheme);
    }

    private void BackButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if(NavigationService.CanGoBack)
        {
            NavigationService.GoBack();
        }
    }
}
