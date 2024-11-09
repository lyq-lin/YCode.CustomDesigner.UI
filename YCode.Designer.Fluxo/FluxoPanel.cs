namespace YCode.Designer.Fluxo;

internal class FluxoPanel : Panel
{
    #region Dependency properties

    public static readonly DependencyProperty ExtentProperty = DependencyProperty.Register(
        nameof(Extent), typeof(Rect), typeof(FluxoPanel), new PropertyMetadata(default(Rect)));

    public Rect Extent
    {
        get => (Rect)GetValue(ExtentProperty);
        set => SetValue(ExtentProperty, value);
    }

    #endregion

    protected override Size ArrangeOverride(Size finalSize)
    {
        var minX = Double.MaxValue;
        var minY = Double.MaxValue;

        var maxX = Double.MinValue;
        var maxY = Double.MinValue;

        var children = InternalChildren;
        
        for (var i = 0; i < children.Count; i++)
        {
            if (children[i] is IFluxoItem item)
            {
                item.Arrange(new Rect(item.Location, item.DesiredSize));

                var size = children[i].RenderSize;

                if (item.Location.X < minX)
                {
                    minX = item.Location.X;
                }

                if (item.Location.Y < minY)
                {
                    minY = item.Location.Y;
                }

                var sizeX = item.Location.X + size.Width;
                if (sizeX > maxX)
                {
                    maxX = sizeX;
                }

                var sizeY = item.Location.Y + size.Height;
                if (sizeY > maxY)
                {
                    maxY = sizeY;
                }
            }
        }

        Extent = minX == Double.MaxValue
            ? new Rect(0, 0, 0, 0)
            : new Rect(minX, minY, maxX - minX, maxY - minY);

        return finalSize;
    }

    protected override Size MeasureOverride(Size availableSize)
    {
        var constraint = new Size(Double.PositiveInfinity, Double.PositiveInfinity);

        var children = InternalChildren;

        for (var i = 0; i < children.Count; i++)
        {
            children[i].Measure(constraint);
        }

        return default;
    }
}