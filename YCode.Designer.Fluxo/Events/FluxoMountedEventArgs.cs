namespace YCode.Designer.Fluxo;

public class FluxoMountedEventArgs(FluxoSource? source) : EventArgs
{
    public FluxoSource? Source { get; } = source;
}