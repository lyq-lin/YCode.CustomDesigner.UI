namespace YCode.Designer.Fluxo;

internal abstract class FluxoBaseLine(LineType type, FluxoDesigner designer) : IFluxoLineGeometry
{
    protected readonly FluxoDesigner _designer = designer;
    public LineType Type { get; } = type;

    public FluxoLineParameter Parameter { get; set; } = default!;
    protected List<Point> Points { get; } = [];

    public virtual void DrawLine(FluxoLineParameter @params, StreamGeometryContext context)
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