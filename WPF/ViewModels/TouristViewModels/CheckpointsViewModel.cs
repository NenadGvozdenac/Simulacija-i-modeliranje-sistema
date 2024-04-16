using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.Views.TouristViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.TouristViewModels
{
    public class CheckpointsViewModel
    {
        public User user { get; set; }
        public CheckpointRepository checkpointRepository { get; set; }
        public ObservableCollection<Checkpoint> checkpoints { get; set; }

        private ObservableCollection<Checkpoint> _checkpoints;
        public ObservableCollection<Checkpoint> MyCheckpoints
        {
            get { return _checkpoints; }
            set
            {
                if (_checkpoints != value)
                {
                    _checkpoints = value;
                    OnPropertyChanged();
                }
            }
        }
        public int tourId { get; set; }
        public Checkpoints Checkpoints {  get; set; }
        public CheckpointsViewModel(User _user, int _tourId, Checkpoints _checkpoints)
        {
            tourId = _tourId;
            user = _user;
            checkpointRepository = new CheckpointRepository();
            checkpoints = new ObservableCollection<Checkpoint>();
            //Update();
            Checkpoints = _checkpoints;
        }

        public void Update()
        {
            foreach (Checkpoint check in checkpointRepository.GetAll())
            {
                if (check.TourId == tourId)
                {
                    checkpoints.Add(check);
                }
            }
            MyCheckpoints = new ObservableCollection<Checkpoint>(checkpoints);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
