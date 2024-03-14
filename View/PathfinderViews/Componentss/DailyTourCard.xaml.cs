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
    /// Interaction logic for DailyTourCard.xaml
    /// </summary>
    public partial class DailyTourCard : UserControl
    {

        

        public DailyTourCard()
        {
            InitializeComponent();
        }

        public void BeginButton_Click(object sender, RoutedEventArgs e)
        {
            CheckpointsView checkpointsView = new CheckpointsView(Convert.ToInt32(IdTextBlock.Text));
            checkpointsView.ShowDialog();
        }

       


    }
}
