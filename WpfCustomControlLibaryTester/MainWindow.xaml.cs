using System.Windows;
using WpfCustomControlLibrary;

namespace WpfCustomControlLibaryTester;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }


    private void OpenContentDialogButton_Click(object sender, RoutedEventArgs e)
    {
        var cd = new ContentDialog
        {
            Title = new Person { FirstName = "Ger", Surname = "Foley" },
            Content = "This is a test dialog.",
            CloseButtonText = "Close"
        };

        cd.ShowAsync();
    }

    private void OpenXamlContentDialogButton_Click(object sender, RoutedEventArgs e)
    {
        XamlContnetDialog.ShowAsync();
    }
}

class Person
{
    public string? FirstName { get; set; }
    public string? Surname { get; set; }
}