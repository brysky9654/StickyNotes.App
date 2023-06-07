using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using System.Windows.Shapes;
using System.Xml.Linq;
using static StickyNotes.App.StickyNotesMainView;

namespace StickyNotes.App
{

  /// <summary>
  /// Interaction logic for StickyNotesView.xaml
  /// </summary>




  public partial class StickyNotesView : Window
  {
    private TextBlock _textBlock;
    private List<Linq> _Linq;

    StickyNotesMainView _main_this;


    public StickyNotesView(TextBlock textBlock, List<Linq> linq, StickyNotesMainView main_this)
    {
      InitializeComponent();
      _textBlock = textBlock;
      _Linq = linq;  // The linq in the main view is referenced by _Linq. When deleting _Linq elements, also delete linq
      _main_this = main_this; // 새 윈도우창에서 새윈도우 창 생성법.
      newWindow();
    }

    //Link with windows and text blocks to form a set. Removing windows and text blocks through the delete button function in windows.

    private void newWindow()
    {
      //If there are two windows in the same file, the item name created in one window cannot be used in another.
            _Linq.Add(new Linq { win= this, textBlock = _textBlock });  
     this.Topmost = true;
      this.Show();
      
    }
    private void TextBox_Changed(object sender, TextChangedEventArgs e)
    {
      //this.Topmost = true;
      if (e.Source != null)
      {
        StickyNotesMainView mainView  = new StickyNotesMainView();
        _textBlock.Text = "";
        _textBlock.Text = SubTextBox.Text;
        _textBlock.Foreground = new SolidColorBrush(Colors.White);

        //system.invalidoperationexception: 'specified element is already the logical child of another element. disconnect it first.'
        //resolve:remove downward code.
        //if(mainView.stackPanel_Notes != null)
        // {
        //   //mainView.stackPanel_Notes.Children.Clear();
        // }
        //mainView.stackPanel_Notes.Children.Add(_textBlock);

      }
    }
    private void CloseNewWindow_Click(object sender, RoutedEventArgs e)
    {
      this.Visibility = Visibility.Collapsed;
    }
    private void SubHeader_MouseDownLeft(object sender, MouseButtonEventArgs e)
    {
      if (e.ButtonState == MouseButtonState.Pressed)
      {
        DragMove();
      }
    }

    private void SubAddTextBox_Click(object sender, RoutedEventArgs e)
    {
      _main_this.MainAddTextBox_Click(sender, e); // If i do this, new window is showing but can't make textblock in MainView stackpanel since new() initialze.

    }

    private void SubWindow_MouseClick(object sender, MouseButtonEventArgs e)
    {
      this.Topmost = true;
    }
  }
}
