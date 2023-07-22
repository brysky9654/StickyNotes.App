using System.Threading;
using System.Windows;

namespace StickyNotes.App
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    /* single instance 프로그램 두개 안띄우는법  */
    Mutex mutex;
    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);

      string mutexName = "my.StickyNote";
      bool bCreateNew;

      mutex = new Mutex(true, mutexName, out bCreateNew);

      if (!bCreateNew) // 기존에 있으면 false가 나오기에 !로 true 만들어서 실행되려는 거 shutdown
      {
        Shutdown();
      }
    }
  }
}
