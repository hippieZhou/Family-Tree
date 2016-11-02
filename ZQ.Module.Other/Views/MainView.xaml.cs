using System.Windows;
using System.Windows.Controls;

namespace ZQ.Module.Other.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("这是在一个模块中动态加载另一个模块！");
        }
    }
}
