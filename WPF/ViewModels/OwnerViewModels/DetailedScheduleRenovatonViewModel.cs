using BookingApp.Application.Commands;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Miscellaneous;
using BookingApp.Domain.Models;
using BookingApp.WPF.DTOs.OwnerDTOs;
using BookingApp.WPF.Views.OwnerViews;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.OwnerViewModels;

public partial class DetailedScheduleRenovatonViewModel : ObservableObject
{
    public AccommodationDTO Accommodation { get; set; }

    private readonly DetailedScheduleRenovationPage page;
    private Accommodation _accommodation;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FindAvailableTimespanForRenovationCommand))]
    private DateTime _startDate;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FindAvailableTimespanForRenovationCommand))]
    private DateTime _endDate;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FindAvailableTimespanForRenovationCommand))]
    private string _timePeriod;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ScheduleRenovationCommand))]
    private DateSpan _selectedDateSpan;

    [ObservableProperty]
    private List<DateSpan> _dates;

    public ICommand FindAvailableTimespanForRenovationCommand => new FindAvailableTimespanForRenovationCommand(page);
    public ICommand ScheduleRenovationCommand => new ScheduleRenovationCommand(page);

    public DetailedScheduleRenovatonViewModel(DetailedScheduleRenovationPage page, Accommodation accommodation)
    {
        this.page = page;
        this._accommodation = accommodation;
        Accommodation = new AccommodationDTO(accommodation);

        StartDate = DateTime.Now;
        EndDate = DateTime.Now;
    }

    public void PopulateTable()
    {
        Dates = AccommodationRenovationService.GetInstance().FindAvailableTimespanForRenovation(_accommodation, StartDate, EndDate, int.Parse(TimePeriod));
    }

    public void ScheduleRenovation()
    {
        AccommodationRenovationService.GetInstance().ScheduleRenovation(_accommodation, SelectedDateSpan);
    }
}
