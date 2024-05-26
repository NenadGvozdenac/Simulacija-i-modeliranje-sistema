using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModels.OwnerViewModels;
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

namespace BookingApp.WPF.Views.OwnerViews;

public partial class ForumsPage : Page
{
    private readonly User user;
    public ForumsViewModel ForumsViewModel { get; set; }

    public ForumsPage(User user)
    {
        InitializeComponent();
        this.user = user;
        ForumsViewModel = new ForumsViewModel(user, this);
        DataContext = ForumsViewModel;
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        ForumsViewModel.AddForums();
    }

    private void BackButton_Click(object sender, MouseButtonEventArgs e)
    {
        if (NavigationService.CanGoBack)
        {
            NavigationService.GoBack();
        }
    }
}
