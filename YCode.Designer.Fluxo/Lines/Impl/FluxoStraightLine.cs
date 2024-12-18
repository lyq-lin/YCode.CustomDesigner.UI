namespace YCode.Designer.Fluxo;

public class FluxoStraightLine(FluxoDesigner designer) : FluxoBaseLine(LineType.Straight, designer)
{
    public override void DrawLine(FluxoLineParameter @params, StreamGeometryContext context)
    {
        base.DrawLine(@params, context);

        var start = this.Points.FirstOrDefault();

        var end = this.Points.LastOrDefault();

        context.BeginFigure(start, false, false);
        context.LineTo(start, true, true);
        context.LineTo(end, true, true);
    }

    protected override void OnHorizontal()
    {
        this.Parameter.Start = this.Parameter.Source.Left;

        this.Parameter.End = this.Parameter.Target.Left;

        if (this.Parameter.Source.Right.X < this.Parameter.Target.Left.X)
        {
            this.Parameter.Start = this.Parameter.Source.Right;
        }
        else if (this.Parameter.Target.Right.X < this.Parameter.Source.Left.X)
        {
            this.Parameter.End = this.Parameter.Target.Right;
        }

        this.Points.AddRanage([this.Parameter.Start, this.Parameter.End]);
    }

    protected override void OnVertical()
    {
        this.Parameter.Start = this.Parameter.Source.Top;

        this.Parameter.End = this.Parameter.Target.Top;

        if (this.Parameter.Source.Bottom.X < this.Parameter.Target.Top.X)
        {
            this.Parameter.Start = this.Parameter.Source.Bottom;
        }
        else if (this.Parameter.Target.Bottom.X < this.Parameter.Source.Top.X)
        {
            this.Parameter.End = this.Parameter.Target.Bottom;
        }

        this.Points.AddRanage([this.Parameter.Start, this.Parameter.End]);
    }

    protected override void OnCross()
    {
        throw new NotImplementedException();
    }

    public override Point GetPoint(double target)
    {
        var start = this.Points.FirstOrDefault();
        var end = this.Points.LastOrDefault();

        var x = start.X + (end.X - start.X) * target;
        var y = start.Y + (end.Y - start.Y) * target;

        return new Point(x, y);
    }

    public override double GetLength()
    {
        var start = this.Points.FirstOrDefault();
        var end = this.Points.LastOrDefault();

        return Math.Sqrt(Math.Pow(start.X - end.X, 2) + Math.Pow(start.Y - end.Y, 2));
    }
}