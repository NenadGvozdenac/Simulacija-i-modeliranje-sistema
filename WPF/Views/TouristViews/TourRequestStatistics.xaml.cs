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
            GetPercentages();
            PopulateYearsComboBox();
            GetAverageNumOfGuests();
        }

        public void GetAverageNumOfGuests()
        {
            int numOfGuests=0;
            int numOfAcceptedRequests = 0;
            foreach(TourRequest tourRequest in TourRequestService.GetInstance().GetAll())
            {
                if (tourRequest.Status == "Valid")
                {
                    foreach(RequestedTourist rt in RequestedTouristService.GetInstance().GetAll())
                    {
                        if(rt.RequestId == tourRequest.Id)
                        {
                            numOfGuests++;
                        }
                    }
                    //numOfGuests += tourRequest.Tourists.Count();
                    numOfAcceptedRequests++;
                }
            }
            if( numOfAcceptedRequests != 0 ) { 
                averageMessage.Text = ((double)numOfGuests / numOfAcceptedRequests).ToString();
            }else
            {
                averageMessage.Text = "0";
            }
        }
        public void GetAverageNumOfGuestsForYear(int year)
        {
            int numOfGuests = 0;
            int numOfAcceptedRequests = 0;
            foreach (TourRequest tourRequest in TourRequestService.GetInstance().GetAll())
            {
                if (tourRequest.BeginDate.Year == year)
                {
                    if (tourRequest.Status == "Valid")
                    {
                        foreach (RequestedTourist rt in RequestedTouristService.GetInstance().GetAll())
                        {
                            if (rt.RequestId == tourRequest.Id)
                            {
                                numOfGuests++;
                            }
                        }
                        numOfAcceptedRequests++;
                    }
                }
            }
            if (numOfAcceptedRequests != 0)
            {
                averageMessage.Text = ((double)numOfGuests / numOfAcceptedRequests).ToString();
            }else
            {
                averageMessage.Text = "0";
            }
        }
        public void ComboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (yearsComboBox2.SelectedItem != "All time")
            {
                int selectedYear = (int)yearsComboBox2.SelectedItem;
                GetAverageNumOfGuestsForYear(selectedYear);
            }
            else
            {
                GetAverageNumOfGuests();
            }
        }
        public void GetPercentages()
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

        public void PopulateYearsComboBox()
        {
            yearsComboBox.Items.Clear();
            List<int> years = GetYears();
            List<int> validYears=GetYearsWithValidRequests();
            foreach (int year in years)
            {
                yearsComboBox.Items.Add(year);
            }
            foreach(int year in validYears)
            {
                yearsComboBox2.Items.Add(year);
            }
            yearsComboBox.Items.Add("All time");
            yearsComboBox2.Items.Add("All time");
        }
        public List<int> GetYears()
        {
            List<int> years = new List<int>();
            foreach (TourRequest tourRequest in TourRequestService.GetInstance().GetAll())
            {
                if (!years.Contains(tourRequest.BeginDate.Year))
                {
                    years.Add(tourRequest.BeginDate.Year);
                }
            }
            return years;
        }
        public List<int> GetYearsWithValidRequests()
        {
            List<int> years = new List<int>();
            foreach (TourRequest tourRequest in TourRequestService.GetInstance().GetAll())
            {
                if (tourRequest.Status == "Valid")
                {
                    if (!years.Contains(tourRequest.BeginDate.Year))
                    {
                        years.Add(tourRequest.BeginDate.Year);
                    }
                }
            }
            return years;
        }
        public void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (yearsComboBox.SelectedItem != "All time")
            {
                int selectedYear = (int)yearsComboBox.SelectedItem;
                GetPercentagesForYear(selectedYear);
            }else
            {
                GetPercentages();
            }
        }

        public void GetPercentagesForYear(int year)
        {
            int acceptedNum = 0;
            int invalidNum = 0;
            double acceptedPercentage = 0;
            double invalidPercentage = 0;

            foreach (TourRequest tourRequest in TourRequestService.GetInstance().GetAll())
            {
                if (tourRequest.BeginDate.Year == year)
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
            }
            if (acceptedNum != 0)
            {
                acceptedPercentage = 100 * acceptedNum / TourRequestService.GetInstance().GetByYear(year).Count();
                acceptedMessage.Text = acceptedPercentage.ToString() + "%";
            }else
            {
                acceptedMessage.Text = "0%";
            }
            if (invalidNum != 0)
            {
                invalidPercentage = 100 * invalidNum / TourRequestService.GetInstance().GetByYear(year).Count();
                deniedMessage.Text = invalidPercentage.ToString() + "%";
            }
            else
            {
                deniedMessage.Text = "0%";
            }

        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
