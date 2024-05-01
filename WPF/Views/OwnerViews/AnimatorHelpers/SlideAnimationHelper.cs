using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace BookingApp.WPF.Views.OwnerViews.AnimatorHelpers;

public class SlideAnimationHelper
{
    private readonly double _seconds;
    private readonly SlideAnimationType _slideType;
    private readonly UserControl _from;
    private readonly UserControl _to;
    private readonly Grid _wrapperPanel;

    public enum SlideAnimationType {
        SlideFromLeftToRight,
        SlideFromRightToLeft,
    }

    public SlideAnimationHelper(double seconds, SlideAnimationType slideType, UserControl from, UserControl to, Grid wrapperPanel)
    {
        this._seconds = seconds;
        this._slideType = slideType;
        this._from = from;
        this._to = to;
        this._wrapperPanel = wrapperPanel;
    }

    public async Task AnimateIn()
    {
        _from.Visibility = Visibility.Visible;

        switch(_slideType)
        {
            case SlideAnimationType.SlideFromLeftToRight:
                await SlideFromLeftToRight(_from);
                break;
            case SlideAnimationType.SlideFromRightToLeft:
                await SlideFromRightToLeft(_from);
                break;
        }
    }

    public async Task AnimateOut()
    {
        _from.Visibility = Visibility.Hidden;

        switch(_slideType)
        {
            case SlideAnimationType.SlideFromLeftToRight:
                await SlideFromRightToLeft(_to);
                break;
            case SlideAnimationType.SlideFromRightToLeft:
                await SlideFromLeftToRight(_to);
                break;
        }
    }

    private async Task SlideFromRightToLeft(UserControl control)
    {
        var sb = new Storyboard();
        var SlideAnimation = new ThicknessAnimation
        {
            From = new Thickness(control.RenderSize.Width, 0, -control.RenderSize.Width, 0),
            To = new Thickness(0, 0, 0, 0),
            Duration = new Duration(TimeSpan.FromSeconds(0.3)),
            DecelerationRatio = 0.9
        };

        Storyboard.SetTargetProperty(SlideAnimation, new PropertyPath("Margin"));
        sb.Children.Add(SlideAnimation);

        sb.Begin(_wrapperPanel);
    }

    private async Task SlideFromLeftToRight(UserControl control)
    {
        var sb = new Storyboard();
        var SlideAnimation = new ThicknessAnimation
        {
            From = new Thickness(-control.RenderSize.Width, 0, control.RenderSize.Width, 0),
            To = new Thickness(0, 0, 0, 0),
            Duration = new Duration(TimeSpan.FromSeconds(0.3)),
            DecelerationRatio = 0.9
        };

        Storyboard.SetTargetProperty(SlideAnimation, new PropertyPath("Margin"));
        sb.Children.Add(SlideAnimation);

        sb.Begin(_wrapperPanel);
    }
}
