﻿using YCode.CustomDesigner.UI.ViewModel;

namespace YCode.CustomDesigner.UI
{
	public class YCodeNodeViewModel : YCodeNotifyPropertyChanged
	{
		private string _id = String.Empty;
		private string _name = String.Empty;
		private double _x;
		private double _y;

		public double Y
		{
			get { return _y; }
			set { this.OnPropertyChanged(ref _y, value); }
		}

		public double X
		{
			get { return _x; }
			set { this.OnPropertyChanged(ref _x, value); }
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
