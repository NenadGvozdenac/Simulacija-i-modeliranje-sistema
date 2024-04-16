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
    /// Interaction logic for ReviewCard.xaml
    /// </summary>
    public partial class ReviewCard : UserControl
    {
        public ReviewCard()
        {
            InitializeComponent();
        }

        private void Report_click(object sender, MouseButtonEventArgs e)
        {
            statusTextblock.Text = "Invalid";
            
        }
    }
}
