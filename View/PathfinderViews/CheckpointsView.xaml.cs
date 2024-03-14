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
       public TourRepository tourRepository { get; set; }

       public CheckpointRepository checkpointRepository { get; set; }

       public string tourName {  get; set; } 

       

        public CheckpointsView(int TourId)
        {
            InitializeComponent();
            DataContext = this;
            tourRepository = new TourRepository();
            checkpointRepository = new CheckpointRepository();
            checkpoints = new ObservableCollection<Checkpoint>();
            CheckpointsDataGrid.Loaded += CheckpointsDataGrid_Loaded;
            tourName = "";
            Update(TourId);

        }

        public void Update(int TourId)
        {
            tourName = tourRepository.GetById(TourId).Name;
            
            foreach (Checkpoint checkpoint in checkpointRepository.GetCheckpointsByTourId(TourId))
            {
                checkpoints.Add(checkpoint);
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

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            checkBox.IsEnabled = false;
        }




    }
}
