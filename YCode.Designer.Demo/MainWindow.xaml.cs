using System.Windows;
using System.Windows.Controls;

namespace YCode.Designer.Demo
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

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //tips: 测试该UI在切换TabControl时的场景
            if (this.DataContext is MainViewModel viewModel && e.AddedItems.Count > 0)
            {
                if (e.AddedItems[0] is TabItem item)
                {
                    if (item.Tag.ToString() == "0")
                    {
                        item.Content = viewModel.Uis[0];
                    }
                    else
                    {
                        item.Content = viewModel.Uis[1];
                    }
                }
            }
        }
    }
}