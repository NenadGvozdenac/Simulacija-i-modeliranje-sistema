using BookingApp.Domain.Models;
using BookingApp.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BookingApp.View.PathfinderViews;
using BookingApp.Application.UseCases;

namespace BookingApp.WPF.ViewModels.GuideViewModels
{
    public class CheckpointsViewModel
    {
        public ObservableCollection<Checkpoint> checkpoints { get; set; }

        public ObservableCollection<Tourist> tourists { get; set; }

        public ObservableCollection<Tourist> selectedTourists { get; set; }
        public TourRepository tourRepository { get; set; }

       

        public TouristReservationRepository reservationRepository { get; set; }

        public TouristRepository touristRepository { get; set; }

        

        public EventHandler<BeginButtonClickedEventArgs> EndButtonClicked { get; set; }

        public EventHandler<BeginButtonClickedEventArgs> EndButtonClickedMain { get; set; }

        public string tourName { get; set; }

        public int tourTimeId { get; set; }

        public int tourId { get; set; }

        public DateTime _currentDate { get; set; }

        CheckpointsView checkpointsView { get; set; }

        public CheckpointsViewModel(CheckpointsView checkpointViews,int TourId, DateTime currentDate)
        {
            checkpointsView = checkpointViews;
            tourRepository = new TourRepository();
            touristRepository = new TouristRepository();
            reservationRepository = new TouristReservationRepository();           
            checkpoints = new ObservableCollection<Checkpoint>();
            tourists = new ObservableCollection<Tourist>();
            selectedTourists = new ObservableCollection<Tourist>();
            checkpointsView.CheckpointsDataGrid.Loaded += CheckpointsDataGrid_Loaded;
            tourName = "";
            tourTimeId = 0;
            tourId = TourId;
            _currentDate = currentDate;
            Update(TourId, currentDate);

        }

        public void Update(int TourId, DateTime currentDate)
        {
            tourName = tourRepository.GetById(TourId).Name;
            tourTimeId = TourStartTimeService.GetInstance().GetByTourStartTimeAndId(currentDate, TourId).Id; //gets current time id

            foreach (Checkpoint checkpoint in CheckpointService.GetInstance().GetCheckpointsByTourId(TourId))
            {
                checkpoints.Add(checkpoint);
            }

            //gets all reservations
            foreach (TouristReservation reservation in reservationRepository.GetByTimeId(tourTimeId))
            {

                Tourist tourist_temp = new Tourist();
                tourist_temp = touristRepository.GetById(reservation.Id_Tourist);
                if (reservation.CheckpointId == -1)
                {
                    tourists.Add(tourist_temp);
                }

            }



        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void CheckpointsDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            // Access the first row
            var row = checkpointsView.CheckpointsDataGrid.ItemContainerGenerator.ContainerFromIndex(0) as DataGridRow;
            if (row != null)
            {
                // Access the CheckBox in the first row
                var checkBox = checkpointsView.CheckpointsDataGrid.Columns[1].GetCellContent(row) as CheckBox;
                if (checkBox != null)
                {
                    // Set IsChecked to true
                    checkBox.IsChecked = true;
                }
            }


        }

        public void CheckpointCheckboxBox_Checked(object sender, RoutedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            var item1 = checkBox.DataContext as Checkpoint;

            if (item1.Checked == true)
            {
                checkBox.IsChecked = true;
            }

            if (checkBox.IsChecked == true)
            {
                item1.Checked = true;
                CheckpointService.GetInstance().Update(item1);
                TourStartTime timeTemp = TourStartTimeService.GetInstance().GetByTourStartTimeAndId(_currentDate, tourId);
                timeTemp.CurrentCheckpoint = item1.Id;
                TourStartTimeService.GetInstance().Update(timeTemp);

                foreach (var tourist in selectedTourists.ToList())
                {
                    tourists.Remove(tourist);
                    TouristReservation reservationTemp = reservationRepository.GetByTimeId(tourTimeId).First(r => r.Id_Tourist == tourist.Id);
                    reservationTemp.CheckpointId = item1.Id;
                    reservationRepository.Update(reservationTemp);

                }
                selectedTourists.Clear();

                if (CheckIfLast(checkBox) == 1)
                {
                    OnEndButtonClicked(new BeginButtonClickedEventArgs(tourId, _currentDate));
                    OnEndButtonClickedMain(new BeginButtonClickedEventArgs(tourId, _currentDate));
                    foreach (Checkpoint checkpoint in checkpoints)
                    {
                        checkpoint.Checked = false;
                        CheckpointService.GetInstance().Update(checkpoint);
                    }
                    checkpointsView.Close();
                }
            }
        }


        public int CheckIfLast(CheckBox checkbox)
        {
            Checkpoint checkpoint = checkbox.DataContext as Checkpoint;

            if (checkpoints.Last() == checkpoint)
            {
                return 1;
            }
            return 0;
        }

        public int GoBackToCheckbox(CheckBox checkbox)
        {
            Checkpoint checkpoint = checkbox.DataContext as Checkpoint;
            TourStartTime timeTemp = TourStartTimeService.GetInstance().GetByTourStartTimeAndId(_currentDate, tourId);
            if (checkpoint.Id == timeTemp.CurrentCheckpoint)
            {
                return 1;
            }
            return 0;
        }

        public void TouristCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            if (checkBox.IsChecked == true)
            {
                // If the checkbox is checked, add the corresponding tourist to the selectedTourists list
                var tourist = checkBox.DataContext as Tourist;
                selectedTourists.Add(tourist);
            }
            else
            {
                // If the checkbox is unchecked, remove the corresponding tourist from the selectedTourists list
                var tourist = checkBox.DataContext as Tourist;
                selectedTourists.Remove(tourist);
            }
        }

        public void EndTour_Click(object sender, RoutedEventArgs e)
        {
            OnEndButtonClicked(new BeginButtonClickedEventArgs(tourId, _currentDate));
            OnEndButtonClickedMain(new BeginButtonClickedEventArgs(tourId, _currentDate));
            checkpointsView.Close();
        }


        public void OnEndButtonClicked(BeginButtonClickedEventArgs e)
        {
            EndButtonClicked?.Invoke(this, e);
        }

        public void OnEndButtonClickedMain(BeginButtonClickedEventArgs e)
        {
            EndButtonClickedMain?.Invoke(this, e);
        }



    }
}
