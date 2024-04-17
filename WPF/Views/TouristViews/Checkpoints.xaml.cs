using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.ViewModels.TouristViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.Views.TouristViews
{
    /// <summary>
    /// Interaction logic for Checkpoints.xaml
    /// </summary>
    public partial class Checkpoints : UserControl, INotifyPropertyChanged
    {
        public CheckpointsViewModel checkpointsView {  get; set; }
        public Checkpoints(User _user, int _tourId)
        {
            InitializeComponent();
            checkpointsView = new CheckpointsViewModel(_user, _tourId, this);
            DataContext = checkpointsView;
            Update();
        }

        public void Update()
        {
            checkpointsView.Update();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
