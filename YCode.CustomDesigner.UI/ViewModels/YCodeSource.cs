using System.Collections.ObjectModel;

namespace YCode.CustomDesigner.UI;

public class YCodeSource : YCodeNotifyPropertyChanged
{
    private ObservableCollection<YCodeNodeViewModel> _nodes = [];
    private ObservableCollection<YCodeLineViewModel> _lines = [];

    public ObservableCollection<YCodeNodeViewModel> Nodes
    {
        get => _nodes;
        set => this.OnPropertyChanged(ref _nodes, value);
    }

    public ObservableCollection<YCodeLineViewModel> Lines
    {
        get => _lines;
        set => this.OnPropertyChanged(ref _lines, value);
    }
}