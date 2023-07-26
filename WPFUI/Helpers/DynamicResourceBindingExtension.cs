﻿using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace MedicineScheduler.WPFUI.Helpers;

public class DynamicResourceBindingExtension : MarkupExtension
{

  public DynamicResourceBindingExtension() { }
  public DynamicResourceBindingExtension(object resourceKey)
      => ResourceKey = resourceKey ?? throw new ArgumentNullException(nameof(resourceKey));

  public object ResourceKey { get; set; }
  public IValueConverter Converter { get; set; }
  public object ConverterParameter { get; set; }
  public CultureInfo ConverterCulture { get; set; }
  public string StringFormat { get; set; }
  public object TargetNullValue { get; set; }

  private BindingProxy _bindingProxy;
  private BindingTrigger _bindingTrigger;

  public override object ProvideValue(IServiceProvider serviceProvider)
  {
    var dynamicResource = new DynamicResourceExtension(ResourceKey);
    _bindingProxy = new BindingProxy(dynamicResource.ProvideValue(null)); 

    var dynamicResourceBinding = new Binding()
    {
      Source = _bindingProxy,
      Path = new PropertyPath(BindingProxy.ValueProperty),
      Mode = BindingMode.OneWay
    };

    var targetInfo = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));

    if (targetInfo.TargetObject is DependencyObject dependencyObject)
    {

      dynamicResourceBinding.Converter = Converter;
      dynamicResourceBinding.ConverterParameter = ConverterParameter;
      dynamicResourceBinding.ConverterCulture = ConverterCulture;
      dynamicResourceBinding.StringFormat = StringFormat;
      dynamicResourceBinding.TargetNullValue = TargetNullValue;

      if (dependencyObject is FrameworkElement targetFrameworkElement)
        targetFrameworkElement.Resources[_bindingProxy] = _bindingProxy;

      return dynamicResourceBinding.ProvideValue(serviceProvider);
    }

    var findTargetBinding = new Binding()
    {
      RelativeSource = new RelativeSource(RelativeSourceMode.Self)
    };
    _bindingTrigger = new BindingTrigger();

    var wrapperBinding = new MultiBinding()
    {
      Bindings = {
                dynamicResourceBinding,
                findTargetBinding,
                _bindingTrigger.Binding
            },
      Converter = new InlineMultiConverter(WrapperConvert)
    };
    return wrapperBinding.ProvideValue(serviceProvider);
  }
  private object WrapperConvert(object[] values, Type targetType, object parameter, CultureInfo culture)
  {

    var dynamicResourceBindingResult = values[0];
    var bindingTargetObject = values[1];

    if (Converter != null)

      dynamicResourceBindingResult = Converter.Convert(dynamicResourceBindingResult, targetType, ConverterParameter, ConverterCulture);

    if (dynamicResourceBindingResult == null)
      dynamicResourceBindingResult = TargetNullValue;

    else if (targetType == typeof(string) && StringFormat != null)
      dynamicResourceBindingResult = String.Format(StringFormat, dynamicResourceBindingResult);

    if (bindingTargetObject is FrameworkElement targetFrameworkElement
    && !targetFrameworkElement.Resources.Contains(_bindingProxy))
    {
      targetFrameworkElement.Resources[_bindingProxy] = _bindingProxy;

      SynchronizationContext.Current.Post((state) => {
        _bindingTrigger.Refresh();
      }, null);
    }

    return dynamicResourceBindingResult;
  }
}
