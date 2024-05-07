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

namespace BookingApp.WPF.Views.OwnerViews.Components;

/// <summary>
/// Interaction logic for EnterNewLocationModal.xaml
/// </summary>
public partial class EnterNewLocationModal : UserControl
{
    private string _city;
    public string City
    {
        get => _city;
        set
        {
            if (value != null)
            {
                _city = value;
            }
        }
    }

    private string _country;
    public string Country
    {
        get => _country;
        set
        {
            if (value != null)
            {
                _country = value;
            }
        }
    }

    private readonly AddAccommodationViewModel viewModel;

    public EnterNewLocationModal(AddAccommodationViewModel viewModel)
    {
        InitializeComponent();
        DataContext = this;
        this.viewModel = viewModel;
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        this.viewModel.Location = string.Format("{0}, {1}", this.City, this.Country);

        Location location = new Location
        {
            City = this.City,
            Country = this.Country
        };

        LocationService.GetInstance().Add(location);

        this.Visibility = Visibility.Collapsed;
        Clear();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        this.Visibility = Visibility.Collapsed;
        Clear();
    }

    private void Clear()
    {
        this.City = "";
        this.Country = "";
    }
}
