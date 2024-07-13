using YCode.CustomDesigner.UI;

namespace YCode.CustomDesigner.Demo
{
	public class ColumnField : YCodeNotifyPropertyChanged, IYCodeColumn
	{
		private string _id;

		private string _name = String.Empty;

		private string? _description;

		public ColumnField()
		{
			_id = DateTime.Now.Ticks.ToString("x");
		}

		public string? Description
		{
			get { return _description; }
			set { this.OnPropertyChanged(ref _description, value); }
		}

		public string Name
		{
			get { return _name; }
			set { this.OnPropertyChanged(ref _name, value); }
		}

		public string Id
		{
			get { return _id; }
			set { this.OnPropertyChanged(ref _id, value); }
		}
	}
}
