using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

using BookingApp.Resources.Types;
using BookingApp.View.PathfinderViews;
using BookingApp.View.TouristViews;
using BookingApp.Domain.Models;
using BookingApp.Repositories;
using BookingApp.WPF.Views.GuestViews;

namespace BookingApp.View;

public partial class SignInForm : Window
{
    // The repository for the user
    private readonly UserRepository _repository;

    // The username property
    private string _username;
    public string Username
    {
        get => _username;
        set
        {
            if (value != _username)
            {
                _username = value;
                OnPropertyChanged();
            }
        }
    }

    // The password property
    private string _password;
    public string Password
    {
        get => _password;
        set
        {
            if (value != _password)
            {
                _password = value;
                OnPropertyChanged();
            }
        }
    }

    // The event handler for the property changed event
    public event PropertyChangedEventHandler PropertyChanged;

    // The method to invoke the property changed event
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Constructor for the SignInForm
    /// </summary>
    public SignInForm()
    {
        InitializeComponent();
        DataContext = this;
        _repository = new UserRepository();
    }

    /// <summary>
    /// Sign in the user.
    /// Executed when the user clicks the sign in button.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SignIn(object sender, RoutedEventArgs e)
    {
        // Get the user from the database
        User user = _repository.GetByUsername(Username);

        // If the user exists and the password is correct
        if (user != null && user.Password == Password)
        {
            // Open the corresponding window
            OpenCorrespondingWindow(user);
        }
        else
        {
            // Display an error message and reset the text boxes
            DisplayErrorMessage();
            ResetTextBoxes();
        }
        
    }

    /// <summary>
    /// Open the corresponding window based on the user type.
    /// TODO: Implement the corresponding windows.
    /// </summary>
    /// <param name="user"></param>
    private void OpenCorrespondingWindow(User user)
    {
        switch(user.Type) {
            case UserType.Owner:
                OpenOwnerWindow(user);
                break;

            case UserType.Guest:
                OpenGuestWindow(user);
                break;

            case UserType.Tourist:
                OpenTouristWindow(user);
                break;

            case UserType.Pathfinder:
                OpenGuideWindow(user);
                break;
        }
    }

    private void OpenGuideWindow(User user)
    {
        GuideMainWindow PathfinderMainWindow = new GuideMainWindow(user);
        PathfinderMainWindow.Show();
        Close();
    }

    private void OpenTouristWindow(User user)
    {
        TouristMainWindow touristMainWindow = new TouristMainWindow(user);
        touristMainWindow.Show();
        Close();
    }

    private void OpenGuestWindow(User user)
    {
        GuestMainWindow guestMainWindow = new GuestMainWindow(user);
        guestMainWindow.Show();
        Close();
    }

    public void OpenOwnerWindow(User user)
    {
        OwnerMainWindow ownerMainWindow = new OwnerMainWindow(user);
        ownerMainWindow.Show();
        Close();
    }

    /// <summary>
    /// Display an error message that some credentials are invalid.
    /// </summary>
    private void DisplayErrorMessage()
    {
        ErrorMessage.Visibility = Visibility.Visible;
    }

    /// <summary>
    /// Reset the text boxes.
    /// </summary>
    private void ResetTextBoxes()
    {
        UsernameTextBox.Text = "";
        PasswordTextBox.Password = "";
    }

    /// <summary>
    /// Open the sign up form.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SignUp(object sender, RoutedEventArgs e)
    {
        SignUpForm signUpForm = new SignUpForm();
        signUpForm.Show();
        Close();
    }

    /// <summary>
    /// On password changed event.
    /// Used to show/hide the password placeholder.
    /// Also, sets the password property.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void PasswordTextBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        // Get the password box
        PasswordBox passwordBox = sender as PasswordBox;

        if (passwordBox == null)
            return;

        // Get the password
        string password = passwordBox.Password;

        // Show/hide the password placeholder
        PasswordPlaceholder.Visibility = string.IsNullOrEmpty(password) ? Visibility.Visible : Visibility.Hidden;

        // Set the password property
        Password = passwordBox.Password;
    }

    private void CloseApplication(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        Close();
    }
}
