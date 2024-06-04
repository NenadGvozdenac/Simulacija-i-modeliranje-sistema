using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.View.PathfinderViews;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace BookingApp.WPF.Views.GuideViews.Components
{
    /// <summary>
    /// Interaction logic for TourRequestCard.xaml
    /// </summary>
    public partial class TourRequestCard : UserControl
    {
        public EventHandler<BeginButtonClickedEventArgs> AcceptButtonClicked { get; set; }
        public TourRequestCard()
        {
            InitializeComponent();
        }

        private void AcceptRequest_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to accept this request", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);


            if (result == MessageBoxResult.Yes)
            {
                OnAcceptButtonClicked(new BeginButtonClickedEventArgs(Convert.ToInt32(RequestId_TextBlock.Text), Convert.ToDateTime(StartBlock.Text)));
                int id = Convert.ToInt32(Id_TextBlock.Text);
                AddTourWindow addTourWindow = new AddTourWindow(UserService.GetInstance().GetById(id));
                addTourWindow.addTourWindowViewModel.Country = CountryBlock.Text;
                addTourWindow.addTourWindowViewModel.City = CityBlock.Text;
                addTourWindow.addTourWindowViewModel.Language = LanguageBlock.Text;
                addTourWindow.addTourWindowViewModel.RequestId = Convert.ToInt32(RequestId_TextBlock.Text);
                addTourWindow.addTourWindowViewModel.TouristId = Convert.ToInt32(TouristId_TextBlock.Text);

                TourStartTime startTime = new TourStartTime();
                startTime.Time = Convert.ToDateTime(StartBlock.Text);
                startTime.Guests = 0;
                addTourWindow.addTourWindowViewModel.TourDates.Add(startTime);

                addTourWindow.Show();
            }
        }

        public void OnAcceptButtonClicked(BeginButtonClickedEventArgs e)
        {
            AcceptButtonClicked?.Invoke(this, e);
        }
    }
}
