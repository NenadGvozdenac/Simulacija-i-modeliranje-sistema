using BookingApp.Application.UseCases;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repositories;
using BookingApp.View;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace BookingApp;

public partial class App
{
    private static IServiceProvider _serviceProvider;

    public static IServiceProvider ServiceProvider => _serviceProvider;

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
        services.AddSingleton<IGuestRatingRepository, GuestRatingRepository>();
        services.AddSingleton<IAccommodationReservationMovingRepository, AccommodationReservationMovingRepository>();
        services.AddSingleton<IAccommodationImageRepository, AccommodationImageRepository>();
        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<IAccommodationReviewRepository, AccommodationReviewRepository>();
        services.AddSingleton<IReviewImageRepository, ReviewImageRepository>();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        // Add services
        services.AddSingleton<OwnerService>();
        services.AddSingleton<AccommodationService>();
        services.AddSingleton<AccommodationReservationService>();
        services.AddSingleton<LocationService>();
        services.AddSingleton<GuestRatingService>();
        services.AddSingleton<ImageService>();
        services.AddSingleton<AccommodationReviewService>();
    }
}