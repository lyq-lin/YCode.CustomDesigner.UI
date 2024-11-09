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
}