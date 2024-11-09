namespace YCode.Designer.Fluxo;

public interface IFluxoLineGeometry
{
    LineType Type { get; }

    void DrawLine(FluxoLineParameter @params,StreamGeometryContext context);

    Point GetPoint(double target);

    double GetLength();
}