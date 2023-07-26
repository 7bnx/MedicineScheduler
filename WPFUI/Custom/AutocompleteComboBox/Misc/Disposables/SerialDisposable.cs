using System;

namespace MedicineScheduler.WPFUI.Custom.ACCB.Misc.Disposables;

sealed class SerialDisposable : IDisposable
{
  IDisposable _content = null!;

  public IDisposable Content
  {
    get { return _content; }
    set
    {
      _content?.Dispose();

      _content = value;
    }
  }

  public void Dispose()
  {
    Content = null!;
  }
}
