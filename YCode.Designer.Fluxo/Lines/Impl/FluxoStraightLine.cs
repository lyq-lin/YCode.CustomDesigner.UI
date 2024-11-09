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
        throw new NotImplementedException();
    }

    protected override void OnQuartet()
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