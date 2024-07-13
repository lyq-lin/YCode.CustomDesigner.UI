using System.Collections.ObjectModel;

namespace YCode.CustomDesigner.UI
{
	public class YCodeSourceViewModel : YCodeNotifyPropertyChanged
	{
		private ObservableCollection<YCodeNodeViewModel> _nodes = [];

		private ObservableCollection<YCodeLineViewModel> _lines = [];

		public ObservableCollection<YCodeLineViewModel> Lines
		{
			get { return _lines; }
			set { this.OnPropertyChanged(ref _lines, value); }
		}

		public ObservableCollection<YCodeNodeViewModel> Nodes
		{
			get { return _nodes; }
			set { this.OnPropertyChanged(ref _nodes, value); }
		}
	}
}
