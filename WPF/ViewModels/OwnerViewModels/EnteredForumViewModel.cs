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

public partial class EnteredForumViewModel : ObservableObject
{
    private readonly Forum forum;
    private readonly EnteredForumPage page;
    private readonly User user;

    [ObservableProperty]
    private List<ForumComment> forumComments;

    public string ForumName => forum.Location.ToString();

    [ObservableProperty]
    private string newComment;
    public EnteredForumViewModel(Forum forum, EnteredForumPage page, User user)
    {
        this.forum = forum;
        this.page = page;
        this.user = user;

        PopulateComments();
    }

    private void PopulateComments()
    {
        ForumComments = ForumCommentService.GetInstance().GetByForumId(forum.Id);
        AddCommentsToPage();
    }

    private void AddCommentsToPage()
    {
        page.MainPanel.Children.Clear();

        foreach (var comment in ForumComments)
        {
            if(comment.User.Id == user.Id)
            {
                ForumCommentControlMe forumCommentControlMe = new ForumCommentControlMe(comment);
                page.MainPanel.Children.Add(forumCommentControlMe);
                continue;
            }

            ForumCommentControl forumCommentControl = new ForumCommentControl(comment);
            page.MainPanel.Children.Add(forumCommentControl);
        }

        page.ScrollViewerMainPanel.ScrollToBottom();
    }

    public void SendMessage()
    {
        if (string.IsNullOrWhiteSpace(NewComment))
        {
            return;
        }

        ForumComment newComment = new ForumComment
        {
            Content = NewComment,
            CreationDateTime = DateTime.Now,
            Forum = forum,
            ForumId = forum.Id,
            User = user,
            UserId = user.Id
        };

        ForumCommentService.GetInstance().Add(newComment);
        PopulateComments();
        NewComment = string.Empty;
    }
}
