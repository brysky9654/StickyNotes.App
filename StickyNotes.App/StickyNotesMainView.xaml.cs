﻿using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
    public  struct Linq
    {
      public Window win;
      public TextBlock textBlock;
    }
    List<Linq> linq = new();
    public StickyNotesMainView()
    {
      InitializeComponent();
    }
    protected void MainHeader_PreviewMouseDownLeft(object sender, MouseButtonEventArgs e) 
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
        x.win.Topmost = false; ;// 맨앞에있는 것들 Topmost false.  selectTextBlock_Click 이벤트 내에서 클릭버튼 대상 window를 우선적으로 보이게 만듦.
        x.textBlock.MouseLeftButtonDown += selectTextBlock_Click; 
      }
    }

    protected void TextBlockList_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
    {
      //https://stackoverflow.com/questions/16608523/c-sharp-wpf-move-the-window  
      foreach (Linq x in linq)
      {
        x.win.Topmost = false; ;// 맨앞에있는 것들 Topmost false.  selectTextBlock_Click 이벤트 내에서 클릭버튼 대상 window를 우선적으로 보이게 만듦.
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
      if(e.Source != null)
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


      foreach(Linq x in linq)
      {
        if(x.textBlock == sender)
        {

          //TODO 콤보박스 위치 설정. 해당 TXTBLOCK 영역에. 마우스 클릭 위치에 놓기.  [주]
          textBlockComboBox.Visibility = Visibility.Visible;


        }
      }
    }
    private void OpenNote_Click(object sender, MouseButtonEventArgs e)
    {
      if(e.LeftButton == MouseButtonState.Pressed)
      {

      }
    }
    private void DeleteNote_Click(object sender, MouseButtonEventArgs e)
    {
      if (e.LeftButton == MouseButtonState.Pressed)
      {

      }
    }

  }
}

