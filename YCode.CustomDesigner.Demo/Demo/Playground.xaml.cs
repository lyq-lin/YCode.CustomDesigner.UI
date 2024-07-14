using System.Windows;
using System.Windows.Controls;
using YCode.CustomDesigner.UI;

namespace YCode.CustomDesigner.Demo
{
	/// <summary>
	/// Interaction logic for Playground.xaml
	/// </summary>
	public partial class Playground : UserControl
	{
		public Playground()
		{
			InitializeComponent();
		}

		private void OnElementDeleted(object sender, UI.YCodeNodeDeletedEventArgs e)
		{
			MessageBox.Show("该节点已删除!");
		}

		private void OnElementDeleting(object sender, UI.YCodeNodeDeletingEventArgs e)
		{
			var node = e.RemoveItem as YCodeNode;

			var result = MessageBox.Show(
				  $"你确定删除该节点:{node?.NodeId}吗?",
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

		private void OnProcessClick(object sender, RoutedEventArgs e)
		{
			var node = flow.Children.OfType<YCodeLogicNode>().FirstOrDefault();

			node?.Process();
		}

		private void OnPerformanceTestClick(object sender, RoutedEventArgs e)
		{
			if (this.DataContext is MainViewModel main)
			{
				for (int i = 1; i <= 1000; i++)
				{
					main.Source.Nodes.Add(new YCodeColumnNodeViewModel<ColumnField>()
					{
						Id = $"Node{i}",
						Name = $"TestNode{i}",
						Columns = [
							new ColumnField() { Id = $"Column{i}", Name = $"Column{i}", Description = $"This is Column{i}" },
							new ColumnField() { Id = $"Column{i + Random.Shared.Next(9999)}", Name = $"Column{i + Random.Shared.Next(9999)}", Description = $"This is Column{i + Random.Shared.Next(9999)}" },
						],
						X = Random.Shared.Next(0, 5000),
						Y = Random.Shared.Next(0, 4500)
					});

					if (i > 1)
					{
						main.Source.Lines.Add(new()
						{
							SourceId = $"Node{i - 1}",
							TargetId = $"Node{Random.Shared.Next(1, i)}"
						});
					}
				}
			}
		}

		private void OnElementChanged(object sender, EventArgs e)
		{

		}
	}
}
