using System.Windows.Controls;

namespace YCode.CustomDesigner.UI
{
	internal class YCodeSortManager(YCodeCanvas canvas)
	{
		private Canvas _canvas = canvas;
		private List<YCodeNode>? _nodes;
		private List<Edge>? _edges;

		private void InitSort()
		{
			_nodes = _canvas.Children.OfType<YCodeNode>().ToList();

			_edges = _canvas.Children
				.OfType<YCodeLine>()
				.Select(x => new Edge
				{
					SourceNodeId = x.Source.Name,
					TargetNodeId = x.Target.Name,
				})
				.ToList();
		}

		public void HorizontalAutoSort()
		{
			InitSort();
		}

		public void VerticalAutoSort()
		{
			InitSort();
		}
	}



	internal class Edge
	{
		public string SourceNodeId { get; set; } = String.Empty;
		public string TargetNodeId { get; set; } = String.Empty;
	}
}
