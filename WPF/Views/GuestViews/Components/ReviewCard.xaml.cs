using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.Repositories;
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

namespace BookingApp.WPF.Views.GuestViews.Components;

/// <summary>
/// Interaction logic for ReviewCard.xaml
/// </summary>
public partial class ReviewCard : UserControl
{
    public ReviewCard()
    {
        InitializeComponent();
    }

    private void SetUpCard(object sender, DependencyPropertyChangedEventArgs e)
    {
        AccommodationReview review = (AccommodationReview)DataContext;
        user_TextBlock.Text = "User : " + OwnerService.GetInstance().GetById(review.UserId).Item2.Username;   //_userRepository.GetById(review.UserId).Username;
        cleanliness_TextBlock.Text = "Cleanliness: " + review.Cleanliness.ToString() + "/5";
        ownerscourtesy_TextBlock.Text = "Owner's courtesy: " + review.OwnersCourtesy.ToString() + "/5";

        if (review.Feedback.Equals(""))
        {
            feedback_TextBlock.Text = "No comment from this user!";
        }
    }
}
