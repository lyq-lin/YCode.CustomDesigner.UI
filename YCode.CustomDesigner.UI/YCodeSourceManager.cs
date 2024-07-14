namespace YCode.CustomDesigner.UI
{
	internal class YCodeSourceManager(YCodeEditor editor)
	{
		private readonly YCodeEditor _editor = editor;

		public void OnChanged()
		{
			_editor.Nodes = _editor.Source.Nodes.Select(node => this.CreateNode(node));

			_editor.Lines = _editor.Source.Lines.Select(line => this.CreateLine(line));
		}

		private YCodeNode CreateNode(YCodeNodeViewModel node)
		{
			var yNode = new YCodeNode()
			{
				NodeId = node.Id,
				DataContext = node
			};

			if (node.GetType() is Type type &&
				type.IsGenericType &&
				type.GetGenericTypeDefinition() == typeof(YCodeColumnNodeViewModel<>))
			{
				yNode = new YCodeColumnNode
				{
					NodeId = node.Id,
					DataContext = node,
					Content = node
				};
			}
			else
			{
				yNode.SetBinding(YCodeNode.ContentProperty, nameof(node.Name));
			}

			yNode.SetBinding(YCodeCanvas.LeftProperty, nameof(node.X));
			yNode.SetBinding(YCodeCanvas.TopProperty, nameof(node.Y));

			return yNode;
		}

		private YCodeLine CreateLine(YCodeLineViewModel line)
		{
			var yLine = new YCodeLine()
			{
				DataContext = line
			};

			yLine.SetBinding(YCodeLine.SourceIdProperty, nameof(line.SourceId));
			yLine.SetBinding(YCodeLine.TargetIdProperty, nameof(line.TargetId));

			return yLine;
		}
	}
}
