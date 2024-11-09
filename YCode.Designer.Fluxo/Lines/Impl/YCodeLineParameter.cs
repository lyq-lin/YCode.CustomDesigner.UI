namespace YCode.Designer.Fluxo;

using Position = (Point Left, Point Right, Point Top, Point Bottom);

public class YCodeLineParameter((Point, Point, Point, Point) source, (Point, Point, Point, Point) target)
{
    public Point Start { get; set; }
    public Point End { get; set; }


    public Position Source = source;
    public Position Target = target;
}