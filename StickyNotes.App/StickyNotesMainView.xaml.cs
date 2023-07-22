using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;

namespace StickyNotes.App
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class StickyNotesMainView : Window
  {
    private StickyNotesView stickyNotesView;

    public struct Index_struct
    {
      public int startIdx;
      public int endIdx;
    }
    public struct Linq
    {
      public Window win;
      public TextBlock textBlock;
    }
    List<Linq> linq = new();
    List<Linq> deleteLinq = new();
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
    /// watermark code https://code.4noobz.net/wpf-add-a-watermark-to-a-native-wpf-textbox/
    private void selectTextBlock_Click(object sender, MouseButtonEventArgs e)
    {
      var sender_type = sender.GetType();

      //TO DO : Refactoring.
      TextBlock sender_textBlock_type = (TextBlock)sender;
      if (sender_type == sender_textBlock_type.GetType())
      {
        // among linq this the List type, find textBlock what I Clicked and then set visible. 
        foreach (Linq x in linq)
        {
          if (sender_textBlock_type == x.textBlock)
          {
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
        textBlock.TextWrapping = TextWrapping.Wrap;
        stackPanel_Notes.Children.Add(textBlock);
        stickyNotesView = new StickyNotesView(textBlock, linq, this);
      }
    }
    private void TextBlockList_Event(object sender, MouseButtonEventArgs e)
    {
      foreach (Linq x in linq)
      {
        // whitout saving when delete, 'deleteLinq and x.textBlock' should be deleted.
        // delete는 저장할 필요없이 delete 시 deleteLinq와 x.textBlock을 삭제해야함.
        // -> Linq 구조체의 경우 삭제 기능이 없기에 textBlock을 null 처리함.
        if (x.textBlock == sender)
        {
          if (deleteLinq.Count >= 1) deleteLinq.RemoveAt(0);
          deleteLinq.Insert(0, x); // for event purpose(temparary).
          var button = new Button();
          var contextmenu = new ContextMenu();
          button.ContextMenu = contextmenu;
          var closeNote_MenuItem = new MenuItem();
          var openNote_MenuItem = new MenuItem();
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
      deleteLinq[0].win.Visibility = Visibility.Collapsed;
    }
    private void OpenNote_Click(object sender, RoutedEventArgs e)
    {
      //.Visibility = Visibility.Visible;
      deleteLinq[0].win.Visibility = Visibility.Visible;
    }

    //ToDo: Refactoring
    private void DeleteNote_Click(object sender, RoutedEventArgs e)
    {
      //.Close();
      foreach (Linq _linq in linq) //fixed.
      {
        if (deleteLinq.Count == 0 || deleteLinq == null) return;
        if (deleteLinq[0].textBlock == _linq.textBlock) //error
        {
          deleteLinq[0].win.Close();
          _linq.textBlock.Visibility = Visibility.Collapsed;
          _linq.win.Close();
          _linq.textBlock.Text = null;
          stackPanel_Notes.Children.Remove(deleteLinq[0].textBlock);
          deleteLinq.RemoveAt(0);
        }
      }
    }
    private void Search_TextChanged(object sender, TextChangedEventArgs e)
    {
      string textBoxString = (sender as TextBox).Text; //casting
      foreach (Linq x in linq)
      {
        if (x.textBlock.Text == null) continue;
        string textBlockString = (x.textBlock as TextBlock).Text;
        if (textBoxString == "")
        {
          x.textBlock.Inlines.Clear();
          x.textBlock.Inlines.Add(textBlockString);
          x.textBlock.Visibility = Visibility.Visible; // I set Visible  since  downward setting  collapsed. 
          continue;
        }
        //https://ponyozzang.tistory.com/331

        bool isContainText = isSameString(textBlockString, textBoxString);
        if (isContainText == true)
        {
          x.textBlock.Visibility = Visibility.Visible;
          int initIndex = 0;
          int startIndex = 0;
          int endIndex = 0;
          TextBlock textBlock_temp = new();
          textBlock_temp.Text = x.textBlock.Text;
          //하나의 텍스트블록에서 search 문자열 (하나 이상)의 start, end Index들을 찾음. 해당 문자열이 없을때까지.
          List<Index_struct> indexSavingList = new List<Index_struct>();
          while (endIndex != -1 || initIndex < x.textBlock.Text.Length)
          {
            startIndex = getSameStringIndex(textBlockString, textBoxString, initIndex);
            if (startIndex == -1)
            {
              break;
            }
            endIndex = startIndex + (textBoxString.Length - 1);
            if (endIndex == -1) break;
            var idxStruct = new Index_struct();
            idxStruct.startIdx = startIndex;
            idxStruct.endIdx = endIndex;
            indexSavingList.Add(idxStruct);
            initIndex = endIndex + 1;
          }
          makeBackGroundText(x.textBlock, indexSavingList);

        }
        else x.textBlock.Visibility = Visibility.Collapsed;
      }
    }
    private bool isSameString(string textBlock, string textBoxString) // 대소구분없이 동일 문자열 찾기.
    {
      textBoxString = textBoxString.ToUpper();
      textBlock = textBlock.ToUpper();
      return textBlock.IndexOf(textBoxString) != -1 ? true : false;
    }

    /*
     * 로직 변경 : 스택에 검색한 단어 인덱스 넣기,. 검색 단어가 변경될 때마다 stack. 클리어해서 다시 넣기.
     * 스택 사이즈 만큼 한번에 해당인덱스에 반복문 돌려서 hight light 색상 입히기
     */
    private int getSameStringIndex(string textBlock, string textBoxString, int initIndex) // 대소구분없이 동일 문자열 찾기
    {
      textBoxString = textBoxString.ToUpper();
      int findIndex = 0;
      int unfindIndex = -1;
      for (int textBlockIndex = initIndex; textBlockIndex <= textBlock.Length - textBoxString.Length; ++textBlockIndex)
      {
        string text = "";
        text += textBlock[textBlockIndex];
        findIndex = textBlockIndex;
        for (int textBlockIndex2 = textBlockIndex + 1; textBlockIndex2 < textBlockIndex + textBoxString.Length; ++textBlockIndex2)
        {
          text += textBlock[textBlockIndex2];
        }
        text = text.ToUpper();
        if (text == textBoxString) return findIndex;
      }
      return unfindIndex;
    }
    //Highlight Searched Text in WPF 
    //https://www.codeproject.com/Tips/1229482/WPF-TextBlock-Highlighter
    //I just reference this code, and then I wrote my algorightm from my thinking
    //richtextBlock  c#tutorial 26 search and highlight text in textbox or richtextbox.

    //run / inline / richtext  개념.
    private void makeBackGroundText(TextBlock textBlock, List<Index_struct> indexSavingList)
    {  //Highlight Searched Text in WPF 
      string strTextblock = textBlock.Text.ToString(); // strTextBlock은 하이라이트와 하이라이트가 아닌 문자열을 찾을 용도
      textBlock.Inlines.Clear(); // textBlock 초기화해서 general Text와 highlightText를 삽입할 예정.
      int offset = 1; //기본값:1 ,  하이라이트 문자열을 찾으면 textBlock에 모두 추가 한 후 인덱스 시작을 하이라이트문자열 뒤 문자부터 시작하기 위한 용도
      for (int idx = 0; idx < strTextblock.Length; idx += offset) // 
      {
        string generalText = ""; // 하이라이트가 아닌 문자 (문자로 칭한 이유는 generalText에 한글자 씩 textBlock에 추가)
        string highlightText = ""; // 하이라이트 문자열
        int isHighLight = 0;
        for (int SavingListidx = 0; SavingListidx < indexSavingList.Count; ++SavingListidx) // 하이라이트 시작,끝 인덱스 구조체를 넣은 indexSavingList 를 탐색
        {
          // strTextBlock의 인덱스와 하이라이트 start인덱스가 동일하면
          // 해당 start인덱스와 end인덱스 사이 문자열을 하이라이트 색으로 업데이트해 textBlock에 삽입,
          // offset값을 업데이트
          if (idx == indexSavingList[SavingListidx].startIdx)
          {
            isHighLight = 1;
            int startIdx = indexSavingList[SavingListidx].startIdx;
            int endIdx = indexSavingList[SavingListidx].endIdx;
            highlightText += strTextblock.Substring(startIdx, endIdx - startIdx + 1);
            textBlock.Inlines.Add(new Run(highlightText) { Background = Brushes.LightBlue });
            offset = (endIdx - startIdx + 1);
            break;
          }
        }
        //general 텍스트이면 해당 인덱스 문자열(1글자)를 textBlock에 삽입./
        if (isHighLight == 0)
        {
          offset = 1;
          generalText += strTextblock[idx];
          textBlock.Inlines.Add(generalText);
        }
      }
    }
  }
}