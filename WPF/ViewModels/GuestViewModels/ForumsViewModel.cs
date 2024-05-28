using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.Resources.Types;
using BookingApp.WPF.Views.GuestViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.GuestViewModels;

public class ForumsViewModel
{
    public  Forums Forums;
    public User _user;

    public ObservableCollection<Forum> _forums { get; set; }

    public ForumsViewModel(Forums forums,User user)
    {
        Forums = forums;
        _user = user;
        _forums = new ObservableCollection<Forum>();

        foreach (var forum in ForumService.GetInstance().GetAll())
        {
            _forums.Add(forum);
        }
    }

    public void StartForum_click()
    {
        if (String.IsNullOrEmpty(Forums.comment_TextBox.Text) || Forums.CountryComboBox.SelectedItem == null || Forums.CityComboBox.SelectedItem == null)
        {
            Forums.alertTextBox.Visibility = System.Windows.Visibility.Visible;
            Forums.successTextBox.Visibility = System.Windows.Visibility.Hidden;
        }
        else
        {
            Forums.alertTextBox.Visibility = System.Windows.Visibility.Hidden;
            Forums.successTextBox.Visibility = System.Windows.Visibility.Hidden;

            int locationId = LocationService.GetInstance().GetLocationByCityAndCountry(Forums.CityComboBox.SelectedItem.ToString(), Forums.CountryComboBox.SelectedItem.ToString()).Id;

            if (ForumService.GetInstance().IsLocationTaken(locationId))
            {
                Forums.usedTextBox.Visibility = System.Windows.Visibility.Visible;
                Forums.successTextBox.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                ForumService.GetInstance().Add(new Forum(_user.Id, LocationService.GetInstance().GetLocationByCityAndCountry(Forums.CityComboBox.SelectedItem.ToString(), Forums.CountryComboBox.SelectedItem.ToString()).Id, ForumStatus.Open));
                ForumCommentService.GetInstance().Add(new ForumComment(ForumService.GetInstance().GetByLocationId(locationId).Id, _user.Id, Forums.comment_TextBox.Text));
                Forums.successTextBox.Visibility = System.Windows.Visibility.Visible;
                Forums.usedTextBox.Visibility = System.Windows.Visibility.Hidden;

                _forums.Clear();
                foreach (var forum in ForumService.GetInstance().GetAll())
                {
                    _forums.Add(forum);
                }
            }

        }
    }

}
