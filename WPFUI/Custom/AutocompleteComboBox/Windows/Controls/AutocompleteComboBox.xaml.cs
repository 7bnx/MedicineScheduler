using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using MedicineScheduler.WPFUI.Custom.ACCB.Misc.Disposables;
using MedicineScheduler.WPFUI.Custom.ACCB.Windows;
using MedicineScheduler.WPFUI.Custom.ACCB.Windows.Media;

namespace MedicineScheduler.WPFUI.Custom.ACCB
{

  public partial class AutocompleteComboBox : ComboBox
  {
    private readonly SerialDisposable _disposable = new();
    private bool _firstTextSet;
    private TextBox _editableTextBoxCache = null!;
    private Predicate<object> _defaultItemsFilter = null!;

    public TextBox EditableTextBox
    {
      get
      {
        if (_editableTextBoxCache == null)
        {
          const string name = "PART_EditableTextBox";
          _editableTextBoxCache = (TextBox)VisualTreeModule.FindChild(this, name);
        }
        return _editableTextBoxCache;
      }
    }

    /// <summary>
    /// Gets text to match with the query from an item.
    /// Never null.
    /// </summary>
    /// <param name="item"/>
    string TextFromItem(object item)
    {
      if (item == null) return string.Empty;

      var d = new DependencyVariable<string>();
      d.SetBinding(item, TextSearch.GetTextPath(this));
      return d.Value ?? string.Empty;
    }

    #region ItemsSource
    public static new readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable), typeof(AutocompleteComboBox),
            new PropertyMetadata(null, ItemsSourcePropertyChanged));
    public new IEnumerable ItemsSource
    {
      get
      {
        return (IEnumerable)GetValue(ItemsSourceProperty);
      }
      set
      {
        SetValue(ItemsSourceProperty, value);
      }
    }
    private static void ItemsSourcePropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dpcea)
    {
      var comboBox = (ComboBox)dependencyObject;
      var previousSelectedItem = comboBox.SelectedItem;
      if (dpcea.NewValue is ICollectionView cv)
      {
        ((AutocompleteComboBox)dependencyObject)._defaultItemsFilter = cv.Filter;
        comboBox.ItemsSource = cv;
      } else
      {
        ((AutocompleteComboBox)dependencyObject)._defaultItemsFilter = null!;
        IEnumerable? newValue = dpcea.NewValue as IEnumerable;
        CollectionViewSource newCollectionViewSource = new()
        {
          Source = newValue
        };
        comboBox.ItemsSource = newCollectionViewSource.View;
      }

      comboBox.SelectedItem = previousSelectedItem;

      // if ItemsSource doesn't contain previousSelectedItem
      if (comboBox.SelectedItem != previousSelectedItem)
      {
        comboBox.SelectedItem = null;
      }
    }
    #endregion ItemsSource

    #region Setting
    static readonly DependencyProperty _settingProperty =
        DependencyProperty.Register(
            "Setting",
            typeof(AutoCompleteComboBoxSetting),
            typeof(AutocompleteComboBox)
        );

    public static DependencyProperty SettingProperty
    {
      get { return _settingProperty; }
    }

    public AutoCompleteComboBoxSetting Setting
    {
      get { return (AutoCompleteComboBoxSetting)GetValue(SettingProperty); }
      set { SetValue(SettingProperty, value); }
    }

    AutoCompleteComboBoxSetting SettingOrDefault
    {
      get { return Setting ?? AutoCompleteComboBoxSetting.Default; }
    }
    #endregion

    #region OnTextChanged
    long _revisionId;
    string _previousText = null!;

    struct TextBoxStatePreserver : IDisposable
    {
      readonly TextBox _textBox;
      readonly int _selectionStart;
      readonly int _selectionLength;
      readonly string _text;

      public void Dispose()
      {
        _textBox.Text = _text;
        _textBox.Select(_selectionStart, _selectionLength);
      }

      public TextBoxStatePreserver(TextBox textBox)
      {
        _textBox = textBox;
        _selectionStart = textBox.SelectionStart;
        _selectionLength = textBox.SelectionLength;
        _text = textBox.Text;
      }
    }

    static int CountWithMax<T>(IEnumerable<T> xs, Predicate<T> predicate, int maxCount)
    {
      var count = 0;
      foreach (var x in xs)
      {
        if (predicate(x))
        {
          count++;
          if (count > maxCount) return count;
        }
      }
      return count;
    }

    void Unselect()
    {
      var textBox = EditableTextBox;
      textBox.Select(textBox.SelectionStart + textBox.SelectionLength, 0);
    }

    void UpdateFilter(Predicate<object> filter)
    {
      using (new TextBoxStatePreserver(EditableTextBox))
      using (Items.DeferRefresh())
      {
        // Can empty the text box. I don't why.
        Items.Filter = filter;
      }
    }

    void OpenDropDown(Predicate<object> filter)
    {
      UpdateFilter(filter);
      IsDropDownOpen = true;
      Unselect();
    }

    void OpenDropDown()
    {
      var filter = GetFilter();
      OpenDropDown(filter);
    }

    void UpdateSuggestionList()
    {
      if (!_firstTextSet)
      {
        _firstTextSet = true;
        return;
      }
      var text = Text;

      if (text == _previousText) return;
      _previousText = text;

      if (string.IsNullOrEmpty(text))
      {
        IsDropDownOpen = false;
        SelectedItem = null;

        using (Items.DeferRefresh())
        {
          Items.Filter = _defaultItemsFilter;
        }
      } else if (SelectedItem != null && TextFromItem(SelectedItem) == text)
      {
        // It seems the user selected an item.
        // Do nothing.
      } else
      {
        using (new TextBoxStatePreserver(EditableTextBox))
        {
          SelectedItem = null;
        }
        var filter = GetFilter();
        var maxCount = SettingOrDefault.MaxSuggestionCount;
        var count = CountWithMax(ItemsSource?.Cast<object>() ?? Enumerable.Empty<object>(), filter, maxCount);

        if (0 < count && count <= maxCount)
        {
          OpenDropDown(filter);
        } else if (count == 0)
        {
          IsDropDownOpen = false;
          SelectedItem = null;
        }
      }
    }

    void OnTextChanged(object sender, TextChangedEventArgs e)
    {
      var id = unchecked(++_revisionId);
      var setting = SettingOrDefault;

      if (setting.Delay <= TimeSpan.Zero)
      {
        UpdateSuggestionList();
        return;
      }

      _disposable.Content =
          new Timer(
              state =>
              {
                Dispatcher.InvokeAsync(() =>
                {
                  if (_revisionId != id) return;
                  UpdateSuggestionList();
                });
              },
              null,
              setting.Delay,
              Timeout.InfiniteTimeSpan
          );
    }
    #endregion

    public void ComboBox_PreviewKeyDown(object sender, KeyEventArgs e)
    {
      if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control) && e.Key == Key.Space)
      {
        OpenDropDown();
        e.Handled = true;
      }
    }

    Predicate<object> GetFilter()
    {
      var filter = SettingOrDefault.GetFilter(Text, TextFromItem);

      return _defaultItemsFilter != null
          ? i => _defaultItemsFilter(i) && filter(i)
          : filter;
    }
    public AutocompleteComboBox()
    {
      InitializeComponent();
      AddHandler(TextBoxBase.TextChangedEvent, new TextChangedEventHandler(OnTextChanged));
    }
  }
}
