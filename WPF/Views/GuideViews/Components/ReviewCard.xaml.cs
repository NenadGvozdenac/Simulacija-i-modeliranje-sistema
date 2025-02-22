﻿using BookingApp.Application.UseCases;
using BookingApp.Model.MutualModels;
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

namespace BookingApp.WPF.Views.GuideViews.Components
{
    /// <summary>
    /// Interaction logic for ReviewCard.xaml
    /// </summary>
    public partial class ReviewCard : UserControl
    {
        public ReviewCard()
        {
            InitializeComponent();
        }

        private void Report_click(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to report account?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                statusTextblock.Text = "Invalid";
                TourReview review = TourReviewService.GetInstance().GetById(int.Parse(idTextblock.Text));
                review.Status = "Invalid";
                TourReviewService.GetInstance().Update(review);
            }
            
        }
    }
}
