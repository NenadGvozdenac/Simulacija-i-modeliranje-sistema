using BookingApp.Model.MutualModels;
using BookingApp.Repository;
using BookingApp.Resources.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace BookingApp.View;

/// <summary>
/// Interaction logic for SignUpForm.xaml
/// </summary>
public partial class SignUpForm : Window
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
    /// Constructor for the SignUpForm
    /// </summary>
    public SignUpForm()
    {
        InitializeComponent();
        DataContext = this;
        _repository = new UserRepository();
    }

    /// <summary>
    /// The event handler for the sign up button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SignUp(object sender, RoutedEventArgs e)
    {
        // If the username is empty, reset the errors
        if (string.IsNullOrEmpty(Username))
        {
            ResetErrors();
            return;
        }

        // Get the user by the username
        User user = _repository.GetByUsername(Username);

        // If the user is not null, display the error message
        if (user != null)
        {
            ResetErrors();
            DisplayUsernameErrorMessage();
            ResetTextBoxes();
            return;
        }

        // If the password and the confirm password are not the same, display the error message
        if (PasswordTextBox.Password != ConfirmPasswordTextBox.Password)
        {
            ResetErrors();
            DisplayPasswordsNotSameErrorMessage();
            ResetTextBoxes();
            return;
        }

        // Create a new user
        User newUser = new User(Username, Password, FindCurrentlyActivatedRadioButton());
        newUser.Id = _repository.NextId();

        // Add the user to the database
        _repository.Add(newUser);

        // Show the success message box
        MessageBox.Show("User created!");

        // Open the sign in form
        SignInForm signInForm = new SignInForm();
        signInForm.Show();
        Close();
    }

    /// <summary>
    /// Display the error message for the username.
    /// Invoked when the username is already taken.
    /// </summary>
    private void DisplayUsernameErrorMessage()
    {
        ErrorMessageUsername.Visibility = Visibility.Visible;
    }

    /// <summary>
    /// Reset the errors.
    /// </summary>
    private void ResetErrors()
    {
        ErrorMessageUsername.Visibility = Visibility.Hidden;
        ErrorMessage.Visibility = Visibility.Hidden;
    }

    /// <summary>
    /// Reset the text boxes.
    /// </summary>
    private void ResetTextBoxes()
    {
        UsernameTextBox.Text = "";
        PasswordTextBox.Password = "";
        ConfirmPasswordTextBox.Password = "";
    }

    /// <summary>
    /// Display the error message that the passwords are not the same.
    /// Invoked when the password and the confirm password are not the same.
    /// </summary>
    private void DisplayPasswordsNotSameErrorMessage()
    {
        ErrorMessage.Visibility = Visibility.Visible;
    }

    /// <summary>
    /// Find the currently activated radio button.
    /// </summary>
    private UserType FindCurrentlyActivatedRadioButton()
    {
        if(OwnerRadioButton.IsChecked == true)
        {
            return UserType.Owner;
        } else if(GuestRadioButton.IsChecked == true)
        {
            return UserType.Guest;
        } else if(TouristRadioButton.IsChecked == true)
        {
            return UserType.Tourist;
        } else
        {
            return UserType.Pathfinder;
        }
    }

    /// <summary>
    /// Sign in button event handler.
    /// Activated when the sign in button is clicked.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SignIn(object sender, RoutedEventArgs e)
    {
        SignInForm signInForm = new SignInForm();
        signInForm.Show();
        Close();
    }

    /// <summary>
    /// On password changed event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void PasswordTextBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        // Get the password box
        PasswordBox passwordBox = sender as PasswordBox;

        // If the password box is null, return
        if (passwordBox == null)
            return;

        // Get the password
        string password = passwordBox.Password;

        // Show/hide the password placeholder
        if (passwordBox.Name == "PasswordTextBox")
        {
            PasswordPlaceholder.Visibility = string.IsNullOrEmpty(password) ? Visibility.Visible : Visibility.Hidden;
        }
        else if (passwordBox.Name == "ConfirmPasswordTextBox")
        {
            ConfirmPasswordPlaceholder.Visibility = string.IsNullOrEmpty(password) ? Visibility.Visible : Visibility.Hidden;
        }

        // Set the password property
        Password = passwordBox.Password;
    }

    private void CloseApplication(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        Close();
    }
}
