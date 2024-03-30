﻿using System;
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
using System.Windows.Shapes;
using BookingApp.Model.MutualModels;
using BookingApp.Repository;
using BookingApp.Repository.MutualRepositories;

namespace BookingApp.View.GuestViews;


public partial class GuestMainWindow : Window
{
    public EventHandler<int> ReviewClicked;

    private readonly User _user;
    public AccommodationRepository _accommodationRepository { get; set; }
    public AccommodationReservationRepository _accommodationReservationRepository;
    public AccommodationReservationMovingRepository _accommodationReservationMovingRepository;
    public AccommodationReviewRepository _accommodationReviewRepository;
    public ReviewImageRepository _reviewImageRepository;
    public Accommodations AccommodationsUserControl;
    public MyReservations MyReservationsUserControl;
    

    public GuestMainWindow(User user)
    {
        InitializeComponent();       
        _accommodationRepository = new AccommodationRepository();
        _accommodationReservationRepository = AccommodationReservationRepository.GetInstance();
        _accommodationReservationMovingRepository = new AccommodationReservationMovingRepository();
        _accommodationReviewRepository = new AccommodationReviewRepository();
        _reviewImageRepository = new ReviewImageRepository();
        _user = user;
        Update(_user);
        AccommodationsUserControl = new Accommodations(user);
        GuestWindowFrame.Content = AccommodationsUserControl;
    }
    public void Accommodations_Click(object sender, RoutedEventArgs e)
    {
        GuestWindowFrame.Content = AccommodationsUserControl;
    }
    public void MyReservations_Click(object sender, RoutedEventArgs e)
    {
        GuestWindowFrame.Content = MyReservationsUserControl;
    }
    public void ShowAccommodationDetails(int accommodationId)
    {
        Accommodation detailedAccommodation = _accommodationRepository.GetById(accommodationId);
        var a = new AccommodationDetails(detailedAccommodation, _user, _accommodationReviewRepository);
        a.UpcomingReservationsChanged += (sender, e) =>
        {
            RefreshReservations();
        };
        GuestWindowFrame.Content = a;

    }
    private void RefreshReservations()
    {
        MyReservationsUserControl.RefreshUpcomingReservations();
    }

    private void Update(User user)
    {
        AccommodationsUserControl = new Accommodations(user);
        MyReservationsUserControl = new MyReservations(user, _accommodationRepository, _accommodationReservationRepository, _accommodationReservationMovingRepository);
        MyReservationsUserControl.ReviewClicked += ShowReviewPage;
    }

    private void ShowReviewPage(object sender, int reservationId)
    {
        GuestWindowFrame.Content = new ReservationReview(_user, _accommodationReservationRepository, _accommodationRepository, _accommodationReviewRepository, _reviewImageRepository, reservationId);
    }

    private void Logout_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
