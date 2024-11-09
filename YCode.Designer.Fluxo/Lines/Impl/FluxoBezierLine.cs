namespace YCode.Designer.Fluxo;

internal class FluxoBezierLine(FluxoDesigner designer) : FluxoBaseLine(LineType.Bezier, designer)
{
    public override void DrawLine(FluxoLineParameter @params, StreamGeometryContext context)
    {
        base.DrawLine(@params, context);

        var start = this.Points.FirstOrDefault();

        var end = this.Points.LastOrDefault();

        context.BeginFigure(start, false, false);
        context.LineTo(start, true, true);
        context.BezierTo(this.Points[1], this.Points[2], end, true, true);
        context.LineTo(end, true, true);
    }

    protected override void OnHorizontal()
    {
        this.Parameter.Start = this.Parameter.Source.Left;

        this.Parameter.End = this.Parameter.Target.Left;

        var p1 = new Point(this.Parameter.Start.X - 50, this.Parameter.Start.Y);

        var p2 = new Point(this.Parameter.End.X - 50, this.Parameter.End.Y);

        if (this.Parameter.Source.Right.X < this.Parameter.Target.Left.X)
        {
            this.Parameter.Start = this.Parameter.Source.Right;

            p1.X = this.Parameter.Start.X + 50;

            p2.X -= 30;
        }
        else if (this.Parameter.Target.Right.X < this.Parameter.Source.Left.X)
        {
            this.Parameter.End = this.Parameter.Target.Right;

            p2.X = this.Parameter.End.X + 50;

            p1.X -= 30;
        }

        this.Points.AddRanage([this.Parameter.Start, p1, p2, this.Parameter.End]);
    }

    protected override void OnVertical()
    {
        throw new NotImplementedException();
    }

    protected override void OnQuartet()
    {
        throw new NotImplementedException();
    }

    public override Point GetPoint(double target)
    {
        var start = this.Points[0];
        var p1 = this.Points[1];
        var p2 = this.Points[2];
        var end = this.Points[3];

        var x = Math.Pow(1 - target, 3) * start.X
                + 3 * Math.Pow(1 - target, 2) * target * p1.X
                + 3 * (1 - target) * Math.Pow(target, 2) * p2.X
                + Math.Pow(target, 3) * end.X;

        var y = Math.Pow(1 - target, 3) * start.Y
                + 3 * Math.Pow(1 - target, 2) * target * p1.Y
                + 3 * (1 - target) * Math.Pow(target, 2) * p2.Y
                + Math.Pow(target, 3) * end.Y;

        return new Point(x, y);
    }

    public override double GetLength()
    {
        var pointCount = 30;

        var length = 0d;

        var lastPoint = this.GetPoint(0d / Convert.ToDouble(pointCount));

        for (var i = 1; i < pointCount; i++)
        {
            var point = this.GetPoint(Convert.ToDouble(i) / Convert.ToDouble(pointCount));

            length += Point.Subtract(point, lastPoint).Length;

            lastPoint = point;
        }

        return length;
    }
}