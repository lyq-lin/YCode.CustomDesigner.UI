using System.Collections.ObjectModel;
using YCode.CustomDesigner.UI;

namespace YCode.CustomDesigner.Demo;

public class YCodeContext : YCodeNotifyPropertyChanged
{
    private ObservableCollection<object> _children = [];
    private bool _isExpand;

    public ObservableCollection<object> Children
    {
        get => _children;
        set => this.OnPropertyChanged(ref _children, value);
    }

    public bool IsExpand
    {
        get => _isExpand;
        set => this.OnPropertyChanged(ref _isExpand, value);
    }
}