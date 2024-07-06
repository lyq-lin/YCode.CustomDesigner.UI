namespace YCode.CustomDesigner.UI
{
	internal class YCodeItemSourceManager(YCodeCanvas canvas)
	{
		private readonly YCodeCanvas _canvas = canvas;

		public void OnChanged()
		{
			if (_canvas.Source != null)
			{
				_canvas.Source.Nodes.CollectionChanged += OnNodesCollectionChanged;

				_canvas.Source.Lines.CollectionChanged += OnLinesCollectionChanged;
			}
		}

		private void OnLinesCollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{

		}

		private void OnNodesCollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
		}
	}
}
