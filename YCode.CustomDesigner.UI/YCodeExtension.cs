namespace YCode.CustomDesigner.UI;

public static class YCodeExtension
{
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
    
    public static bool CaptureMouseSafe(this UIElement elem)
    {
        if (Mouse.Captured == null || elem.IsMouseCaptured)
        {
            return elem.CaptureMouse();
        }

        return false;
    }
}