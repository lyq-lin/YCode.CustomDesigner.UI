using System.Runtime.InteropServices.JavaScript;

namespace YCode.Designer.Fluxo;

public class FluxoLineViewModel : FluxoNotifyPropertyChanged
{
    private string _prevId = String.Empty;
    private string _nextId = String.Empty;
    private string _prevPort = String.Empty;
    private string _nextPort = String.Empty;
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

    public string PrevPort
    {
        get => _prevPort;
        set => this.OnPropertyChanged(ref _prevPort, value);
    }

    public string NextPort
    {
        get => _nextPort;
        set => this.OnPropertyChanged(ref _nextPort, value);
    }
}