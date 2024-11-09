using System.Collections.ObjectModel;

namespace YCode.Designer.Fluxo;

public class FluxoSource : FluxoNotifyPropertyChanged
{
    private ObservableCollection<FluxoNodeViewModel> _nodes = [];
    private ObservableCollection<FluxoLineViewModel> _lines = [];

    public FluxoSource()
    {
        BindingOperations.EnableCollectionSynchronization(this.Nodes, this.Nodes);
        
        BindingOperations.EnableCollectionSynchronization(this.Lines, this.Lines);
    }

    public ObservableCollection<FluxoNodeViewModel> Nodes
    {
        get => _nodes;
        set => this.OnPropertyChanged(ref _nodes, value);
    }

    public ObservableCollection<FluxoLineViewModel> Lines
    {
        get => _lines;
        set => this.OnPropertyChanged(ref _lines, value);
    }
}