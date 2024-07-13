using YCode.CustomDesigner.UI;

namespace YCode.CustomDesigner.Demo
{
	internal class MainViewModel : YCodeNotifyPropertyChanged
	{
		private YCodeSourceViewModel _source;

		public MainViewModel()
		{
			_source = new YCodeSourceViewModel();

			_source.Nodes.Add(new YCodeColumnNodeViewModel<ColumnField>()
			{
				Id = "A",
				Name = "AAA",
				Columns = [
					new ColumnField() { Id = "Column1", Name = "Column1", Description = "This is Column1" },
					new ColumnField() { Id = "Column2", Name = "Column2", Description = "This is Column2" },
				],
				X = 100,
				Y = 100,
			});

			_source.Nodes.Add(new YCodeColumnNodeViewModel<ColumnField>()
			{
				Id = "B",
				Name = "BBB",
				Columns = [
					new ColumnField() { Id = "Column1", Name = "Column1", Description = "This is Column1" },
					new ColumnField() { Id = "Column2", Name = "Column2", Description = "This is Column2" },
				],
				X = 400,
				Y = 200,
			});

			_source.Lines.Add(new YCodeLineViewModel()
			{
				SourceId = "A",
				TargetId = "B"
			});
		}

		public YCodeSourceViewModel Source
		{
			get { return _source; }
			set { this.OnPropertyChanged(ref _source, value); }
		}
	}
}
