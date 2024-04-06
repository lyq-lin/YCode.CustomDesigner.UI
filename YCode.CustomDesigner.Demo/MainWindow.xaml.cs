using System.Windows;
using YCode.CustomDesigner.UI;

namespace YCode.CustomDesigner.Demo
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

		private void OnElementDeleted(object sender, UI.Events.YCodeNodeDeletedEventArgs e)
		{
			MessageBox.Show("该节点已删除!");
		}

		private void OnElementDeleting(object sender, UI.Events.YCodeNodeDeletingEventArgs e)
		{
			var node = e.RemoveItem as YCodeNode;

			var result = MessageBox.Show(
				  $"你确定删除该节点:{node.Name}吗?",
					"删除操作",
					MessageBoxButton.YesNo);

			if (result == MessageBoxResult.No)
			{
				e.Cancel = true;
			}
		}

		private void OnSortClick(object sender, RoutedEventArgs e)
		{
			flow.IsSort = true;
		}
	}
}