namespace YCode.Designer.Fluxo;

public class FluxoNodeDragCompletedEventArgs(FluxoNode node) : EventArgs
{
    public UIElement Node { get; } = node;
}