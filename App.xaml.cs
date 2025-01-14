﻿using BookingApp.Application.Localization;
using BookingApp.Application.UseCases;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repositories;
using BookingApp.View;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Wpf;
using System;
using System.Collections.Generic;
using System.Windows;

namespace BookingApp;

public partial class App
{
    private static IServiceProvider _serviceProvider;

    public static IServiceProvider ServiceProvider => _serviceProvider;

    public static Dictionary<string, string> Languages = new Dictionary<string, string>
    {
        { "English", "en-US" },
        { "Serbian", "sr-RS" }
    };

    private void Application_Startup(object sender, StartupEventArgs e)
    {
        IServiceCollection services = new ServiceCollection();

        ConfigureRepositories(services);
        ConfigureServices(services);
        ConfigureWindows(services);

        _serviceProvider = services.BuildServiceProvider();

        var signInForm = _serviceProvider.GetRequiredService<SignInForm>();
        signInForm.Show();
    }

    public void ChangeLanguage(string lang)
    {
        TranslationSource.Instance.CurrentCulture = new System.Globalization.CultureInfo(Languages[lang]);
    }

    private void ConfigureWindows(IServiceCollection services)
    {
        // Register the form
        services.AddTransient<SignInForm>();
    }

    private void ConfigureRepositories(IServiceCollection services)
    {
        // Add repositories
        services.AddSingleton<IAccommodationRepository, AccommodationRepository>();
        services.AddSingleton<IAccommodationReservationRepository, AccommodationReservationRepository>();
        services.AddSingleton<ILocationRepository, LocationRepository>();
        services.AddSingleton<IOwnerInfoRepository, OwnerInfoRepository>();
        services.AddSingleton<IGuestInfoRepository, GuestInfoRepository>();
        services.AddSingleton<IGuestRatingRepository, GuestRatingRepository>();
        services.AddSingleton<IAccommodationReservationMovingRepository, AccommodationReservationMovingRepository>();
        services.AddSingleton<IAccommodationImageRepository, AccommodationImageRepository>();
        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<IAccommodationReviewRepository, AccommodationReviewRepository>();
        services.AddSingleton<IReviewImageRepository, ReviewImageRepository>();
        services.AddSingleton<ITourRepository, TourRepository>();
        services.AddSingleton<ITourStartTimeRepository, TourStartTimeRepository>();
        services.AddSingleton<ICheckpointRepository, CheckpointRepository>();
        services.AddSingleton<ITourVoucherRepository, TourVoucherRepository>();
        services.AddSingleton<ITourImageRepository, TourImageRepository>();
        services.AddSingleton<ILanguageRepository, LanguageRepository>();
        services.AddSingleton<ITouristRepository, TouristRepository>();
        services.AddSingleton<ITouristReservationRepository, TouristReservationRepository>();
        services.AddSingleton<ITourReviewRepository, TourReviewRepository>();
        services.AddSingleton<ITourReviewImageRepository, TourReviewImageRepository>();
        services.AddSingleton<IAccommodationRenovationRepository, AccommodationRenovationRepository>();
        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<ITourRequestRepository, TourRequestRepository>();
        services.AddSingleton<IMonthRepository, MonthRepository>();
        services.AddSingleton<IRequestedTouristRepository, RequestedTouristRepository>();
        services.AddSingleton<IForumRepository, ForumRepository>();
        services.AddSingleton<IForumCommentRepository, ForumCommentRepository>();
        services.AddSingleton<IForumCommentReportRepository, ForumCommentReportRepository>();
        services.AddSingleton<IComplexTourRequestsRepository, ComplexTourRequestsRepository>();
        services.AddSingleton<IGuideInfoRepository, GuideInfoRepository>();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        // Add services
        services.AddSingleton<OwnerService>();
        services.AddSingleton<GuestService>();
        services.AddSingleton<AccommodationService>();
        services.AddSingleton<AccommodationReservationService>();
        services.AddSingleton<LocationService>();
        services.AddSingleton<GuestRatingService>();
        services.AddSingleton<ImageService>();
        services.AddSingleton<AccommodationReviewService>();
        services.AddSingleton<TourService>();
        services.AddSingleton<TourStartTimeService>();
        services.AddSingleton<CheckpointService>();
        services.AddSingleton<TourVoucherService>();
        services.AddSingleton<TourImageService>();
        services.AddSingleton<LanguageService>();
        services.AddSingleton<TouristService>();
        services.AddSingleton<TourReservationService>();
        services.AddSingleton<TourReviewService>();
        services.AddSingleton<TourReviewImageService>();
        services.AddSingleton<AccommodationRenovationService>();
        services.AddSingleton<UserService>();
        services.AddSingleton<TourRequestService>();
        services.AddSingleton<MonthService>();
        services.AddSingleton<RequestedTouristService>();
        services.AddSingleton<ForumService>();
        services.AddSingleton<ForumCommentService>();
        services.AddSingleton<ForumCommentReportService>();
        services.AddSingleton<ComplexTourRequestService>();

        services.AddSingleton<GuideService>();
        
    }
}