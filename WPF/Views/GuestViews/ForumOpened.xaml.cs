using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BookingApp.WPF.Views.GuestViews
{
    /// <summary>
    /// Interaction logic for ForumOpened.xaml
    /// </summary>
    public partial class ForumOpened : UserControl
    {

        public Forum forum { get; set; }
        public User User { get; set; }
        public ObservableCollection<ForumComment> comments { get; set; }
        public ForumOpened(int forumidd, User _user)
        {
            InitializeComponent();
            DataContext = this;
            User = _user;
            forum = ForumService.GetInstance().GetById(forumidd);
            comments = new ObservableCollection<ForumComment>();
            LoadForum();
        }

        private void LoadForum()
        {
            if(User.Id == forum.UserId && forum.ForumStatus == BookingApp.Resources.Types.ForumStatus.Open)
            {
                deleteforumButton.Visibility = Visibility.Visible;
            }

            if(forum.ForumStatus == BookingApp.Resources.Types.ForumStatus.Closed)
            {
                messageTextBox.IsEnabled = false;
                forumclosedtextbox.Visibility = Visibility.Visible;
                deleteforumButton.Visibility = Visibility.Hidden;
            }

            Location location = LocationService.GetInstance().GetById(forum.LocationId);
            LocationTextBlock.Text = location.Country + ", " + location.City;

            foreach(ForumComment comment in ForumCommentService.GetInstance().GetByForumId(forum.Id))
            {
                comments.Add(comment);
            }

            scrollviewer.ScrollToBottom();
        }

        private void Send_click(object sender, RoutedEventArgs e)
        {
            if(!String.IsNullOrWhiteSpace(messageTextBox.Text))
            {
                ForumCommentService.GetInstance().Add(new ForumComment(forum.Id,User.Id, messageTextBox.Text));
                comments.Add(new ForumComment(forum.Id, User.Id, messageTextBox.Text));
                messageTextBox.Text = "";
                scrollviewer.ScrollToBottom();
            }
        }

        private void CloseForum_Click(object sender, RoutedEventArgs e)
        {
            ForumService.GetInstance().CloseForum(forum);
            //navigate one page back
            NavigationService.GetNavigationService(this).GoBack();
        }
    }
}
