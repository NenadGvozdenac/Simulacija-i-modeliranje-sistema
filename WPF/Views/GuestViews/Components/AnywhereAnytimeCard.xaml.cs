using BookingApp.Domain.Models;
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
/// Interaction logic for AnywhereAnytimeCard.xaml
/// </summary>
public partial class AnywhereAnytimeCard : UserControl
{
    public DateTime FirstDate
    {
        get { return (DateTime)GetValue(FirstDateProperty); }
        set { SetValue(FirstDateProperty, value); }
    }


    public static readonly DependencyProperty FirstDateProperty =
        DependencyProperty.Register("FirstDate", typeof(DateTime), typeof(AnywhereAnytimeCard), new PropertyMetadata(DateTime.Now));

    public DateTime LastDate
    {
        get { return (DateTime)GetValue(LastDateProperty); }
        set { SetValue(LastDateProperty, value); }
    }

    public static readonly DependencyProperty LastDateProperty =
        DependencyProperty.Register("LastDate", typeof(DateTime), typeof(AnywhereAnytimeCard), new PropertyMetadata(DateTime.Now));

    public AnywhereAnytimeCard()
    {
        InitializeComponent();
    }

    private void See_more(object sender, RoutedEventArgs e)
    {
        if (this.DataContext is Accommodation accommodation)
        {
            int accommodationId = accommodation.Id;

            Window parentWindow = Window.GetWindow(this);

            if (parentWindow is GuestMainWindow mainWindow)
            {
                mainWindow.GuestViewModel.ShowAccommodationDetailsAnywhere(accommodationId, FirstDate, LastDate);
            }
        }
    }
}
