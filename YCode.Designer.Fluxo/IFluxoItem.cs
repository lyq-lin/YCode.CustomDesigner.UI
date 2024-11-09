namespace YCode.Designer.Fluxo;

public interface IFluxoItem
{
    Point Location { get; }

    Size DesiredSize { get; }

    void Arrange(Rect rect);
}