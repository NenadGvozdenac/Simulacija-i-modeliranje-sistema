using BookingApp.Model.MutualModels;
using BookingApp.Model.OwnerModels;
using BookingApp.Repository;
using BookingApp.Repository.MutualRepositories;
using BookingApp.Repository.OwnerRepositories;
using BookingApp.Services;
using BookingApp.Services.Mutual;
using BookingApp.Services.Owner;
using BookingApp.View;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace BookingApp;

public partial class App : Application
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
        services.AddSingleton<AccommodationRepository>();
        services.AddSingleton<AccommodationReservationRepository>();
        services.AddSingleton<LocationRepository>();
        services.AddSingleton<OwnerInfoRepository>();
        services.AddSingleton<GuestRatingRepository>();
        services.AddSingleton<AccommodationReservationMovingRepository>();
        services.AddSingleton<AccommodationImageRepository>();
        services.AddSingleton<UserRepository>();
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
    }

}
