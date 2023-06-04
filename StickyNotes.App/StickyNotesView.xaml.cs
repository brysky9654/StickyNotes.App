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
    private TextBlock _textBlock;
    public StickyNotesView(TextBlock textBlock)
    {
      InitializeComponent();
      _textBlock = textBlock;
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
      SubTextBox.AcceptsReturn = true;  // 개행문자.

      win.Content = SubTextBox;
      win.Show();

    }
    private void TextBox_Changed(object sender, TextChangedEventArgs e)
    {
      if(e.Source != null)
      {
        StickyNotesMainView mainView  = new StickyNotesMainView();
        //TextBlock textBlock = new TextBlock();
        _textBlock.Text = "";
        _textBlock.Text = SubTextBox.Text;
        _textBlock.Width = 50;
        _textBlock.Height = 50;
        _textBlock.Foreground = new SolidColorBrush(Colors.White);

        //system.invalidoperationexception: 'specified element is already the logical child of another element. disconnect it first.'
        //이거 지우니 됨. 이미 children이 추가되었는데 더 추가됬다는 에러.
        //if(mainView.stackPanel_Notes != null)
        // {
        //   //mainView.stackPanel_Notes.Children.Clear();
        // }

        //mainView.stackPanel_Notes.Children.Add(_textBlock);

      }
    }
  }
}
