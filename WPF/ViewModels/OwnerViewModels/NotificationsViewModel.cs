using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.WPF.Views.OwnerViews;
using BookingApp.WPF.Views.OwnerViews.Components;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.OwnerViewModels;

public partial class NotificationsViewModel : ObservableObject
{
    private User user;
    private readonly NotificationsPage page;
    [ObservableProperty]
    private List<Notification> notifications;
    public NotificationsViewModel(User user, NotificationsPage page)
    {
        this.user = user;
        this.page = page;
        AddNotifications();
    }

    public void AddNotifications()
    {
        Notifications = OwnerService.GetInstance().GetNotificationsForOwner(user.Id);
        AddNotificationControls();
    }

    private void AddNotificationControls()
    {
        page.MainPanel.Children.Clear();
        foreach (var notification in Notifications)
        {
            var notificationControl = new NotificationControl(notification);
            notificationControl.Margin = new System.Windows.Thickness(0, 5, 0, 5);

            if(notification is Notification<Forum>)
            {
                var forum = (notification as Notification<Forum>).Value;
                notificationControl.Border.MouseDown += (s, e) => NavigationService.GetNavigationService(page).Navigate(new EnteredForumPage(forum, this.user));
            } else if(notification is Notification<GuestRating>)
            {
                var guestRating = (notification as Notification<GuestRating>).Value;
                notificationControl.Border.MouseDown += (s, e) => NavigationService.GetNavigationService(page).Navigate(new AddGuestRatingPage(guestRating));
            }

            page.MainPanel.Children.Add(notificationControl);
        }
    }
}
