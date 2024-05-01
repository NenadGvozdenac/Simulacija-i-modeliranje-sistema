using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows;

namespace BookingApp.WPF.Views.OwnerViews;

public class GridLengthAnimation : AnimationTimeline
{
    public override Type TargetPropertyType => typeof(GridLength);

    protected override Freezable CreateInstanceCore()
    {
        return new GridLengthAnimation();
    }

    public static readonly DependencyProperty FromProperty =
        DependencyProperty.Register("From", typeof(GridLength), typeof(GridLengthAnimation));

    public static readonly DependencyProperty ToProperty =
        DependencyProperty.Register("To", typeof(GridLength), typeof(GridLengthAnimation));

    public GridLength From
    {
        get => (GridLength)GetValue(FromProperty);
        set => SetValue(FromProperty, value);
    }

    public GridLength To
    {
        get => (GridLength)GetValue(ToProperty);
        set => SetValue(ToProperty, value);
    }

    public override object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock)
    {
        double fromVal = ((GridLength)GetValue(FromProperty)).Value;
        double toVal = ((GridLength)GetValue(ToProperty)).Value;
        double progress = animationClock.CurrentProgress.Value;

        if (fromVal > toVal)
        {
            return new GridLength((1 - progress) * (fromVal - toVal) + toVal, GridUnitType.Star);
        }
        else
        {
            return new GridLength(progress * (toVal - fromVal) + fromVal, GridUnitType.Star);
        }
    }
}