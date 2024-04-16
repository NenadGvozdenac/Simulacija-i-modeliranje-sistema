using BookingApp.Domain.Models;
using BookingApp.View.PathfinderViews;
using BookingApp.WPF.ViewModels.GuideViewModels;
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

namespace BookingApp.WPF.Views.GuideViews
{
    /// <summary>
    /// Interaction logic for ReviewsWindow.xaml
    /// </summary>
    public partial class ReviewsWindow : Window
    {
        public ReviewsWindowViewModel reviewsWindowViewModel { get; set; }
        public ReviewsWindow(User user)
        {
            InitializeComponent();
            reviewsWindowViewModel = new ReviewsWindowViewModel(this,user);
            
        }
    }
}
