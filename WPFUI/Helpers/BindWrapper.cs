using System.ComponentModel;
using System.Windows.Media;

namespace MedicineScheduler.WPFUI.Helpers;

public class BindWrapper<T> : INotifyPropertyChanged
{
  private T _content = default!;
  public T Content
  {
    get => _content;
    set
    {
      _content = value;
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Content)));
    }
  }

  public event PropertyChangedEventHandler? PropertyChanged;
}

public class BindWrapperString : BindWrapper<string> { }
public class BindWrapperSolidColorBrush : BindWrapper<SolidColorBrush> { }
public class BindWrapperDouble : BindWrapper<double> { }
