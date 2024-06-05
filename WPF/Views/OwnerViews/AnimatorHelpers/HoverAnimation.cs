using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace BookingApp.WPF.Views.OwnerViews.AnimatorHelpers
{
    public class HoverAnimation
    {
        public void AnimateHover(Border border)
        {
            if (border == null)
                throw new ArgumentNullException(nameof(border));

            // Create animations
            Storyboard mouseEnterStoryboard = new Storyboard();
            Storyboard mouseLeaveStoryboard = new Storyboard();

            DoubleAnimation scaleXEnterAnimation = new DoubleAnimation
            {
                To = 1.02,
                Duration = new Duration(TimeSpan.FromSeconds(0.2))
            };
            Storyboard.SetTarget(scaleXEnterAnimation, border);
            Storyboard.SetTargetProperty(scaleXEnterAnimation, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleX)"));

            DoubleAnimation scaleYEnterAnimation = new DoubleAnimation
            {
                To = 1.02,
                Duration = new Duration(TimeSpan.FromSeconds(0.2))
            };
            Storyboard.SetTarget(scaleYEnterAnimation, border);
            Storyboard.SetTargetProperty(scaleYEnterAnimation, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleY)"));

            DoubleAnimation scaleXLeaveAnimation = new DoubleAnimation
            {
                To = 1,
                Duration = new Duration(TimeSpan.FromSeconds(0.2))
            };
            Storyboard.SetTarget(scaleXLeaveAnimation, border);
            Storyboard.SetTargetProperty(scaleXLeaveAnimation, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleX)"));

            DoubleAnimation scaleYLeaveAnimation = new DoubleAnimation
            {
                To = 1,
                Duration = new Duration(TimeSpan.FromSeconds(0.2))
            };
            Storyboard.SetTarget(scaleYLeaveAnimation, border);
            Storyboard.SetTargetProperty(scaleYLeaveAnimation, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleY)"));

            // Add animations to storyboards
            mouseEnterStoryboard.Children.Add(scaleXEnterAnimation);
            mouseEnterStoryboard.Children.Add(scaleYEnterAnimation);

            mouseLeaveStoryboard.Children.Add(scaleXLeaveAnimation);
            mouseLeaveStoryboard.Children.Add(scaleYLeaveAnimation);

            // Register event handlers
            border.MouseEnter += (sender, e) => mouseEnterStoryboard.Begin(border);
            border.MouseLeave += (sender, e) => mouseLeaveStoryboard.Begin(border);
        }
    }
}