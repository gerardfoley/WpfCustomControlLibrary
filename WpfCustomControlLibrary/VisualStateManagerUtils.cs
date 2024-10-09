namespace WpfCustomControlLibrary;

public static class VisualStateManagerUtils
{
    public static VisualStateGroup? GetVisualStateGroup(
        FrameworkElement obj,
        string name)
    {
        return VisualStateManager.
            GetVisualStateGroups(obj).
            OfType<VisualStateGroup>().
            FirstOrDefault(x => x.Name == name);
    }
}