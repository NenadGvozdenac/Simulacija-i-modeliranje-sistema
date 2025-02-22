﻿using BookingApp.Domain.Models;
using BookingApp.Model.MutualModels;
using BookingApp.Repositories;
using BookingApp.WPF.Views.TouristViews;
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

namespace BookingApp.WPF.Views.TouristViews.Components
{
    /// <summary>
    /// Interaction logic for TourCardWithStar.xaml
    /// </summary>
    public partial class TourCardWithStar : UserControl
    {
        public User _user {  get; set; }

        public event EventHandler RateTourClicked;

        public TourCardWithStar()
        {
            InitializeComponent();

            
        }
        private void RateTheTour_Click(object sender, MouseButtonEventArgs e)
        {
                if (this.DataContext is Tour tour)
                {
                    int tourId = tour.Id;

                    Window parentWindow = Window.GetWindow(this);

                    if (parentWindow is TouristMainWindow mainWindow)
                    {
                        mainWindow.RateTour(_user, tourId);
                    }
                }
        }
        public void SetUser(User user)
        {
            _user = user;
        }
    }
}
