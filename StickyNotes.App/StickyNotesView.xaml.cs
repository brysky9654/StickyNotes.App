using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
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

    public StickyNotesView(TextBlock textBlock, List<Linq> linq)
    {
      InitializeComponent();
      _textBlock = textBlock;
      _Linq = linq;  // Main 뷰에있는 linq를 _Linq가 참조. _Linq 원소삭제시 linq도 삭제
      newWindow();

    }

    //TODO: 윈도우창과 텍스트 블록 링킹되서 윈도우창도 구분되야함. 윈도우창 내 삭제버튼 기능 통해 윈도우창과 텍스트블록 제거하기.

    //TextBlock에 이름 설정. window도 이름설정가능?  가능한가봄./
    private void newWindow() 
    {

      //TODO: 정리하기 win 사용해도 되나?
      // this 윈도우로 해결할 수 있을 것 같으면 해결.
      //동일 파일에 윈도우가 2개 있을지라도 다른 윈도우에 만들어진 아이템 이름을 다른 윈도에 사용할 수 없다.

      //TODO!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
      //win 과 관련된거 모두 this로 옮기기. 
      Window win = new Window();

      _Linq.Add(new Linq { win = win, textBlock = _textBlock });
 
      //win.Width = 300;
      //win.Height = 300;


      //linq.Add(new Linq { win = win, textBlock = _textBlock });

      SubTextBox.Foreground = new SolidColorBrush(Colors.White);
      SubTextBox.Background = new SolidColorBrush(Colors.Black);
      SubTextBox.TextWrapping = TextWrapping.Wrap;
      SubTextBox.AcceptsReturn = true;  // 개행문자.
      this.Width = 300;
      this.Height = 300;


      win.WindowStyle = WindowStyle.ThreeDBorderWindow;

      this.Show();
    }
    private void TextBox_Changed(object sender, TextChangedEventArgs e)
    {
      if(e.Source != null)
      {
        StickyNotesMainView mainView  = new StickyNotesMainView();
        _textBlock.Text = "";
        _textBlock.Text = SubTextBox.Text;
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
