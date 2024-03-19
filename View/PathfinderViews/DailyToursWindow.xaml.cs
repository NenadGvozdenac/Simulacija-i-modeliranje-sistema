using BookingApp.Model.MutualModels;
using BookingApp.Model.PathfinderModels;
using BookingApp.Repository.MutualRepositories;
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
using System.Windows.Shapes;

namespace BookingApp.View.PathfinderViews
{
    /// <summary>
    /// Interaction logic for DailyToursWindow.xaml
    /// </summary>
    public partial class DailyToursWindow : Window
    {
        public EventHandler<BeginButtonClickedEventArgs> BeginButtonClickedWindow { get; set; }

        public DailyToursWindow()
        {
            InitializeComponent();
            DailyTours.BeginButtonClickedControl += (s,e)=>DailyTours_SomeEventHandler(s,e);
        }

        private void DailyTours_SomeEventHandler(object sender, BeginButtonClickedEventArgs e)
        {
            OnBeginButtonClicked(new BeginButtonClickedEventArgs(e.TourId,e.StartTime));
        }

        public void OnBeginButtonClicked(BeginButtonClickedEventArgs e)
        {
            BeginButtonClickedWindow?.Invoke(this, e);
            Close();
        }
    }
}
