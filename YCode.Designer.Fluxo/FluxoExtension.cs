namespace YCode.Designer.Fluxo;

public static class FluxoExtension
{
    public static void AddRanage<T>(this IList list, IEnumerable<T> collection)
    {
        if (list == null)
        {
            throw new ArgumentNullException(nameof(list));
        }

        if (collection == null)
        {
            throw new ArgumentNullException(nameof(collection));
        }

        if (list is List<T> listCopy)
        {
            listCopy.AddRange(collection);

            return;
        }

        foreach (var item in collection)
        {
            list.Add(item);
        }
    }

    internal static T? FindParent<T>(this UIElement element) where T : UIElement
    {
        if (element is T value)
        {
            return value;
        }
        else
        {
            var parent = VisualTreeHelper.GetParent(element);

            if (parent is UIElement last)
            {
                return last.FindParent<T>();
            }
        }

        return default;
    }

    internal static bool CaptureMouseSafe(this UIElement elem)
    {
        if (Mouse.Captured == null || elem.IsMouseCaptured)
        {
            return elem.CaptureMouse();
        }

        return false;
    }

    internal static UIElement GetPotentialElement<T>(this FluxoDesigner designer) where T : UIElement
    {
        var element = designer.ItemsHost.GetElementUnderMouse<T>();

        if (element != null)
        {
            return element;
        }

        return designer;
    }

    internal static T? GetElementUnderMouse<T>(this UIElement container)
        where T : UIElement
    {
        T? result = default;
        VisualTreeHelper.HitTest(container, depObj =>
        {
            if (depObj is UIElement elem && elem.IsHitTestVisible)
            {
                if (elem is T r)
                {
                    result = r;
                    return HitTestFilterBehavior.Stop;
                }

                return HitTestFilterBehavior.Continue;
            }

            return HitTestFilterBehavior.ContinueSkipSelfAndChildren;
        }, hitResult =>
        {
            if (hitResult.VisualHit is T r)
            {
                result = r;
                return HitTestResultBehavior.Stop;
            }

            return HitTestResultBehavior.Continue;
        }, new PointHitTestParameters(Mouse.GetPosition(container)));

        return result;
    }
}