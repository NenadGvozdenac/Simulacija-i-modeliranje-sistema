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
using LiveCharts;
using LiveCharts.Wpf;

namespace BookingApp.WPF.Views.TouristViews
{
    /// <summary>
    /// Interaction logic for TourRequestStatistics.xaml
    /// </summary>
    public partial class TourRequestStatistics : UserControl, INotifyPropertyChanged
    {
        public SeriesCollection SeriesCollection {  get; set; }
        public string[] Labels {  get; set; }
        public string[] LocationLabels {  get; set; }
        public Func<double, string> LocationValues { get; set; }
        public Func<double, string> Values { get; set; }
        public SeriesCollection SeriesCollectionLocation {  get; set; }
        public TourRequestStatistics()
        {
            InitializeComponent();
            DataContext = this;
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title="Requests",
                    Values = new ChartValues<int>()
                }
            };

            Labels = GetLanguages();
            Values = value => value.ToString("N");

            GetTourRequestNumber();

            SeriesCollectionLocation = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title="Requests",
                    Values = new ChartValues<int>()
                }
            };
            LocationLabels = GetLocations();
            LocationValues = value => value.ToString("N");
            GetTourRequestNumberForLocation();

            GetPercentages();
            PopulateYearsComboBox();
            GetAverageNumOfGuests();
        }

        public void GetTourRequestNumber()
        {
            var requestCountsByLanguage = new Dictionary<string, int>(); // Rječnik za pohranu broja zahtjeva po jeziku

            foreach (TourRequest tr in TourRequestService.GetInstance().GetAll())
            {
                var languageName = tr.Language.Name;
                // Ako jezik nije već u rječniku, dodajemo ga i postavljamo brojač na 1
                if (!requestCountsByLanguage.ContainsKey(languageName))
                {
                    requestCountsByLanguage[languageName] = 1;
                }
                else
                {
                    // Inače, povećavamo broj za 1
                    requestCountsByLanguage[languageName]++;
                }
            }
            // Popunjavamo Y os sa brojem zahtjeva za svaki jezik
            foreach (var count in requestCountsByLanguage.Values)
            {
                ((ChartValues<int>)SeriesCollection[0].Values).Add(count); // Dodajemo broj zahtjeva u vrijednosti grafikona
            }
        }
        public void GetTourRequestNumberForLocation()
        {
            var requestCountsByLocation = new Dictionary<string, int>(); // Rječnik za pohranu broja zahtjeva po jeziku

            foreach (TourRequest tr in TourRequestService.GetInstance().GetAll())
            {
                var location = tr.Location.Country;
                // Ako jezik nije već u rječniku, dodajemo ga i postavljamo brojač na 1
                if (!requestCountsByLocation.ContainsKey(location))
                {
                    requestCountsByLocation[location] = 1;
                }
                else
                {
                    // Inače, povećavamo broj za 1
                    requestCountsByLocation[location]++;
                }
            }
            // Popunjavamo Y os sa brojem zahtjeva za svaki jezik
            foreach (var count in requestCountsByLocation.Values)
            {
                ((ChartValues<int>)SeriesCollectionLocation[0].Values).Add(count); // Dodajemo broj zahtjeva u vrijednosti grafikona
            }
        }

        public string[] GetLocations()
        {
            var locations = new List<string>();
            foreach (TourRequest tr in TourRequestService.GetInstance().GetAll())
            {
                foreach (Location l in LocationService.GetInstance().GetAll())
                {
                    if (tr.Location.Country == l.Country && !locations.Contains(l.Country))
                    {
                        locations.Add(l.Country);
                    }
                }
            }
            return locations.ToArray();
        }
        public string[] GetLanguages()
        {
            var languages = new List<string>();

            foreach (TourRequest tr in TourRequestService.GetInstance().GetAll())
            {
                foreach (Language l in LanguageService.GetInstance().GetAll())
                {
                    if(tr.LanguageId == l.Id)
                    {
                        languages.Add(l.Name);
                    }
                }
            }
            
            return languages.ToArray();
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
