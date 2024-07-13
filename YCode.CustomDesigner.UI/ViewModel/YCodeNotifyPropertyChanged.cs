using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace YCode.CustomDesigner.UI
{
	public class YCodeNotifyPropertyChanged : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		public virtual void OnPropertyChanged(string property)
		{
			var parameter = new PropertyChangedEventArgs(property);

			this.PropertyChanged?.Invoke(this, parameter);
		}

		public virtual bool OnPropertyChanged<TValue>(ref TValue field, TValue value, [CallerMemberName] string property = "")
		{
			if (!Equals(field, value))
			{
				this.OnPropertyChanged(property);

				field = value;

				this.OnPropertyChanged(property);

				return true;
			}

			return false;
		}
	}
}
