using BookingApp.Application.Localization;
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
    [NotifyPropertyChangedFor(nameof(NumberOfReservationsPerMonth))]
    [NotifyPropertyChangedFor(nameof(NumberOfCancelledReservationsPerMonth))]
    [NotifyPropertyChangedFor(nameof(NumberOfMovedReservationsPerMonth))]
    [NotifyPropertyChangedFor(nameof(NumberOfRenovationRecommendationsPerMonth))]
    private string _selectedYear;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(MostPopularMonthInYear))]
    private string _selectedYearDetails;

    [ObservableProperty]
    private List<Month> _months = MonthService.GetInstance().GetAll();

    [ObservableProperty]
    private List<string> _monthsNames = MonthService.GetInstance().GetAllNames();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(NumberOfReservationsPerMonth))]
    [NotifyPropertyChangedFor(nameof(NumberOfCancelledReservationsPerMonth))]
    [NotifyPropertyChangedFor(nameof(NumberOfMovedReservationsPerMonth))]
    [NotifyPropertyChangedFor(nameof(NumberOfRenovationRecommendationsPerMonth))]
    private string _selectedMonth;

    [ObservableProperty]
    private string _accommodationName;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(NumberOfReservations))]
    [NotifyPropertyChangedFor(nameof(NumberOfCancelledReservations))]
    [NotifyPropertyChangedFor(nameof(NumberOfMovedReservations))]
    [NotifyPropertyChangedFor(nameof(NumberOfRenovationRecommendations))]
    [NotifyPropertyChangedFor(nameof(NumberOfReservationsPerMonth))]
    [NotifyPropertyChangedFor(nameof(NumberOfCancelledReservationsPerMonth))]
    [NotifyPropertyChangedFor(nameof(NumberOfMovedReservationsPerMonth))]
    [NotifyPropertyChangedFor(nameof(NumberOfRenovationRecommendationsPerMonth))]
    [NotifyPropertyChangedFor(nameof(MostPopularYear))]
    [NotifyPropertyChangedFor(nameof(MostPopularMonthInYear))]
    private Accommodation _selectedAccommodation;

    private readonly AccommodationStatisticsPage page;
    public string NumberOfReservations => GetNumberOfReservations();
    public string NumberOfCancelledReservations => GetNumberOfCancelledReservations();
    public string NumberOfMovedReservations => GetNumberOfMovedReservations();
    public string NumberOfRenovationRecommendations => GetNumberOfRenovationRecommendations();
    public string NumberOfReservationsPerMonth => GetNumberOfReservationsPerMonth();
    public string NumberOfCancelledReservationsPerMonth => GetNumberOfCancelledReservationsPerMonth();
    public string NumberOfMovedReservationsPerMonth => GetNumberOfMovedReservationsPerMonth();
    public string NumberOfRenovationRecommendationsPerMonth => GetNumberOfRenovationRecommendationsPerMonth();
    public string MostPopularYear => GetMostPopularYear();
    public string MostPopularMonthInYear => GetMostPopularMonthInYear();

    private string GetMostPopularMonthInYear()
    {
        if (SelectedAccommodation == null || SelectedYearDetails == null)
            return string.Empty;

        int month = Month.GetMostPopularMonthInYear(SelectedAccommodation.Id, SelectedYearDetails);

        if(month == -1)
            return TranslationSource.Instance["NoReservationsInYear"];

        return string.Format("{0}: {1}", TranslationSource.Instance["MostPopularMonth"], MonthService.GetInstance().GetMonthByNumber(month).Name);
    }

    private string GetMostPopularYear()
    {
        if (SelectedAccommodation == null)
            return string.Empty;

        List<AccommodationReservation> accommodationReservations = AccommodationReservationService.GetInstance().GetByAccommodationId(SelectedAccommodation.Id);

        if (accommodationReservations.Count == 0)
            return string.Empty;

        Dictionary<string, int> years = new();

        foreach (var reservation in accommodationReservations)
        {
            if (years.ContainsKey(reservation.FirstDateOfStaying.Year.ToString()))
                years[reservation.FirstDateOfStaying.Year.ToString()]++;
            else
                years.Add(reservation.FirstDateOfStaying.Year.ToString(), 1);
        }

        var year = years.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;

        return string.Format("{0}: {1}", TranslationSource.Instance["MostPopularYear"], year);
    }

    private string GetNumberOfReservationsPerMonth()
    {
        if(SelectedAccommodation == null || SelectedYear == null || SelectedMonth == null)
            return string.Empty;

        int numberOfReservations = Month.GetNumberOfReservationsPerMonth(SelectedAccommodation.Id, SelectedYear, SelectedMonth);

        return string.Format("{0} {1}", numberOfReservations, TranslationSource.Instance["ReservationsLC"]);
    }
    private string GetNumberOfCancelledReservationsPerMonth()
    {
        if (SelectedAccommodation == null || SelectedYear == null || SelectedMonth == null)
            return string.Empty;

        int numberOfCancelledReservations = Month.GetNumberOfCancelledReservationsPerMonth(SelectedAccommodation.Id, SelectedYear, SelectedMonth);

        return string.Format("{0} {1}", numberOfCancelledReservations, TranslationSource.Instance["CancelledReservationsLC"]);
    }
    private string GetNumberOfMovedReservationsPerMonth()
    {
        if (SelectedAccommodation == null || SelectedYear == null || SelectedMonth == null)
            return string.Empty;

        int numberOfMovedReservations = Month.GetNumberOfMovedReservationsPerMonth(SelectedAccommodation.Id, SelectedYear, SelectedMonth);

        return string.Format("{0} {1}", numberOfMovedReservations, TranslationSource.Instance["MovedReservationsLC"]);
    }
    private string GetNumberOfRenovationRecommendationsPerMonth()
    {
        if (SelectedAccommodation == null || SelectedYear == null || SelectedMonth == null)
            return string.Empty;

        int numberOfRenovationRecommendations = Month.GetNumberOfRenovationRecommendationsPerMonth(SelectedAccommodation.Id, SelectedYear, SelectedMonth);

        return string.Format("{0} {1}", numberOfRenovationRecommendations, TranslationSource.Instance["RenovationRecommendationsLC"]);
    }

    private string GetNumberOfRenovationRecommendations()
    {
        if (SelectedAccommodation == null || SelectedYear == null)
            return string.Empty;

        List<AccommodationReview> accommodationReviews =
            AccommodationReviewService.GetInstance().GetByAccommodationId(SelectedAccommodation.Id)
            .Where(review => review.RequiresRenovation)
            .Where(review => review.Reservation.FirstDateOfStaying.Year.ToString() == SelectedYear).ToList();

        return string.Format("{0} {1}", accommodationReviews.Count, TranslationSource.Instance["RenovationRecommendationsLC"]);
    }

    private string GetNumberOfMovedReservations()
    {
        if (SelectedAccommodation == null || SelectedYear == null)
            return string.Empty;

        List<AccommodationReservationMoving> accommodationReservations = 
            AccommodationReservationService.GetInstance().GetMovingsByOwnerId(User.Id)
            .Where(moving => moving.CurrentReservationTimespan.Start.Year.ToString() == SelectedYear).ToList();

        return string.Format("{0} {1}", accommodationReservations.Count, TranslationSource.Instance["MovedReservationsLC"]);
    }

    private string GetNumberOfReservations()
    {
        if(SelectedAccommodation == null || SelectedYear == null)
            return string.Empty;

        List<AccommodationReservation> accommodationReservations = 
            AccommodationReservationService.GetInstance().GetByAccommodationId(SelectedAccommodation.Id)
            .Where(res => res.FirstDateOfStaying.Year.ToString() == SelectedYear && res.ReservationType != ReservationType.Cancelled).ToList();

        return string.Format("{0} {1}", accommodationReservations.Count, TranslationSource.Instance["ReservationsLC"]);
    }


    private string GetNumberOfCancelledReservations()
    {
        if (SelectedAccommodation == null || SelectedYear == null)
            return string.Empty;

        List<AccommodationReservation> accommodationReservations = 
            AccommodationReservationService.GetInstance().GetByAccommodationId(SelectedAccommodation.Id)
            .Where(res => res.FirstDateOfStaying.Year.ToString() == SelectedYear && res.ReservationType == ReservationType.Cancelled).ToList();

        return string.Format("{0} {1}", accommodationReservations.Count, TranslationSource.Instance["CancelledReservationsLC"]);
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
