using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.Resources.Types;
using BookingApp.WPF.ViewModels.OwnerViewModels.WrapperViewModels.MainWindowWrapperViewModels;
using BookingApp.WPF.Views.OwnerViews;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.OwnerViewModels;

public partial class AccommodationStatisticsViewModel : ObservableObject
{
    [ObservableProperty]
    private List<string> _years = new() { "2021", "2022", "2023", "2024", "2025" };

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(NumberOfReservations))]
    [NotifyPropertyChangedFor(nameof(NumberOfCancelledReservations))]
    [NotifyPropertyChangedFor(nameof(NumberOfMovedReservations))]
    [NotifyPropertyChangedFor(nameof(NumberOfRenovationRecommendations))]
    private string _selectedYear;

    [ObservableProperty]
    private string _selectedYearDetails;

    [ObservableProperty]
    private List<string> _months = new() { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

    [ObservableProperty]
    private string _selectedMonth;

    [ObservableProperty]
    private string _accommodationName;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(NumberOfReservations))]
    [NotifyPropertyChangedFor(nameof(NumberOfCancelledReservations))]
    [NotifyPropertyChangedFor(nameof(NumberOfMovedReservations))]
    [NotifyPropertyChangedFor(nameof(NumberOfRenovationRecommendations))]
    private Accommodation _selectedAccommodation;

    private readonly AccommodationStatisticsPage page;
    public string NumberOfReservations => GetNumberOfReservations();
    public string NumberOfCancelledReservations => GetNumberOfCancelledReservations();
    public string NumberOfMovedReservations => GetNumberOfMovedReservations();
    public string NumberOfRenovationRecommendations => GetNumberOfRenovationRecommendations();

    private string GetNumberOfRenovationRecommendations()
    {
        if (SelectedAccommodation == null || SelectedYear == null)
            return string.Empty;

        List<AccommodationReview> accommodationReviews =
            AccommodationReviewService.GetInstance().GetByAccommodationId(SelectedAccommodation.Id)
            .Where(review => review.RequiresRenovation)
            .Where(review => review.Reservation.FirstDateOfStaying.Year.ToString() == SelectedYear).ToList();

        return string.Format("{0} renovation recommendations", accommodationReviews.Count);
    }

    private string GetNumberOfMovedReservations()
    {
        if (SelectedAccommodation == null || SelectedYear == null)
            return string.Empty;

        List<AccommodationReservationMoving> accommodationReservations = 
            AccommodationReservationService.GetInstance().GetMovingsByOwnerId(User.Id)
            .Where(moving => moving.CurrentReservationTimespan.Start.Year.ToString() == SelectedYear).ToList();

        return string.Format("{0} moved reservations", accommodationReservations.Count);
    }

    private string GetNumberOfReservations()
    {
        if(SelectedAccommodation == null || SelectedYear == null)
            return string.Empty;

        List<AccommodationReservation> accommodationReservations = 
            AccommodationReservationService.GetInstance().GetByAccommodationId(SelectedAccommodation.Id)
            .Where(res => res.FirstDateOfStaying.Year.ToString() == SelectedYear && res.ReservationType != ReservationType.Cancelled).ToList();

        return string.Format("{0} reservations", accommodationReservations.Count);
    }


    private string GetNumberOfCancelledReservations()
    {
        if (SelectedAccommodation == null || SelectedYear == null)
            return string.Empty;

        List<AccommodationReservation> accommodationReservations = 
            AccommodationReservationService.GetInstance().GetByAccommodationId(SelectedAccommodation.Id)
            .Where(res => res.FirstDateOfStaying.Year.ToString() == SelectedYear && res.ReservationType == ReservationType.Cancelled).ToList();

        return string.Format("{0} cancelled reservations", accommodationReservations.Count);
    }


    public User User { get; }
    public AccommodationStatisticsViewModel(User user, AccommodationStatisticsPage page)
    {
        User = user;
        this.page = page;
    }

    public void AccommodationTextBox_TextChanged()
    {
        string searchText = page.accommodationName_textbox.Text.ToLower();

        if (searchText.Length == 0)
        {
            _accommodationName = "";
            return;
        }

        UpdateAccommodation(searchText);
    }

    public void AccommodationTextBox_PreviewKeyDown(Key key)
    {
        if (key == Key.Back)
        {
            AccommodationName = "";
            return;
        }

        if (key == Key.Tab || key == Key.Enter)
        {
            string searchText = page.accommodationName_textbox.Text.ToLower();

            UpdateAccommodation(searchText);
        }
    }

    private void UpdateAccommodation(string searchText)
    {
        string matchedAcc = AccommodationService.GetInstance().GetByOwnerId(User.Id).FirstOrDefault(acc => acc.Name.ToLower().StartsWith(searchText))?.Name;

        if (!string.IsNullOrEmpty(matchedAcc))
        {
            AccommodationName = matchedAcc;
            SelectedAccommodation = AccommodationService.GetInstance().GetByOwnerId(User.Id).FirstOrDefault(acc => acc.Name == matchedAcc);
            page.accommodationName_textbox.Select(searchText.Length, matchedAcc.Length - searchText.Length);
        }
    }
}
