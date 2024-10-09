namespace WpfCustomControlLibrary;

public static class VisualStateChangedEventArgsExtensions
{
    public static bool IsChanging(
        this VisualStateChangedEventArgs visualStateChangedEventArgs,
        string? from,
        string? to)
    {
        return
            visualStateChangedEventArgs.OldState?.Name == from &&
            visualStateChangedEventArgs.NewState?.Name == to;
    }
}