namespace YCode.Designer.Fluxo;

public abstract class FluxoBaseLine(LineType type, FluxoDesigner designer) : IFluxoLineGeometry
{
    private readonly FluxoDesigner _designer = designer;

    public LineType Type { get; } = type;

    protected FluxoLineParameter Parameter { get; set; } = default!;
    protected List<Point> Points { get; } = [];

    public virtual void DrawLine(FluxoLineParameter @params, StreamGeometryContext context)
    {
        this.Points.Clear();

        this.Parameter = @params;

        switch (_designer.Orientation)
        {
            case FluxoLayoutOrientation.Cross:
            {
                this.OnCross();
            }
                break;
            case FluxoLayoutOrientation.Vertical:
            {
                this.OnVertical();
            }
                break;
            case FluxoLayoutOrientation.Horizontal:
            default:
            {
                this.OnHorizontal();
            }
                break;
        }
    }

    protected abstract void OnHorizontal();

    protected abstract void OnVertical();

    protected abstract void OnCross();

    public abstract Point GetPoint(double target);

    public abstract double GetLength();
}