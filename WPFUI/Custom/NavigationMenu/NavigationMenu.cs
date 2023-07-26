using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace MedicineScheduler.WPFUI.Custom.NavigationMenu;

public class NavigationMenu : Control
{

  public static readonly DependencyProperty OpenMinimumWidthProperty =
    DependencyProperty.Register("OpenMinimumWidth", typeof(double), typeof(NavigationMenu),
        new PropertyMetadata(0.0, OnOpenMinimumWidthPropertyChanged));

  public double OpenMinimumWidth
  {
    get { return (double)GetValue(OpenMinimumWidthProperty); }
    set { SetValue(OpenMinimumWidthProperty, value); Width = value; }
  }

  public static readonly DependencyProperty IsOpenProperty =
      DependencyProperty.Register("IsOpen", typeof(bool), typeof(NavigationMenu),
          new PropertyMetadata(false, OnIsOpenPropertyChanged));

  public bool IsOpen
  {
    get { return (bool)GetValue(IsOpenProperty); }
    set { SetValue(IsOpenProperty, value); }
  }

  public static readonly DependencyProperty OpenCloseDurationProperty =
      DependencyProperty.Register("OpenCloseDuration", typeof(Duration), typeof(NavigationMenu),
          new PropertyMetadata(Duration.Automatic));

  public Duration OpenCloseDuration
  {
    get { return (Duration)GetValue(OpenCloseDurationProperty); }
    set { SetValue(OpenCloseDurationProperty, value); }
  }

  public static readonly DependencyProperty FallbackOpenWidthProperty =
      DependencyProperty.Register("FallbackOpenWidth", typeof(double), typeof(NavigationMenu),
          new PropertyMetadata(100.0));

  public double FallbackOpenWidth
  {
    get { return (double)GetValue(FallbackOpenWidthProperty); }
    set { SetValue(FallbackOpenWidthProperty, value); }
  }

  public static readonly DependencyProperty ContentProperty =
      DependencyProperty.Register("Content", typeof(FrameworkElement), typeof(NavigationMenu),
          new PropertyMetadata(null));

  public FrameworkElement Content
  {
    get { return (FrameworkElement)GetValue(ContentProperty); }
    set { SetValue(ContentProperty, value); }
  }

  static NavigationMenu()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationMenu), new FrameworkPropertyMetadata(typeof(NavigationMenu)));
  }

  public NavigationMenu()
  {
    Width = OpenMinimumWidth;
  }

  private static void OnIsOpenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (d is NavigationMenu navigationMenu)
    {
      navigationMenu.OnIsOpenPropertyChanged();
    }
  }

  private void OnIsOpenPropertyChanged()
  {
    if (IsOpen)
    {
      OpenMenuAnimated();
    } else
    {
      CloseMenuAnimated();
    }
  }

  private static void OnOpenMinimumWidthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (d is NavigationMenu navigationMenu)
    {
      navigationMenu.OnOpenMinimumWidthProperty();
    }
  }

  private void OnOpenMinimumWidthProperty()
  {
    Width = OpenMinimumWidth;
  }

  private void OpenMenuAnimated()
  {
    double contentWidth = GetDesiredContentWidth();

    DoubleAnimation openingAnimation = new DoubleAnimation(contentWidth, OpenCloseDuration);
    BeginAnimation(WidthProperty, openingAnimation);
  }

  private double GetDesiredContentWidth()
  {
    if (Content == null)
    {
      return FallbackOpenWidth;
    }

    Content.Measure(new Size(MaxWidth, MaxHeight));

    return Content.DesiredSize.Width;
  }

  private void CloseMenuAnimated()
  {
    DoubleAnimation closingAnimation = new DoubleAnimation(OpenMinimumWidth, OpenCloseDuration);
    BeginAnimation(WidthProperty, closingAnimation);
  }
}