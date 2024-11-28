namespace YCode.Designer.Fluxo;

public class FluxoLineParameter(FluxoPoint source, FluxoPoint target)
{
    public Point Start { get; set; }
    public Point End { get; set; }

    public FluxoPoint Source { get; set; } = source;
    public FluxoPoint Target { get; set; } = target;

    public FluxoLinePosition SourcePosition { get; set; } = FluxoLinePosition.None;
    public FluxoLinePosition TargetPosition { get; set; } = FluxoLinePosition.None;
}