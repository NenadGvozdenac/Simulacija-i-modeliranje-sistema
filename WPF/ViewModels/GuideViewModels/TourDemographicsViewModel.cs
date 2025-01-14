﻿using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.View.PathfinderViews;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using BookingApp.Application.UseCases;
using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.ObjectModel;

namespace BookingApp.WPF.ViewModels.GuideViewModels
{
    public class TourDemographicsViewModel : INotifyPropertyChanged
    {
        private int sub18;
        public int Sub18
        {
            get => sub18;
            set
            {
                if (value != sub18)
                {
                    sub18 = value;
                    OnPropertyChanged();
                }
            }
        }

        private int middle;
        public int Middle
        {
            get => middle;
            set
            {
                if (value != middle)
                {
                    middle = value;
                    OnPropertyChanged();
                }
            }
        }

        private int above50;
        public int Above50
        {
            get => above50;
            set
            {
                if (value != above50)
                {
                    above50 = value;
                    OnPropertyChanged();
                }
            }
        }

        private string tourName;

        public string TourName
        {
            get => tourName;
            set
            {
                if (value != tourName)
                {
                    tourName = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<int> times { get; set; }

        private SeriesCollection _pieSeriesCollection;
        public SeriesCollection PieSeriesCollection
        {
            get { return _pieSeriesCollection; }
            set
            {
                _pieSeriesCollection = value;
                OnPropertyChanged();
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        public TourDemographics tourDemographics { get; set; }  

        // The method to invoke the property changed event
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public TourDemographicsViewModel(TourDemographics _tourDemographics)
        {
            tourDemographics = _tourDemographics;

            
            

            TourName = FindMostReserved().Name;
            Sub18 = FindSub18(FindMostReserved());
            Middle = FindMiddle(FindMostReserved());
            Above50 = FindAbove50(FindMostReserved());

           

            times = FindYears();
            
            tourDemographics.demographicsControl.demographicsControlViewModel.StatsButtonClickedControl += (s,e) => OnStatsButtonClicked_Handler(s,e);
            UpdatePieChart();
            

        }

        public void Update()
        {

        }

        private void UpdatePieChart()
        {
            PieSeriesCollection = new SeriesCollection
        {
            new PieSeries
            {
                Title = "Underage",
                Values = new ChartValues<double> { Sub18 }
            },
            new PieSeries
            {
                Title = "Adult",
                Values = new ChartValues<double> { Middle }
            },
            new PieSeries
            {
                Title = "Elderly",
                Values = new ChartValues<double> { Above50 }
            }
        };
        }

        public Tour FindMostReserved()
        {
            List<TourStartTime> dates = new List<TourStartTime>();
            List<Tour> tours = new List<Tour>();
            tours = TourService.GetInstance().GetAll();
            Tour tour = tours[0];
            int touristNumber = 0;
            int touristNumberTemp = 0;

            foreach (Tour t in tours)
            {
                dates = TourStartTimeService.GetInstance().GetByTourId(t.Id).Where(a => a.Status == "passed").ToList();

                foreach (TourStartTime date in dates)
                {
                    foreach (TouristReservation reservation in TourReservationService.GetInstance().GetAll())
                    {
                        if (reservation.Id_TourTime == date.Id)
                        {
                            touristNumberTemp++;
                        }
                    }

                }
                if (touristNumberTemp > touristNumber)
                {
                    touristNumber = touristNumberTemp;
                    tour = t;
                }
                touristNumberTemp = 0;
            }
            return tour;
        }

        public List<Tourist> GroupTourists(Tour t)
        {

            Tour tour = new Tour();
            tour = t;
            List<TourStartTime> dates = new List<TourStartTime>();
            dates = TourStartTimeService.GetInstance().GetByTourId(t.Id).Where(a => a.Status == "passed").ToList();
            List<TouristReservation> reservations = new List<TouristReservation>();

            foreach (TourStartTime time in dates)
            {
                reservations.AddRange(TourReservationService.GetInstance().GetByTimeId(time.Id));
            }



            List<Tourist> tourists = new List<Tourist>();

            foreach (TouristReservation reservation in reservations)
            {
                Tourist tourist = new Tourist();
                tourist.Name = TouristService.GetInstance().GetById(reservation.Id_Tourist).Name;
                tourist.Surname = TouristService.GetInstance().GetById(reservation.Id_Tourist).Surname;
                tourist.Age = TouristService.GetInstance().GetById(reservation.Id_Tourist).Age;
                tourists.Add(tourist);
            }

            return tourists;

        }


        public int FindSub18(Tour t)
        {
            
            List<Tourist> tourists = GroupTourists(t);
            int sub18 = 0;
            foreach (Tourist tourist in tourists)
            {
                if (tourist.Age < 18)
                    sub18++;
            }
            return sub18;
        }

        public int FindMiddle(Tour t)
        {
            
            List<Tourist> tourists = GroupTourists(t);
            int middle = 0;
            foreach (Tourist tourist in tourists)
            {
                if (tourist.Age >= 18 && tourist.Age < 50)
                    middle++;
            }
            return middle;
        }

        public int FindAbove50(Tour t)
        {
            
            List<Tourist> tourists = GroupTourists(t);
            int above50 = 0;
            foreach (Tourist tourist in tourists)
            {
                if (tourist.Age >= 50)
                    above50++;
            }
            return above50;
        }

        public List<int> FindYears()
        {
            List<int> times = new List<int>();

            foreach (TourStartTime time in TourStartTimeService.GetInstance().GetAll())
            {
                if (!times.Contains(time.Time.Year) && time.Status == "passed")
                    times.Add(time.Time.Year);
            }
            return times;
        }

        public void OnStatsButtonClicked_Handler(object sender, BeginButtonClickedEventArgs e)
        {
           OnStatsButtonClicked(new BeginButtonClickedEventArgs(e.TourId, e.StartTime));

        }

        public void OnStatsButtonClicked(BeginButtonClickedEventArgs e)
        {
            Tour t = TourService.GetInstance().GetById(e.TourId);
            Sub18 = FindSub18(t);

            Middle = FindMiddle(t);

            Above50 = FindAbove50(t);

            TourName = TourService.GetInstance().GetById(e.TourId).Name;

            UpdatePieChart();
        }

        public void AllTime_Click(object sender, RoutedEventArgs e)
        {
            Sub18 = FindSub18(FindMostReserved());
            Middle = FindMiddle(FindMostReserved());
            Above50 = FindAbove50(FindMostReserved());
            TourName = FindMostReserved().Name;
            UpdatePieChart();
        }

        public void yearSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int year = (int)tourDemographics.YearBox.SelectedItem;

            Sub18 = FindSub18(FindMostReservedForYear(year));
            Middle = FindMiddle(FindMostReservedForYear(year));
            Above50 = FindAbove50(FindMostReservedForYear(year));
            TourName = FindMostReservedForYear(year).Name;  
            UpdatePieChart();
        }

        public void yearSelectionChanged_Click(object sender, RoutedEventArgs e)
        {
            int year = (int)tourDemographics.YearBox.SelectedItem;

            Sub18 = FindSub18(FindMostReservedForYear(year));
            Middle = FindMiddle(FindMostReservedForYear(year));
            Above50 = FindAbove50(FindMostReservedForYear(year));
            TourName = FindMostReservedForYear(year).Name;
            UpdatePieChart();
        }


        public Tour FindMostReservedForYear(int i)
        {
            List<TourStartTime> dates = new List<TourStartTime>();
            List<Tour> tours = new List<Tour>();
            tours = TourService.GetInstance().GetAll();
            Tour tour = tours[0];
            int touristNumber = 0;
            int touristNumberTemp = 0;

            foreach (Tour t in tours)
            {
                dates = TourStartTimeService.GetInstance().GetByTourId(t.Id).Where(a => a.Status == "passed" && a.Time.Year == i).ToList();

                foreach (TourStartTime date in dates)
                {
                    foreach (TouristReservation reservation in TourReservationService.GetInstance().GetAll())
                    {
                        if (reservation.Id_TourTime == date.Id)
                        {
                            touristNumberTemp++;
                        }
                    }

                }
                if (touristNumberTemp > touristNumber)
                {
                    touristNumber = touristNumberTemp;
                    tour = t;
                }
                touristNumberTemp = 0;
            }
            return tour;
        }


    }
}
