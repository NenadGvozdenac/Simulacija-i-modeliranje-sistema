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
        public TourRequestCard()
        {
            InitializeComponent();
        }

        private void AcceptRequest_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(Id_TextBlock.Text);
            AddTourWindow addTourWindow = new AddTourWindow(UserService.GetInstance().GetById(id));
            addTourWindow.addTourWindowViewModel.Country = CountryBlock.Text;
            addTourWindow.addTourWindowViewModel.City = CityBlock.Text;
            addTourWindow.addTourWindowViewModel.Language = LanguageBlock.Text;



            TourStartTime startTime = new TourStartTime();
            startTime.Time = Convert.ToDateTime(StartBlock.Text);
            startTime.Guests = 0;
            addTourWindow.addTourWindowViewModel.TourDates.Add(startTime);

            addTourWindow.Show();
        }
    }
}
