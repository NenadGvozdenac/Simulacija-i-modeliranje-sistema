using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.Resources.Types;
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

namespace BookingApp.WPF.Views.GuestViews.Components
{
    /// <summary>
    /// Interaction logic for ForumCommentCard.xaml
    /// </summary>
    public partial class ForumCommentCard : UserControl
    {
        public ForumCommentCard()
        {
            InitializeComponent();
        }

        private void UpdateCard(object sender, DependencyPropertyChangedEventArgs e)
        {
            ForumComment forumcomment = (ForumComment)DataContext;
            User user = UserService.GetInstance().GetById(forumcomment.UserId);
            UsernameTextBlock.Text = user.Username;
            Forum forum = ForumService.GetInstance().GetById(forumcomment.ForumId);
            CheckIfVisited(forum,user);

        }

        private void CheckIfVisited(Forum forum,User user)
        {
            //checks to see if the user has visited this location before
           if(user.Type == UserType.Owner)
            {
                CheckedPlaceTextBlock.Text = "This user is an owner at this location";
                CheckedPlaceTextBlock.Foreground = Brushes.Blue;
                return;
            }

            if (ForumService.GetInstance().IsVisitedByUser(forum, user.Id))
            {
                CheckedPlaceTextBlock.Text = "This user has visited this place before";
                CheckedPlaceTextBlock.Foreground = Brushes.Green;
            }
            else
            {
                CheckedPlaceTextBlock.Text = "This user has not visited this place before";
                CheckedPlaceTextBlock.Foreground = Brushes.Red;
            }
        }
    }
}
