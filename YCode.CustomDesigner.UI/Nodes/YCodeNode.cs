namespace YCode.CustomDesigner.UI;

public class YCodeNode:ContentControl
{
    static YCodeNode()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(YCodeNode),
            new FrameworkPropertyMetadata(typeof(YCodeNode))
        );
    }

    public YCodeNode()
    {
        
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
    }
}