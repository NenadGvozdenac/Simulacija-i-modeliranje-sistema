using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using Org.BouncyCastle.Asn1.Ocsp;
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
    /// Interaction logic for ComplexTourRequests.xaml
    /// </summary>
    public partial class ComplexTourRequests : UserControl
    {
        public User user {  get; set; }
        public ObservableCollection<ComplexTourRequest> requests { get; set; }

        private ObservableCollection<ComplexTourRequest> _myRequests;
        public ObservableCollection<ComplexTourRequest> MyRequests
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
        public ComplexTourRequests(User _user)
        {
            InitializeComponent();
            DataContext = this;
            user = _user;
            requests = new ObservableCollection<ComplexTourRequest>();
            Update();
        }

        public void Update()
        {
            requests.Clear();
            foreach(ComplexTourRequest cr in ComplexTourRequestService.GetInstance().GetAll())
            {
                cr.Status = "Valid";
                foreach (TourRequest tr in TourRequestService.GetInstance().GetAll())
                {
                    if (cr.Id == tr.ComplexRequestId)
                    {
                        if(tr.Status == "Pending")
                        {
                            cr.Status = "Pending";
                        }

                    }
                }
                requests.Add(cr);
            }
            MyRequests = new ObservableCollection<ComplexTourRequest>(requests);
        }
        public void UpdateStatus()
        {
        }
        public void Add_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);

            if (parentWindow is TouristMainWindow mainWindow)
            {
                mainWindow.ShowComplexTourRequest(user);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
