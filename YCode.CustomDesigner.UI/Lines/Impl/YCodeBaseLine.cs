namespace YCode.CustomDesigner.UI;

internal abstract class YCodeBaseLine(LineType type, YCodeDesigner designer) : IYCodeLineGeometry
{
    protected readonly YCodeDesigner _designer = designer;
    public LineType Type { get; } = type;

    public YCodeLineParameter Parameter { get; set; } = default!;
    protected List<Point> Points { get; } = [];

    public virtual void DrawLine(YCodeLineParameter @params, StreamGeometryContext context)
    {
        //TODO: 算法策略

        this.Parameter = @params;

        this.Points.Clear();

        this.OnHorizontal();
    }

    protected abstract void OnHorizontal();

    protected abstract void OnVertical();

    protected abstract void OnQuartet();

    public abstract Point GetPoint(double target);

    public abstract double GetLength();
}