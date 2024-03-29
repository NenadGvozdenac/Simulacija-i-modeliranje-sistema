using BookingApp.Model.MutualModels;
using BookingApp.Model.OwnerModels;
using BookingApp.Repository;
using BookingApp.Repository.OwnerRepositories;
using BookingApp.Resources.Types;
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

    private void SignUp(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(Username))
        {
            ResetErrors();
            return;
        }

        User user = _repository.GetByUsername(Username);

        if (IsBadPassword(user))
        {
            IsError();
            return;
        }

        AddUser();
        OpenSignInForm();
    }

    private void OpenSignInForm()
    {
        SignInForm signInForm = new SignInForm();
        signInForm.Show();
        Close();
    }

    private void AddUser()
    {
        User user = new User(Username, Password, FindCurrentlyActivatedRadioButton());
        _repository.Add(user);

        // Adds a copy for super owner
        if(user.Type == UserType.Owner)
        {
            OwnerInfoRepository.GetInstance().Add(new OwnerInfo(user.Id, false, 0, 0, 0));
        }
    }

    private bool IsBadPassword(User user)
    {
        return user != null || PasswordTextBox.Password != ConfirmPasswordTextBox.Password;
    }

    private void IsError()
    {
        ResetErrors();
        DisplayPasswordsNotSameErrorMessage();
        ResetTextBoxes();
    }

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
        ShowHidePasswordPlaceholder(passwordBox, password);

        // Set the password property
        Password = passwordBox.Password;
    }

    private void ShowHidePasswordPlaceholder(PasswordBox passwordBox, string password)
    {
        if (passwordBox.Name == "PasswordTextBox")
        {
            PasswordPlaceholder.Visibility = string.IsNullOrEmpty(password) ? Visibility.Visible : Visibility.Hidden;
        }
        else if (passwordBox.Name == "ConfirmPasswordTextBox")
        {
            ConfirmPasswordPlaceholder.Visibility = string.IsNullOrEmpty(password) ? Visibility.Visible : Visibility.Hidden;
        }
    }

    private void CloseApplication(object sender, MouseButtonEventArgs e)
    {
        Close();
    }
}
