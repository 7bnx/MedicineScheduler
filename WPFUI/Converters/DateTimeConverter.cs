using System;
using System.Globalization;
using System.Windows;

namespace MedicineScheduler.WPFUI.Converters;

public class DateTimeConverter : ConverterMarkupExtension<DateTimeConverter>
{
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (parameter != null && parameter.ToString() == "EN")
      return ((DateTime)value).ToString("MM.dd.yyyy");

    return ((DateTime)value).ToString("dd.MM.yyyy");
  }

  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    return DependencyProperty.UnsetValue;
  }
}