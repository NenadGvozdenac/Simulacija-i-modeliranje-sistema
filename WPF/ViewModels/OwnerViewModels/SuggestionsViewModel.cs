using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.WPF.Views.OwnerViews;
using BookingApp.WPF.Views.OwnerViews.Components;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.OwnerViewModels;

public partial class SuggestionsViewModel : ObservableObject
{
    private SuggestionsPage suggestionsPage;
    private User user;

    [ObservableProperty]
    private Location _popularLocation;

    [ObservableProperty]
    private int _popularLocationReservations;

    [ObservableProperty]
    private double _popularLocationFullness;
    public string ButtonText => string.Format("Open accommodation in {0}, {1}", PopularLocation.City, PopularLocation.Country);
    public string PopularLocationText => string.Format("{0} overall reservations", PopularLocationReservations);
    public string PopularLocationFullnessText => string.Format("{0:F2}% fullness", PopularLocationFullness);

    [ObservableProperty]
    private Location _leastPopularLocation;

    [ObservableProperty]
    private int _leastPopularLocationReservations;

    [ObservableProperty]
    private double _leastPopularLocationFullness;

    public string LeastPopularLocationText => string.Format("{0} overall reservations", LeastPopularLocationReservations);
    public string LeastPopularLocationFullnessText => string.Format("{0:F2}% fullness", LeastPopularLocationFullness);

    [ObservableProperty]
    private List<Accommodation> _leastPopularAccommodations;
    public SuggestionsViewModel(SuggestionsPage suggestionsPage, User user)
    {
        this.suggestionsPage = suggestionsPage;
        this.user = user;
;
        Refresh();
    }

    private void AddAccommodations()
    {
        suggestionsPage.AccommodationsPanel.Children.Clear();
        foreach (var accommodation in LeastPopularAccommodations)
        {
            suggestionsPage.AccommodationsPanel.Children.Add(new AccommodationControl(accommodation));
        }
    }

    public void Refresh()
    {
        PopularLocation = AccommodationService.GetInstance().GetMostPopularLocation();
        PopularLocationReservations = AccommodationService.GetInstance().GetLocationReservations(PopularLocation);
        PopularLocationFullness = AccommodationService.GetInstance().GetLocationFullness(PopularLocation, user);

        LeastPopularLocation = AccommodationService.GetInstance().GetLeastPopularLocation();
        LeastPopularLocationReservations = AccommodationService.GetInstance().GetLocationReservations(LeastPopularLocation);
        LeastPopularLocationFullness = AccommodationService.GetInstance().GetLocationFullness(LeastPopularLocation, user);

        LeastPopularAccommodations = AccommodationService.GetInstance().GetAccommodationsByLocation(LeastPopularLocation, user.Id);

        AddAccommodations();
    }
}
