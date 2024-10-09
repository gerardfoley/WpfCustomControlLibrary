namespace WpfCustomControlLibrary;

internal sealed class ThicknesConverter : IValueConverter
{
    public double? Left { get; set; }
    public double? Top { get; set; }
    public double? Right { get; set; }
    public double? Bottom { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Thickness thickness)
        {
            return new Thickness(
                Left ?? thickness.Left,
                Top ?? thickness.Top,
                Right ?? thickness.Right,
                Bottom ?? thickness.Bottom);
        }

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}