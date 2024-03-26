using BookingApp.Model.MutualModels;
using BookingApp.Repository.MutualRepositories;
using BookingApp.Repository;
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

namespace BookingApp.View.OwnerViews.MainWindowWrappers;

public partial class AccommodationWrapper : UserControl
{
    private AccommodationRepository _accommodationRepository;
    private User _user;
    private ObservableCollection<Accommodation> _accommodations;

    public AccommodationWrapper(User user)
    {
        InitializeComponent();

        _user = user;
        _accommodationRepository = AccommodationRepository.GetInstance();
        _accommodations = new ObservableCollection<Accommodation>();

        Update();
    }

    public void Update()
    {
        _accommodations.Clear();

        List<Accommodation> accommodations = _accommodationRepository.GetAccommodationsByOwnerId(_user.Id);

        foreach(Accommodation accommodation in accommodations)
        {
            _accommodations.Add(accommodation);
        }

        AddAccommodations();
    }

    private void AddAccommodations()
    {
        Accommodations.Children.Clear();

        foreach(Accommodation accommodation in _accommodations)
        {
            AccommodationControl component = new AccommodationControl(accommodation, LocationRepository.GetInstance().GetById(accommodation.LocationId));
            component.Margin = new Thickness(15);

            component.AccommodationSeeMore += (sender, e) => InvokeSeeMore(e);

            Accommodations.Children.Add(component);
        }
    }

    private void InvokeSeeMore(Accommodation e)
    {
        LoadAccommodationInfo(e);
        DetailedAccommodationPage detailedAccommodationPage = new DetailedAccommodationPage(e);
        NavigationService.GetNavigationService(this).Navigate(detailedAccommodationPage);
    }

    private void LoadAccommodationInfo(Accommodation e)
    {
        e.Location = LocationRepository.GetInstance().GetById(e.LocationId);
        e.Images = AccommodationImageRepository.GetInstance().GetImagesByAccommodationId(e.Id);
    }
}
