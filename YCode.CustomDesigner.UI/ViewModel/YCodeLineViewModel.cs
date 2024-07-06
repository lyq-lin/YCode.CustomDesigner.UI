using YCode.CustomDesigner.UI.ViewModel;

namespace YCode.CustomDesigner.UI
{
	public class YCodeLineViewModel : YCodeNotifyPropertyChanged
	{
		private string _sourceId = String.Empty;

		private string _targetId = String.Empty;

		public string TargetId
		{
			get { return _targetId; }
			set { this.OnPropertyChanged(ref _targetId, value); }
		}

		public string SourceId
		{
			get { return _sourceId; }
			set { this.OnPropertyChanged(ref _sourceId, value); }
		}
	}
}
