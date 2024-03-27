using BookingApp.Model.MutualModels;
using BookingApp.Model.OwnerModels;
using BookingApp.Repository.OwnerRepositories;
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

namespace BookingApp.View.OwnerViews
{
    public partial class SettingsAndProfile : Page
    {
        private User _user;
        public User User
        {
            get => _user;
            set
            {
                if(value != _user)
                {
                    _user = value;
                }
            }
        }

        private OwnerInfo _ownerInfo;
        public OwnerInfo OwnerInfo
        {
            get => _ownerInfo;
            set
            {
                if(value != _ownerInfo)
                {
                    _ownerInfo = value;
                }
            }
        }

        public string OwnerType
        {
            get => OwnerInfo.IsSuperOwner ? "SUPER OWNER" : "OWNER";
        }
        public SettingsAndProfile(User user, OwnerInfo ownerInfo)
        {
            User = user;
            OwnerInfo = ownerInfo;
            DataContext = this;
            InitializeComponent();
        }

        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DarkModeRadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void LightModeRadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void BackButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}
