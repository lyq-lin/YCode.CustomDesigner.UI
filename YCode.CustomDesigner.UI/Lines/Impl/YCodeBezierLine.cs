namespace YCode.CustomDesigner.UI;

internal class YCodeBezierLine(YCodeDesigner designer) : YCodeBaseLine(LineType.Bezier, designer)
{
    public override void DrawLine(YCodeLineParameter @params, StreamGeometryContext context)
    {
        base.DrawLine(@params, context);

        var start = this.Points.FirstOrDefault();

        var end = this.Points.LastOrDefault();

        context.BeginFigure(start, false, false);
        context.LineTo(start, true, true);
        context.BezierTo(this.Points[1], this.Points[2], end, true, true);
        context.LineTo(end, true, true);
    }

    protected internal override void OnHorizontal()
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

    protected internal override void OnVertical()
    {
        throw new NotImplementedException();
    }

    protected internal override void OnQuartet()
    {
        throw new NotImplementedException();
    }

    public override Point GetPoint(double target)
    {
        throw new NotImplementedException();
    }

    public override double GetLength()
    {
        throw new NotImplementedException();
    }
}