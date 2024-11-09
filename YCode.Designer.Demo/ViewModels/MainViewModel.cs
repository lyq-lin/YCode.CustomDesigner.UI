using System.Collections.ObjectModel;
using System.Windows;
using YCode.Designer.Fluxo;

namespace YCode.Designer.Demo;

internal class MainViewModel : FluxoNotifyPropertyChanged
{
    private ObservableCollection<UIElement> _uis;

    private UIElement? _view;

    private bool _isChanged;

    public MainViewModel()
    {
        _uis = [new Playground(), new Performance()];

        _view = _uis[0];
    }

    public bool IsChanged
    {
        get => _isChanged;
        set
        {
            if (this.OnPropertyChanged(ref _isChanged, value))
            {
                this.View = _isChanged ? _uis[1] : _uis[0];
            }
        }
    }

    public UIElement? View
    {
        get => _view;
        set => this.OnPropertyChanged(ref _view, value);
    }

    public ObservableCollection<UIElement> Uis
    {
        get => _uis;
        set => this.OnPropertyChanged(ref _uis, value);
    }
}