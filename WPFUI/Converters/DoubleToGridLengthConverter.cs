using System;
using System.Globalization;
using System.Windows;

namespace MedicineScheduler.WPFUI.Converters;

public class DoubleToGridLengthConverter : ConverterMarkupExtension<DoubleToGridLengthConverter>
{
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value is not null && value is double d)
      return new GridLength(d);

    throw new InvalidCastException();
  }

  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    return DependencyProperty.UnsetValue;
  }
}
