using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StickyNotes.App
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class StickyNotesMainView : Window
  {

    private StickyNotesView stickyNotesView;
    public StickyNotesMainView()
    {
      InitializeComponent();
    }

    protected  void Border_PreviewMouseDownLeft(object sender, MouseButtonEventArgs e)
    {
    //https://stackoverflow.com/questions/16608523/c-sharp-wpf-move-the-window
      DragMove();
    }
    private void closeButton_Click(object sender, RoutedEventArgs e) => Close();

    //TODO TextBlock
    //text block 을 만든 이유 : 텍스트박스와 구분해서 텍스트박스 글자 수가 0이면 텍스트 block에 Search...  을 글자수가 0이아니면 textBlock 글자는 "" 
    //text box안에 Textblock 또는 Label로 Search... 글자 만들기.
    // mouse down event : 커서를 텍스트박스에 클릭하면 Label과 Textblock 글자색 얕아지게 만들기. 
    // keyboard down event : Search... 없애고 입력된 값들을 textbox에 채워지게만들기.

    // --> 해결 워터마크 코드 https://code.4noobz.net/wpf-add-a-watermark-to-a-native-wpf-textbox/

    //TODO New window , TextBox
    // + New Note 클릭 시 텍스트 박스가 다른 윈도우창에서 생겨지고 Main 에선 3행에 텍스트 Block이 만들어짐에 따라 글자 입력 시 함께 진행됨.


    //New Window 구성

    private void addTextBox_Click(object sender, RoutedEventArgs e)
    {
      if(e.Source != null)
      {

        //새로운 윈도우창에 텍스트 box가 생겨지고 동시에 기존 윈도우창에 textblock이 생겨진다.
        //그리고 새 윈도우창에 텍스트box 에 textChaned이벤트를 줘서 원래 윈도우창의 textblock에 문자열들이 출력되야한다.
        // 또한 원래 윈도우창에서 textblock을 클릭하면, lingking된 textbox가 나와서 쓰기모드가 가능해야한다.
        TextBlock textBlock = new TextBlock();

        
        stackPanel_Notes.Children.Add(textBlock);
        stickyNotesView = new StickyNotesView(textBlock);


        //if (textBox.Text.Length > 0)
        //{
        //  string fileText = File.ReadAllText(textBox.Text);
        //}

      }


    }


  }
}

