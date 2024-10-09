namespace WpfCustomControlLibrary;

internal sealed class CornerRadiusConverter : IValueConverter
{
    public double? TopLeft { get; set; }
    public double? TopRight { get; set; }
    public double? BottomRight { get; set; }
    public double? BottomLeft { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is CornerRadius cornerRadius)
        {
            return new CornerRadius(
                TopLeft ?? cornerRadius.TopLeft,
                TopRight ?? cornerRadius.TopRight,
                BottomRight ?? cornerRadius.BottomRight,
                BottomLeft ?? cornerRadius.BottomLeft);
        }

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}