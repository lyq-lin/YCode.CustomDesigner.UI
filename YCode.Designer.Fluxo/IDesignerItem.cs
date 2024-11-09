namespace YCode.Designer.Fluxo;

public interface IDesignerItem
{
    Point Location { get; }

    Size DesiredSize { get; }

    void Arrange(Rect rect);
}