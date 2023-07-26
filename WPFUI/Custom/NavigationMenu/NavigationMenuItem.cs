using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MedicineScheduler.WPFUI.Custom.NavigationMenu;

public class NavigationMenuItem : RadioButton
{
  static NavigationMenuItem()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationMenuItem), new FrameworkPropertyMetadata(typeof(NavigationMenuItem)));
  }

  public NavigationMenuItem()
  {
    Binding b = new("IsChecked")
    {
      Source = this
    };
    SetBinding(IsCheckedInteractionProperty, b);
  }

  public static readonly DependencyProperty TextProperty =
      DependencyProperty.Register("Text", typeof(string), typeof(NavigationMenuItem),
      new PropertyMetadata(null, TextPropertyChanged));

  public string Text
  {
    get { return (string)GetValue(TextProperty); }
    set { SetValue(TextProperty, value); }
  }
  private static void TextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) { }


  public static readonly DependencyProperty IconGeometryProperty =
    DependencyProperty.Register("IconGeometry", typeof(string), typeof(NavigationMenuItem),
    new PropertyMetadata(null, IconGeometryPropertyChanged));

  public string IconGeometry
  {
    get { return (string)GetValue(IconGeometryProperty); }
    set { SetValue(IconGeometryProperty, value); }
  }
  private static void IconGeometryPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) { }


  public static readonly DependencyProperty IsInteractionAnimationEnabledProperty =
      DependencyProperty.Register("IsInteractionAnimationEnabled", typeof(bool), typeof(NavigationMenuItem),
      new PropertyMetadata(true, null));

  public bool IsInteractionAnimationEnabled
  {
    get { return (bool)GetValue(IsInteractionAnimationEnabledProperty); }
    set { SetValue(IsInteractionAnimationEnabledProperty, value); }
  }

  public static readonly DependencyProperty IsCheckedInteractionProperty =
    DependencyProperty.Register("IsCheckedInteraction", typeof(bool?), typeof(NavigationMenuItem), new(false, null, OnCoerceIsCheckedInteraction));

  private static object OnCoerceIsCheckedInteraction(DependencyObject d, object baseValue)
  {
    var vm = (NavigationMenuItem)d;
    var testBool = vm.IsChecked;
    return testBool is not null && testBool != false && vm.IsInteractionAnimationEnabled;
  }

  public bool? IsCheckedInteraction
  {
    get { return (bool)GetValue(IsCheckedInteractionProperty); }
    set { SetValue(IsCheckedInteractionProperty, value); }
  }
}
