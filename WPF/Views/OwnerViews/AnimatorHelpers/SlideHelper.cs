using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace BookingApp.WPF.Views.OwnerViews.AnimatorHelpers; 
public class SlideHelper
{
    private enum Direction
    {
        Left,
        Right
    }

    private readonly Panel mainPanel;
    private readonly List<UserControl> userControls;

    public SlideHelper(List<UserControl> controls, Panel panel)
    {
        userControls = controls ?? throw new ArgumentNullException(nameof(controls));
        mainPanel = panel ?? throw new ArgumentNullException(nameof(panel));

        foreach (var control in userControls)
        {
            if (!mainPanel.Children.Contains(control))
            {
                throw new ArgumentException("User control must be present in the main panel.", nameof(controls));
            }
        }
    }

    private Storyboard CreateStoryboard()
    {
        return new Storyboard();
    }

    private async Task ExecuteSlideAnimation(UserControl control, Thickness from, Thickness to, TimeSpan duration)
    {
        var sb = CreateStoryboard();
        var slideAnimation = CreateSlideAnimation(from, to, duration);
        Storyboard.SetTarget(slideAnimation, control);
        Storyboard.SetTargetProperty(slideAnimation, new PropertyPath("Margin"));
        sb.Children.Add(slideAnimation);

        sb.Begin();
        await Task.Delay(duration);
    }

    private ThicknessAnimation CreateSlideAnimation(Thickness from, Thickness to, TimeSpan duration)
    {
        return new ThicknessAnimation
        {
            From = from,
            To = to,
            Duration = new Duration(duration),
            DecelerationRatio = 0.9
        };
    }

    private Thickness GetSlideFrom(Direction direction, double panelWidth)
    {
        switch (direction)
        {
            case Direction.Left:
                return new Thickness(panelWidth, 0, -panelWidth, 0);
            case Direction.Right:
                return new Thickness(-panelWidth, 0, panelWidth, 0);
            default:
                return new Thickness(0);
        }
    }

    public async Task AnimateIn(UserControl wrapper)
    {
        var visibleWrapper = userControls.FirstOrDefault(x => x.Visibility == Visibility.Visible);

        if (visibleWrapper == wrapper)
        {
            return;
        }

        int indexOfVisibleWrapper = userControls.IndexOf(visibleWrapper);
        int indexOfWrapper = userControls.IndexOf(wrapper);

        Direction slideDirection = indexOfWrapper < indexOfVisibleWrapper ? Direction.Right : Direction.Left;

        await AnimateOut(visibleWrapper, slideDirection == Direction.Left ? Direction.Right : Direction.Left);

        wrapper.Visibility = Visibility.Visible;

        var slideFrom = GetSlideFrom(slideDirection, mainPanel.ActualWidth);
        var slideTo = new Thickness(0, 0, 0, 0);

        await ExecuteSlideAnimation(wrapper, slideFrom, slideTo, TimeSpan.FromSeconds(0.3));
    }

    private async Task AnimateOut(UserControl? visibleWrapper, Direction slideDirection)
    {
        if (visibleWrapper == null)
        {
            return;
        }

        var slideFrom = new Thickness(0, 0, 0, 0);
        var slideTo = GetSlideFrom(slideDirection, mainPanel.ActualWidth);

        await ExecuteSlideAnimation(visibleWrapper, slideFrom, slideTo, TimeSpan.FromSeconds(0.2));

        visibleWrapper.Visibility = Visibility.Collapsed;
    }
}