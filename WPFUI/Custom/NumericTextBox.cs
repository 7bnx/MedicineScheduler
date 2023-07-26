using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace MedicineScheduler.WPFUI.Custom;

public partial class NumericTextBox : TextBox
{
  public bool AllowOnlyIntegers { get; set; }
  private Regex _regex = null!;
  public string NumericText
  {
    get => (string)GetValue(NumericTextProperty);
    set => SetValue(NumericTextProperty, value);
  }
  public static readonly DependencyProperty NumericTextProperty =
        DependencyProperty.Register(
            nameof(NumericText),
            typeof(string),
            typeof(NumericTextBox),
            new FrameworkPropertyMetadata(
                string.Empty,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal,
                new PropertyChangedCallback(OnNumericTextChanged),
                null,
                true,
                UpdateSourceTrigger.LostFocus));

  private static void OnNumericTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (d is TextBox textBox)
    {
      var prevCuret = textBox.CaretIndex;
      if (textBox.Text != (string)e.NewValue)
      {
        textBox.Text = (string)e.NewValue;
        if (prevCuret > 0) textBox.CaretIndex = prevCuret - 1;
      }
    }
  }

  protected override void OnTextChanged(TextChangedEventArgs e)
  {
    base.OnTextChanged(e);
    var allowedArr = Text.Where(c => char.IsDigit(c) || (!AllowOnlyIntegers && c == '.')).ToArray();
    if (Text.Length != allowedArr.Length)
      Text = new(allowedArr);
    if (string.IsNullOrEmpty(Text))
    {
      Text = "0";
      CaretIndex = 1;
    } else if (Text.Length > 1 && Text[0] == '0' && Text[1] != '.')
    {
      Text = Text[1..];
      CaretIndex = Text.Length;
    }
    NumericText = Text;
  }

  private Regex GetRegex()
    => _regex ??= AllowOnlyIntegers ? IntegerRegex() : DoubleRegex();

  protected override void OnPreviewTextInput(TextCompositionEventArgs e)
  {
    Regex regex = GetRegex();
    e.Handled = regex.IsMatch(e.Text) || Text.Contains('.') && e.Text[0] == '.';
  }

  [GeneratedRegex("[^0-9]+")]
  private static partial Regex IntegerRegex();
  [GeneratedRegex("[^.0-9]+")]
  private static partial Regex DoubleRegex();
}