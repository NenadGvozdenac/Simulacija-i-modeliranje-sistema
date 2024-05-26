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

namespace BookingApp.WPF.ViewModels.OwnerViewModels;

public partial class ForumsViewModel : ObservableObject
{
    private readonly User user;
    private readonly ForumsPage page;

    [ObservableProperty]
    private List<Forum> forums;

    public ForumsViewModel(User user, ForumsPage page)
    {
        this.user = user;
        this.page = page;
        AddForums();
    }

    public void AddForums()
    {
        Forums = ForumService.GetInstance().GetForumsByOwnerId(this.user.Id);
        AddForumControls();
    }

    private void AddForumControls()
    {
        page.MainPanel.Children.Clear();

        foreach (Forum forum in Forums)
        {
            ForumControl forumControl = new ForumControl(forum, user);
            forumControl.Margin = new System.Windows.Thickness(0, 5, 0, 5);
            page.MainPanel.Children.Add(forumControl);
        }
    }
}
