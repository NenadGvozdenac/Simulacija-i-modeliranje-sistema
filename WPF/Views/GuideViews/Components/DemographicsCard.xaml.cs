using BookingApp.Domain.Models;
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

namespace BookingApp.View.PathfinderViews.Componentss
{
    /// <summary>
    /// Interaction logic for DemographicsCard.xaml
    /// </summary>
    public partial class DemographicsCard : UserControl
    {
        public EventHandler<BeginButtonClickedEventArgs> TourStatsClicked { get; set; }
        public DemographicsCard()
        {
            InitializeComponent();
        }


        private void TourStats_Click(object sender, RoutedEventArgs e)
        {
          OnStatsButtonClicked(new BeginButtonClickedEventArgs(Convert.ToInt32(IdTextBlock.Text), Convert.ToDateTime(DateTextBlock.Text)));
        }


        public void OnStatsButtonClicked(BeginButtonClickedEventArgs e)
        {
            TourStatsClicked?.Invoke(this, e);
        }

    }
}
