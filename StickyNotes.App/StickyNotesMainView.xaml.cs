using System.Collections.Generic;
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

    public struct Linq
    {
      public Window win;
      public TextBlock textBlock;
    }
    List<Linq> linq = new();
    public StickyNotesMainView()
    {
      InitializeComponent();

  }


    protected void Header_PreviewMouseDownLeft(object sender, MouseButtonEventArgs e) 
    {
      // Grid  에서 마우스 down이벤트 안된 이유 https://stackoverflow.com/questions/12669756/mousedown-doesnt-work-in-grid-only-on-buttons-which-in-grids
      if (e.ButtonState == MouseButtonState.Pressed)
      {
        DragMove();
      }


    }
    //TODO: TextBlock 클릭시 해당 윈도우창 나타나야함. 단, 1나만 나타나야함. 기존에 이미 나타나져있으면 pass
    // 윈도우창들을 구분할 수 있는 인덱스 존재여부, 존재한다면 그 윈도우창이 특정 textBlock과 링킹 되도록 해야함. 윈도우창(텍스트박스)에서 삭제 버튼 누르면 textBlock이 삭제될 수 있게.
    // textBlock에서 삭제 버튼기능이 있어 삭제 누르면 링킹된 윈도우창도 없앨 수 있게.
    protected  void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      //https://stackoverflow.com/questions/16608523/c-sharp-wpf-move-the-window


      //text 이벤트 추가.
      foreach (Linq x in linq) 
      {
        x.textBlock.MouseLeftButtonDown += selectTextBlock_Click;
      }
      
    }
    private void closeButton_Click(object sender, RoutedEventArgs e) => Close();

    //TODO TextBlock
    /// <summary>
    /// 워터마크 코드 https://code.4noobz.net/wpf-add-a-watermark-to-a-native-wpf-textbox/
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    //TODO New window , TextBox  
    // 내 코드 분석하기.
    //새로운 윈도우창에 텍스트 box가 생겨지고 동시에 기존 윈도우창에 textblock이 생겨진다.
    //그리고 새 윈도우창에 텍스트box 에 textChaned이벤트를 줘서 원래 윈도우창의 textblock에 문자열들이 출력되야한다.
    // 또한 원래 윈도우창에서 textblock을 클릭하면, lingking된 textbox가 나와서 쓰기모드가 가능해야한다.
    // + New Note 클릭 시 텍스트 박스가 다른 윈도우창에서 생겨지고 Main 에선 3행에 텍스트 Block이 만들어짐에 따라 글자 입력 시 함께 진행됨. - 해결. 근데 의아함.
    // 코드분석해보기, textblock을 stack panel에 추가하지 않았고 txtbox와 txtblock을 연결시키지도 않았는데 되네?

    private void selectTextBlock_Click(object sender, MouseButtonEventArgs e)
    {
      var sender_type = sender.GetType();
      TextBlock sender_textBlock_type = (TextBlock)sender;

      //sender 타입이 textBlock 타입이라면
      if (sender_type == sender_textBlock_type.GetType())
      {   
        //linq 원소들 중에 현재 클릭한 textBlock을 찾아서 Visible 주기.
        foreach( Linq x in linq)
         {
          if(sender_textBlock_type ==x.textBlock)
          {
            x.win.Visibility = Visibility.Collapsed;

            x.win.Visibility = Visibility.Visible;

          }

        }
      }
   
    }

    private void addTextBox_Click(object sender, RoutedEventArgs e)
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

        stickyNotesView = new StickyNotesView(textBlock, linq);
      }
    }

  }
}

