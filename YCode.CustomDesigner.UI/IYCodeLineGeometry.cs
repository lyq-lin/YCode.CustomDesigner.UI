namespace YCode.CustomDesigner.UI;

public interface IYCodeLineGeometry
{
    LineType Type { get; }

    void DrawLine(YCodeLineParameter @params,StreamGeometryContext context);

    Point GetPoint(double target);

    double GetLength();
}