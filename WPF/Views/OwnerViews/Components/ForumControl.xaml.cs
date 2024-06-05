using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModels.OwnerViewModels.Components;
using BookingApp.WPF.Views.OwnerViews.AnimatorHelpers;
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

namespace BookingApp.WPF.Views.OwnerViews.Components;

public partial class ForumControl : UserControl
{
    private Forum forum;
    private readonly User user;

    public string City => forum.Location.City;
    public string Country => forum.Location.Country;

    public ForumControl(Forum forum, User user)
    {
        this.forum = forum;
        this.user = user;
        InitializeComponent();

        DataContext = this;
        
        HoverAnimation hoverAnimation = new HoverAnimation();
        hoverAnimation.AnimateHover(this.Border);

        if(ForumService.GetInstance().IsSpecialForum(forum))
        {
            this.ImportantLabel.Visibility = Visibility.Visible;
            this.ImportantLabel2.Visibility = Visibility.Hidden;
        } else
        {
            this.ImportantLabel.Visibility = Visibility.Hidden;
            this.ImportantLabel2.Visibility = Visibility.Visible;
        }
    }

    private void CommentClick(object sender, MouseButtonEventArgs e)
    {
        EnteredForumPage enteredForumPage = new EnteredForumPage(forum, user);
        NavigationService.GetNavigationService(this).Navigate(enteredForumPage);
    }
}
