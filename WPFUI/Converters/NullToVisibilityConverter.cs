using System;
using System.Windows;

namespace MedicineScheduler.WPFUI.Converters;

public class NullToVisibilityConverter : ConverterMarkupExtension<NullToVisibilityConverter>
{
  public Visibility NullValue { get; set; } = Visibility.Collapsed;
  public Visibility NotNullValue { get; set; } = Visibility.Visible;
  public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
  {
    bool invertedParameter = bool.TryParse((string)parameter, out var parsedInverted) && parsedInverted;
    var res = value is null == invertedParameter ? NotNullValue : NullValue;
    return res;
  }

  public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
  {
    return DependencyProperty.UnsetValue;
  }
}
