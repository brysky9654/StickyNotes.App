using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace StickyNotes.App
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class StickyNotesMainView : Window
  {
    private StickyNotesView stickyNotesView;
    public struct Linq
    {
      public Window win;
      public TextBlock textBlock;
    }
    List<Linq> linq = new();
    List<Linq> deleteLinq =  new();
    public StickyNotesMainView()
    {
      InitializeComponent();
    }
    protected void MainHeader_MouseDownLeft(object sender, MouseButtonEventArgs e)
    {
      // the reason why couldn't mouse down event In Grid Grid   https://stackoverflow.com/questions/12669756/mousedown-doesnt-work-in-grid-only-on-buttons-which-in-grids
      if (e.ButtonState == MouseButtonState.Pressed)
      {
        DragMove();
      }
    }
    //syntax: A new window must appear when you click TextBlock. Only one should appear.
    // If it already appears, pass.
    // Whether there is an index that can distinguish new window windows, and if so, make sure that the new window is linked to a specific textBlock.
    // The reason is that if you press the delete button in the new window (text box), the textBlock in the main window window should be deleted. A new window is also deleted.
    // On the other hand, the main window has a delete button function in textBlock so that you can remove the new linked window when you delete it.
    protected void TextBlockList_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      //https://stackoverflow.com/questions/16608523/c-sharp-wpf-move-the-window  
      foreach (Linq x in linq)
      {
        x.win.Topmost = false; ;// Set all sub(new) windows to Topmost false. As a result, the click button target window is made to appear preferentially within the selectTextBlock_Click event.
        x.textBlock.MouseLeftButtonDown += selectTextBlock_Click;
      }
    }

    protected void TextBlockList_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
    {
      //https://stackoverflow.com/questions/16608523/c-sharp-wpf-move-the-window  
      foreach (Linq x in linq)
      {
        x.win.Topmost = false; ;// Set all sub(new) windows to Topmost false. As a result, the click button target window is made to appear preferentially within the selectTextBlock_Click event.
        x.textBlock.MouseRightButtonDown += TextBlockList_Event;
      }



    }
    private void closeButton_Click(object sender, RoutedEventArgs e) => Close();

    //TODO TextBlock
    /// <summary>
    /// watermark code https://code.4noobz.net/wpf-add-a-watermark-to-a-native-wpf-textbox/
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>


    private void selectTextBlock_Click(object sender, MouseButtonEventArgs e)
    {
      var sender_type = sender.GetType();

      //축약하기  sender_TextBlock_Type 굳이 안만들고 sender로 바로 할 수 있을 것 같음
      TextBlock sender_textBlock_type = (TextBlock)sender;
      //sender type is textBlock type

      if (sender_type == sender_textBlock_type.GetType())
      {
        // among linq this the List type, find textBlock what I Clicked and then set visible. 
        foreach (Linq x in linq)
        {
          if (sender_textBlock_type == x.textBlock)
          {
            //x.win.Visibility = Visibility.Collapsed;
            x.win.Visibility = Visibility.Visible;
            x.win.Topmost = true;


          }
        }
      }
    }

    public void MainAddTextBox_Click(object sender, RoutedEventArgs e)
    {
      if (e.Source != null)
      {
        TextBlock textBlock = new TextBlock();
        textBlock.HorizontalAlignment = HorizontalAlignment.Stretch;
        textBlock.TextAlignment = TextAlignment.Left;
        textBlock.Background = new SolidColorBrush(Colors.DimGray);
        textBlock.Height = 50;
        textBlock.Margin = new Thickness(10, 10, 10, 0);
        stackPanel_Notes.Children.Add(textBlock);
        stickyNotesView = new StickyNotesView(textBlock, linq, this);
      }
    }
    private void TextBlockList_Event(object sender, MouseButtonEventArgs e)
    {
      //TODO: Delete list box in main view


      foreach (Linq x in linq)
      {
        if (x.textBlock == sender)
        {
          deleteLinq.Insert(0, x);

          //TODO Set combo box position. In the corresponding textBlock area. Place in the mouse click position. [Note]
          //잠만textBlockComboBox.Visibility = Visibility.Visible;
          var button = new Button();
          var contextmenu = new ContextMenu();
          button.ContextMenu = contextmenu;
          var closeNote_MenuItem = new MenuItem();
          var openNote_MenuItem  = new MenuItem();
          var deleteNote_MenuItem = new MenuItem();
          deleteNote_MenuItem.Header = "Delete note";
          if (x.win.Visibility == Visibility.Visible)
          {
            closeNote_MenuItem.Header = "Close note";
            contextmenu.Items.Add(closeNote_MenuItem);
          }
          else
          {
            openNote_MenuItem.Header = "Open note";
            contextmenu.Items.Add(openNote_MenuItem);
          }
          contextmenu.Items.Add(deleteNote_MenuItem);
          x.textBlock.ContextMenu = contextmenu;

          //https://stackoverflow.com/questions/65974406/dynamically-adding-a-context-menu-item-with-a-click-handler-in-wpf/65974457#65974457
          closeNote_MenuItem.Click += CloseNote_Click;
          openNote_MenuItem.Click += OpenNote_Click;
          deleteNote_MenuItem.Click += DeleteNote_Click;

        }
      }
    }
    private void CloseNote_Click(object sender, RoutedEventArgs e)
    {
      
      MessageBox.Show("close note");
      deleteLinq[0].win.Visibility = Visibility.Collapsed;
      
    }

    private void OpenNote_Click(object sender, RoutedEventArgs e)
    {
      //.Visibility = Visibility.Visible;
      MessageBox.Show("open note");
      deleteLinq[0].win.Visibility = Visibility.Visible;
    }
    private void DeleteNote_Click(object sender, RoutedEventArgs e)
    {
      //.Close();
      MessageBox.Show("delete note");
      stackPanel_Notes.Children.Remove(deleteLinq[0].textBlock);
      deleteLinq[0].win.Close();
    }

    private void Search_TexeChanged(object sender, TextChangedEventArgs e)
    {

      string textBoxString = (sender as TextBox).Text;

      foreach (Linq x in linq)
      {
        string textBlockString = (x.textBlock as TextBlock).Text;
        //https://ponyozzang.tistory.com/331
        if (textBlockString.Contains(textBoxString))
        {
          x.textBlock.Visibility = Visibility.Visible;
          int initIndex = 0;
          int startIndex = 0;
          int endIndex = 0;
          while (endIndex!=-1)
          {
            startIndex = x.textBlock.Text.IndexOf(textBoxString, initIndex);

            if (startIndex == -1)
            {
              startIndex = endIndex;
              endIndex = startIndex + textBoxString.Length-1; // <= 로.
              break;
            }
            endIndex = startIndex + (textBoxString.Length - 1);

            //TODO start ~end background color
            makeBackGroundText(x.textBlock, startIndex, endIndex);

            initIndex = endIndex + 1;
          }
          if (endIndex == -1)
          {
            //Change all background to part
            x.textBlock.Background = new SolidColorBrush(Colors.DimGray);
          }
        }
        else
        {
          x.textBlock.Visibility = Visibility.Collapsed;
        }
      
      }


    }

    //TODO SEARCH PART. BACKGROUND IS YLLEO .
    private void makeBackGroundText(TextBlock textBlock, int startIndex, int endIndex)
    {  //Change all background to part
      textBlock.Background = new SolidColorBrush(Colors.Wheat);

    }
  }
}


