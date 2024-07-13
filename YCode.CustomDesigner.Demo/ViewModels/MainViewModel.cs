using YCode.CustomDesigner.UI;
using YCode.CustomDesigner.UI.ViewModel;

namespace YCode.CustomDesigner.Demo
{
	internal class MainViewModel : YCodeNotifyPropertyChanged
	{
		private YCodeSourceViewModel _source;

		public MainViewModel()
		{
			_source = new YCodeSourceViewModel();

			_source.Nodes.Add(new YCodeNodeViewModel()
			{
				Id = "A",
				Name = "AAA",
				X = 100,
				Y = 100,
			});

			_source.Nodes.Add(new YCodeNodeViewModel()
			{
				Id = "B",
				Name = "BBB",
				X = 300,
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
