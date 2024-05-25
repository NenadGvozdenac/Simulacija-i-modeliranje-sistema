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
public partial class EnteredForumPage : Page
{
    private readonly Forum forum;
    private readonly User user;

    public EnteredForumViewModel ViewModel { get; set; }

    public EnteredForumPage(Forum forum, User user)
    {
        InitializeComponent();
        this.forum = forum;
        this.user = user;
        ViewModel = new EnteredForumViewModel(forum, this, user);
        DataContext = ViewModel;
    }

    private void BackButton_Click(object sender, MouseButtonEventArgs e)
    {
        if(NavigationService.CanGoBack)
        {
            NavigationService.GoBack();
        }
    }

    private void SendButton_Click(object sender, RoutedEventArgs e)
    {
        ViewModel.SendMessage();
    }
}
