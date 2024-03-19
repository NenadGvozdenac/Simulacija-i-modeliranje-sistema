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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.View.PathfinderViews.Componentss
{
    /// <summary>
    /// Interaction logic for DailyTourCard.xaml
    /// </summary>
    public partial class DailyTourCard : UserControl
    {
        public EventHandler<BeginButtonClickedEventArgs> BeginButtonClicked { get; set; }

        public TourStartTimeRepository _timeRepository {  get; set; }

        public DailyTourCard()
        {
            InitializeComponent();
            _timeRepository = new TourStartTimeRepository();
        }

        public void BeginButton_Click(object sender, RoutedEventArgs e)
        {
            OnBeginButtonClicked(new BeginButtonClickedEventArgs(Convert.ToInt32(IdTextBlock.Text), Convert.ToDateTime(DateTextBlock.Text)));
            CheckpointsView checkpointsView = new CheckpointsView(Convert.ToInt32(IdTextBlock.Text), Convert.ToDateTime(DateTextBlock.Text));
            checkpointsView.ShowDialog();
        }    

       

        public void OnBeginButtonClicked(BeginButtonClickedEventArgs e)
        {
            BeginButtonClicked?.Invoke(this, e);
        }


    }
}
