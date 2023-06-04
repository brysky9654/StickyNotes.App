using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StickyNotes.App
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    //TODO TextBlock
    //text block 을 만든 이유 : 텍스트박스와 구분해서 텍스트박스 글자 수가 0이면 텍스트 block에 Search...  을 글자수가 0이아니면 textBlock 글자는 "" 
    //text box안에 Textblock 또는 Label로 Search... 글자 만들기.
    // mouse down event : 커서를 텍스트박스에 클릭하면 Label과 Textblock 글자색 얕아지게 만들기. 
    // keyboard down event : Search... 없애고 입력된 값들을 textbox에 채워지게만들기.

    // --> 해결 워터마크 코드 https://code.4noobz.net/wpf-add-a-watermark-to-a-native-wpf-textbox/

    //TODO New window , TextBox
    // + New Note 클릭 시 텍스트 박스가 다른 윈도우창에서 생겨지고 Main 에선 3행에 텍스트 Block이 만들어짐에 따라 글자 입력 시 함께 진행됨.


    //New Window 구성



    private bool IsWater = true;

    private void TextBox_MouseDown(object sender, MouseButtonEventArgs e)
    {
      //searchTextBlock.Foreground = new System.Windows.Media.SolidColorBrush(Colors.DarkGray);

    }

    private void TextBox_KeyDown(object sender, KeyEventArgs e)
    {


    }
  }
}

