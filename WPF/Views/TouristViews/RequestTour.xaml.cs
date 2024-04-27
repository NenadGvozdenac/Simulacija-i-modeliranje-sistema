using BookingApp.Application.UseCases;
using BookingApp.Model.MutualModels;
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
    /// Interaction logic for RequestTour.xaml
    /// </summary>
    public partial class RequestTour : UserControl, INotifyPropertyChanged
    {
        public RequestTour()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void LoadCountries()
        {
            List<string> listOfCountries = LocationService.GetInstance().GetCountries();
            CountryComboBox.ItemsSource = listOfCountries;
        }
        public void LoadLanguages()
        {
            List<string> listOfLanguages = LanguageService.GetInstance().GetLanguages();
            LanguageComboBox.ItemsSource = listOfLanguages;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
