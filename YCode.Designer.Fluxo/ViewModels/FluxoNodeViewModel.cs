namespace YCode.Designer.Fluxo;

public class FluxoNodeViewModel : FluxoNotifyPropertyChanged
{
    private string _id = String.Empty;
    private string _name = String.Empty;
    private string _description = String.Empty;
    private Point _location = default(Point);
    private object? _context;
    private bool _isEmpty;

    public string Id
    {
        get => _id;
        set => this.OnPropertyChanged(ref _id, value);
    }

    public string Name
    {
        get => _name;
        set => this.OnPropertyChanged(ref _name, value);
    }

    public string Description
    {
        get => _description;
        set => this.OnPropertyChanged(ref _description, value);
    }

    public Point Location
    {
        get => _location;
        set => this.OnPropertyChanged(ref _location, value);
    }

    public bool IsEmpty
    {
        get => _isEmpty;
        set => this.OnPropertyChanged(ref _isEmpty, value);
    }

    public object? Context
    {
        get => _context;
        set => this.OnPropertyChanged(ref _context, value);
    }
}