using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StickyNotes.App
{
  /// <summary>
  /// Interaction logic for StickyNotesView.xaml
  /// </summary>
  
  public partial class StickyNotesView : UserControl
  {
    public StickyNotesView()
    {
      InitializeComponent();
      newWindow();

    }

    private void newWindow() 
    {
      Window win = new Window();
      win.Width = 300;
      win.Height = 300;

      SubTextBox.Foreground = new SolidColorBrush(Colors.White);
      SubTextBox.Background = new SolidColorBrush(Colors.Black);
      SubTextBox.TextWrapping = TextWrapping.Wrap;

      win.Content = SubTextBox;
      win.Show();

    }
    private void TextBox_Changed(object sender, TextChangedEventArgs e)
    {
      if(e.Source != null)
      {
        MainWindow mainView   = new MainWindow();
        TextBlock textBlock = new TextBlock();
        textBlock.Text = SubTextBox.Text;
        mainView.stackPanel_Notes.Children.Insert(0, textBlock);
        MessageBox.Show(mainView.stackPanel_Notes.Children[0].ToString());



      }
    }
  }
}
