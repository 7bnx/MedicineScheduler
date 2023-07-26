using System;
using System.Globalization;

namespace MedicineScheduler.WPFUI.Converters;

public class NullToBoolConverter : ConverterMarkupExtension<NullToBoolConverter>
{
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    return value is not null;
  }

  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    return value;
  }
}
