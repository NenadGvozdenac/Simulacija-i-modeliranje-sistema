using BookingApp.Domain.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.Views.GuideViews
{
    /// <summary>
    /// Interaction logic for ConplexRequestsControl.xaml
    /// </summary>
    public partial class ConplexRequestsControl : UserControl
    {
        public ComplexRequestControlViewModel complexRequestControlViewModel { get; set; }

        public User user {  get; set; }

        public ConplexRequestsControl(User _user)
        {
            InitializeComponent();
            user = _user;
            complexRequestControlViewModel = new ComplexRequestControlViewModel(this, user);
            DataContext = complexRequestControlViewModel;
        }
        
    }
}
