﻿using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace MedicineScheduler.WPFUI.Converters;

public abstract class ConverterMarkupExtension<T> : MarkupExtension, IValueConverter where T : class, new()
{
  private static T _converter = default!;

  public ConverterMarkupExtension()
  {
  }

  public override object ProvideValue(IServiceProvider serviceProvider)
  {
    return _converter ??= new T();
  }

  public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);
  public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
}