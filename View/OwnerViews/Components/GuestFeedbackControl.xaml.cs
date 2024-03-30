using BookingApp.Miscellaneous;
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

namespace BookingApp.View.OwnerViews.Components;

public partial class GuestFeedbackControl : UserControl
{
    public string AccommodationName { get; set; }
    public string GuestName { get; set; }
    public DateSpan DateSpan { get; set; }
    public string RequiresRenovation { get; set; }
    public GuestFeedbackControl()
    {
        AccommodationName = "Accommodation Name";
        GuestName = "Guest Name";
        DateSpan = new DateSpan(DateTime.Now, DateTime.Now.AddDays(10));
        RequiresRenovation = "Requires Renovation";
        InitializeComponent();
    }
}
