using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
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
    /// Interaction logic for ForumCard.xaml
    /// </summary>
    public partial class ForumCard : UserControl
    {
        public ForumCard()
        {
            InitializeComponent();
        }

        private void SeeMore_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is Forum forum)
            {
                int forumId = forum.Id;

                Window parentWindow = Window.GetWindow(this);

                if (parentWindow is GuestMainWindow mainWindow)
                {
                    mainWindow.GuestViewModel.ShowForumPage(forumId);
                }
            }
        }

        private void UpdateCard(object sender, DependencyPropertyChangedEventArgs e)
        {

            Forum forum = (Forum)DataContext;

            Location location = LocationService.GetInstance().GetById(forum.LocationId);

            LocationTextBlock.Text = location.Country + ", " + location.City;

            if (!ForumService.GetInstance().IsSpecialForum(forum))
            {
                crownImage.Visibility = Visibility.Hidden;
            }
            if(forum.ForumStatus != BookingApp.Resources.Types.ForumStatus.Closed)
            {
                closedTextBlock.Visibility = Visibility.Hidden;
            }

        }
    }
}
