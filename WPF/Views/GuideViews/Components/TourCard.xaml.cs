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
    /// Interaction logic for TourCard.xaml
    /// </summary>

    public partial class TourCard : UserControl
    {
        public EventHandler<BeginButtonClickedEventArgs> CancelTourClicked { get; set; }
        public TourCard()
        {
            InitializeComponent();
        }

        private void CancelTour_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToDateTime(DateTextBlock.Text) < DateTime.Now.AddHours(48))
            {
                MessageBox.Show("Selected tour cant be canceled (less then 48 hours until tour comences)");
            }
            else {
                OnCancelButtonClicked(new BeginButtonClickedEventArgs(Convert.ToInt32(IdTextBlock.Text), Convert.ToDateTime(DateTextBlock.Text)));
            }
        }


        public void OnCancelButtonClicked(BeginButtonClickedEventArgs e)
        {
            CancelTourClicked?.Invoke(this, e);
        }

        
    }
}
