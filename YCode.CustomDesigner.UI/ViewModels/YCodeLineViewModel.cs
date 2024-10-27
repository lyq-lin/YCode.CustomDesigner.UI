namespace YCode.CustomDesigner.UI;

public class YCodeLineViewModel : YCodeNotifyPropertyChanged
{
    private string _prevId = String.Empty;
    private string _nextId = String.Empty;
    private object? _context;

    public object? Context
    {
        get => _context;
        set => this.OnPropertyChanged(ref _context, value);
    }

    public string PrevId
    {
        get => _prevId;
        set => this.OnPropertyChanged(ref _prevId, value);
    }

    public string NextId
    {
        get => _nextId;
        set => this.OnPropertyChanged(ref _nextId, value);
    }
}