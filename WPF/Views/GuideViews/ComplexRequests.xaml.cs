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
using System.Windows.Shapes;

namespace BookingApp.WPF.Views.GuideViews
{
    /// <summary>
    /// Interaction logic for ComplexRequests.xaml
    /// </summary>
    public partial class ComplexRequests : Window
    {
        public ComplexRequestsViewModel complexRequestsViewModel { get; set; }

        public User user {  get; set; }

        public ComplexRequests(User _user)
        {
            InitializeComponent();
            user = _user;
            complexRequestsViewModel = new ComplexRequestsViewModel(this, user);
            DataContext = complexRequestsViewModel;
        }






    }
}
