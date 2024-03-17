using BookingApp.Model.MutualModels;
using BookingApp.Model.PathfinderModels;
using BookingApp.Repository.MutualRepositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.View.PathfinderViews
{
    /// <summary>
    /// Interaction logic for CheckpointsView.xaml
    /// </summary>
    public partial class CheckpointsView : Window, INotifyPropertyChanged
    {
       public ObservableCollection<Checkpoint> checkpoints {  get; set; }

       public ObservableCollection<Tourist> tourists { get; set; }

       public ObservableCollection<Tourist> selectedTourists { get; set; }
       public TourRepository tourRepository { get; set; }

       public CheckpointRepository checkpointRepository { get; set; }

       public TouristReservationRepository reservationRepository { get; set; }

       public TouristRepository touristRepository { get; set; }

        public TourStartTimeRepository timeRepository { get; set; }

       public string tourName {  get; set; } 

       public int tourTimeId { get; set; }

        public CheckpointsView(int TourId, DateTime currentDate)
        {
            InitializeComponent();
            DataContext = this;
            tourRepository = new TourRepository();
            touristRepository = new TouristRepository();
            checkpointRepository = new CheckpointRepository();
            reservationRepository = new TouristReservationRepository();
            timeRepository = new TourStartTimeRepository();
            checkpoints = new ObservableCollection<Checkpoint>();
            tourists = new ObservableCollection<Tourist>();
            selectedTourists = new ObservableCollection<Tourist>();
            CheckpointsDataGrid.Loaded += CheckpointsDataGrid_Loaded;
            tourName = "";
            tourTimeId = 0;
            Update(TourId, currentDate);

        }

        public void Update(int TourId, DateTime currentDate)
        {
            tourName = tourRepository.GetById(TourId).Name;
            tourTimeId = timeRepository.GetByTourStartTimeAndId(currentDate,TourId).Id; //gets current time id
            
            foreach (Checkpoint checkpoint in checkpointRepository.GetCheckpointsByTourId(TourId))
            {
                checkpoints.Add(checkpoint);
            }

            //gets all reservations
            foreach (TouristReservation reservation in reservationRepository.GetByTimeId(tourTimeId)) {
                
                Tourist tourist_temp = new Tourist();
                tourist_temp = touristRepository.GetById(reservation.Id_Tourist);
                tourists.Add(tourist_temp);
            
            }



        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CheckpointsDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            // Access the first row
            var row = CheckpointsDataGrid.ItemContainerGenerator.ContainerFromIndex(0) as DataGridRow;
            if (row != null)
            {
                // Access the CheckBox in the first row
                var checkBox = CheckpointsDataGrid.Columns[1].GetCellContent(row) as CheckBox;
                if (checkBox != null)
                {
                    // Set IsChecked to true
                    checkBox.IsChecked = true;
                }
            }
        }

        private void CheckpointCheckboxBox_Checked(object sender, RoutedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            checkBox.IsEnabled = false;
            if (checkBox.IsChecked == true)
            {
                // Transfer selected tourists to another list (or perform desired action)
                foreach (var tourist in selectedTourists.ToList())
                {
                    // Remove the tourist from the tourists collection
                    var item = checkBox.DataContext as Checkpoint;
                    tourists.Remove(tourist);
                    TouristReservation reservationTemp = reservationRepository.GetByTimeId(tourTimeId).First(r => r.Id_Tourist == tourist.Id);
                    reservationTemp.CheckpointId = item.Id;
                    reservationRepository.Update(reservationTemp);
                    // Add transfer logic as per your requirement, e.g., add to another list
                }
                // Clear the selectedTourists list after transfer
                selectedTourists.Clear();
            }
        }

        private void TouristCheckBox_Checked(object sender, RoutedEventArgs e)
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


    }
}
