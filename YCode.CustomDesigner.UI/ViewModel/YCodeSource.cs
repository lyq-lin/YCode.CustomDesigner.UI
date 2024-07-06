using System.Collections.ObjectModel;
using YCode.CustomDesigner.UI.ViewModel;

namespace YCode.CustomDesigner.UI
{
	public class YCodeSource : YCodeNotifyPropertyChanged
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
