namespace YCode.Designer.Fluxo;

public class FluxoNodeDragStartedEventArgs(FluxoNode node) : EventArgs
{
    public UIElement Node { get; } = node;
}