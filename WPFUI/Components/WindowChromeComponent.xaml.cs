using System.Windows;

namespace MedicineScheduler.WPFUI.Components;

public partial class WindowChromeComponent : ResourceDictionary
{
  public WindowChromeComponent()
  {
    InitializeComponent();
  }

  private void CloseClick(object sender, RoutedEventArgs e)
  {
    var window = (Window)((FrameworkElement)sender).TemplatedParent;
    window.Close();
  }

  private void MaximizeRestoreClick(object sender, RoutedEventArgs e)
  {
    var window = (Window)((FrameworkElement)sender).TemplatedParent;
    if (window.WindowState == WindowState.Normal)
    {
      window.WindowState = WindowState.Maximized;
    } else
    {
      window.WindowState = WindowState.Normal;
    }
  }

  private void MinimizeClick(object sender, RoutedEventArgs e)
  {
    var window = (Window)((FrameworkElement)sender).TemplatedParent;
    window.WindowState = WindowState.Minimized;
  }

  private void Button_Click(object sender, RoutedEventArgs e)
  {
    MessageBox.Show("Hello!");
  }
}
