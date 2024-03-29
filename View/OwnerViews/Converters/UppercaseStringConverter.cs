using System;
using System.Globalization;
using System.Windows.Data;

namespace BookingApp.View.OwnerViews.Converters;

public class UppercaseStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string userType)
        {
            return userType.ToUpper();
        }

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
