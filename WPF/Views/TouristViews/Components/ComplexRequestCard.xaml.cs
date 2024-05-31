using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
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

namespace BookingApp.WPF.Views.TouristViews.Components
{
    /// <summary>
    /// Interaction logic for ComplexRequestCard.xaml
    /// </summary>
    public partial class ComplexRequestCard : UserControl
    {
        public ObservableCollection<TourRequest> requests {  get; set; }

        private ObservableCollection<TourRequest> _myRequests;
        public ObservableCollection<TourRequest> MyRequests
        {
            get { return _myRequests; }
            set
            {
                if (_myRequests != value)
                {
                    _myRequests = value;
                    OnPropertyChanged();
                }
            }
        }
        public ComplexRequestCard()
        {
            InitializeComponent();
            requests = new ObservableCollection<TourRequest>();
            Update();
        }
        
        public void Update()
        {
            foreach(TourRequest tr in TourRequestService.GetInstance().GetAll())
            {
                tr.Language = LanguageService.GetInstance().GetById(tr.LanguageId);
                tr.Location = LocationService.GetInstance().GetById(tr.LocationId);
                requests.Add(tr);
            }
            MyRequests = new ObservableCollection<TourRequest>(requests);
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
