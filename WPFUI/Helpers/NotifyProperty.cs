using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MedicineScheduler.WPFUI.Helpers;

public class NotifyProperty<T> : INotifyPropertyChanged
{
  private T _value = default!;

  public static implicit operator T(NotifyProperty<T> obj)
    => obj._value;
  public NotifyProperty(T initValue)
  {
    _value = initValue;
  }

  public NotifyProperty() : this(default!) { }
  public T Value
  {
    get { return _value; }
    set
    {
      if (_value is null || !_value!.Equals(value))
      {
        _value = value;
        OnPropertyChanged();
      }
    }
  }

  public event PropertyChangedEventHandler? PropertyChanged;

  protected void OnPropertyChanged([CallerMemberName] string propertyName = null!)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
}