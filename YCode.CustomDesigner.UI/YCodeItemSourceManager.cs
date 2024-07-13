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

				foreach (var node in _canvas.Source.Nodes)
				{
					this.CreateNode(node);
				}

				foreach (var line in _canvas.Source.Lines)
				{
					this.CreateLine(line);
				}
			}
		}

		private void OnLinesCollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
					{
						if (e.NewItems != null)
						{
							foreach (var item in e.NewItems)
							{
								if (item is YCodeLineViewModel line)
								{
									this.CreateLine(line);
								}
							}
						}
					}
					break;
				case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
					{
						if (e.OldItems != null)
						{
							foreach (var item in e.OldItems)
							{
								if (item is YCodeLineViewModel line)
								{
									var isExist = _canvas.Children.OfType<YCodeLine>()
										.FirstOrDefault(x =>
											line.SourceId.Equals(x.Source.NodeId) &&
											line.TargetId.Equals(x.Target.NodeId));

									if (isExist != null)
									{
										_canvas.Children.Remove(isExist);
									}
								}
							}
						}
					}
					break;
				case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
					{
					}
					break;
			}
		}

		private void OnNodesCollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
					{
						if (e.NewItems != null)
						{
							foreach (var item in e.NewItems)
							{
								if (item is YCodeNodeViewModel node)
								{
									this.CreateNode(node);
								}
							}
						}
					}
					break;
				case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
					{
						if (e.OldItems != null)
						{
							foreach (var item in e.OldItems)
							{
								if (item is YCodeNodeViewModel node)
								{
									var isExist = _canvas.Children.OfType<YCodeNode>()
										.FirstOrDefault(x => node.Id.Equals(x.NodeId));

									if (isExist != null)
									{
										_canvas.Children.Remove(isExist);
									}
								}
							}
						}
					}
					break;
				case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
					{
					}
					break;
			}
		}

		private void CreateNode(YCodeNodeViewModel node)
		{
			var yNode = new YCodeNode()
			{
				NodeId = node.Id,
				DataContext = node
			};

			yNode.SetBinding(YCodeNode.ContentProperty, nameof(node.Name));
			yNode.SetBinding(YCodeCanvas.LeftProperty, nameof(node.X));
			yNode.SetBinding(YCodeCanvas.TopProperty, nameof(node.Y));

			_canvas.Children.Add(yNode);
		}

		private void CreateLine(YCodeLineViewModel line)
		{
			var yLine = new YCodeLine()
			{
				DataContext = line
			};

			yLine.SetBinding(YCodeLine.SourceIdProperty, nameof(line.SourceId));
			yLine.SetBinding(YCodeLine.TargetIdProperty, nameof(line.TargetId));

			_canvas.Children.Add(yLine);
		}
	}
}
