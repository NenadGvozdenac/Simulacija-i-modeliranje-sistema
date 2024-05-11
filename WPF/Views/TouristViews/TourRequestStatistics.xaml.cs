using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for TourRequestStatistics.xaml
    /// </summary>
    public partial class TourRequestStatistics : UserControl, INotifyPropertyChanged
    {
        public TourRequestStatistics()
        {
            InitializeComponent();
            DataContext = this;
            Update();
        }

        public void Update()
        {
            int acceptedNum = 0;
            int invalidNum = 0;
            double acceptedPercentage = 0;
            double invalidPercentage = 0;

            foreach(TourRequest tourRequest in TourRequestService.GetInstance().GetAll())
            {
                if (tourRequest.Status == "Invalid")
                {
                    invalidNum++;
                }
                if (tourRequest.Status == "Valid")
                {
                    acceptedNum++;
                }
                
            }
            if (acceptedNum != 0)
            {
                acceptedPercentage = 100 * acceptedNum / TourRequestService.GetInstance().GetAll().Count();
                acceptedMessage.Text = acceptedPercentage.ToString() + "%";
            }
            if(invalidNum != 0)
            {
                invalidPercentage = 100 * invalidNum / TourRequestService.GetInstance().GetAll().Count();
                deniedMessage.Text = invalidPercentage.ToString() + "%";
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
