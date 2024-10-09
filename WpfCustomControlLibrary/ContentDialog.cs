namespace WpfCustomControlLibrary;

public sealed class ContentDialog : ContentControl
{
    private TaskCompletionSource<ContentDialogResult>? _tcs;
    private ContentDialogResult _result;
    private object? _mainWindowContent;
    private object? _rootGridContent;

    static ContentDialog()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(ContentDialog),
            new FrameworkPropertyMetadata(typeof(ContentDialog)));
    }

    // Title

    public object? Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(
            nameof(Title),
            typeof(object),
            typeof(ContentDialog),
            new PropertyMetadata(null));

    // TitleTemplate

    public DataTemplate TitleTemplate
    {
        get { return (DataTemplate)GetValue(TitleTemplateProperty); }
        set { SetValue(TitleTemplateProperty, value); }
    }

    public static readonly DependencyProperty TitleTemplateProperty =
        DependencyProperty.Register(
            nameof(TitleTemplate),
            typeof(DataTemplate),
            typeof(ContentDialog), 
            new PropertyMetadata(null));

    // CornerRadius

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public static readonly DependencyProperty CornerRadiusProperty =
        Border.CornerRadiusProperty.AddOwner(typeof(ContentDialog));

    // ButtonPanelBrush

    public Brush ButtonPanelBrush
    {
        get => (Brush)GetValue(ButtonPanelBrushProperty);
        set => SetValue(ButtonPanelBrushProperty, value);
    }

    public static readonly DependencyProperty ButtonPanelBrushProperty =
        DependencyProperty.Register(
            nameof(ButtonPanelBrush),
            typeof(Brush),
            typeof(ContentDialog),
            new PropertyMetadata(Brushes.White));

    // PrimaryButtonText

    public string? PrimaryButtonText
    {
        get => (string)GetValue(PrimaryButtonTextProperty);
        set => SetValue(PrimaryButtonTextProperty, value);
    }

    public static readonly DependencyProperty PrimaryButtonTextProperty =
        DependencyProperty.Register(
            nameof(PrimaryButtonText),
            typeof(string),
            typeof(ContentDialog),
            new PropertyMetadata("", null, CoerceButtonText));

    // PrimaryButtonStyle

    public Style PrimaryButtonStyle
    {
        get => (Style)GetValue(PrimaryButtonStyleProperty);
        set => SetValue(PrimaryButtonStyleProperty, value);
    }

    public static readonly DependencyProperty PrimaryButtonStyleProperty =
        DependencyProperty.Register(
            nameof(PrimaryButtonStyle),
            typeof(Style),
            typeof(ContentDialog),
            new PropertyMetadata(null));

    // PrimaryButtonCommand

    public ICommand PrimaryButtonCommand
    {
        get => (ICommand)GetValue(PrimaryButtonCommandProperty);
        set => SetValue(PrimaryButtonCommandProperty, value);
    }

    public static readonly DependencyProperty PrimaryButtonCommandProperty =
        DependencyProperty.Register(
            nameof(PrimaryButtonCommand),
            typeof(ICommand),
            typeof(ContentDialog),
            new PropertyMetadata(null));

    // PrimaryButtonCommandParameter

    public object? PrimaryButtonCommandParameter
    {
        get => GetValue(PrimaryButtonCommandParameterProperty);
        set => SetValue(PrimaryButtonCommandParameterProperty, value);
    }

    public static readonly DependencyProperty PrimaryButtonCommandParameterProperty =
        DependencyProperty.Register(nameof(PrimaryButtonCommandParameter), 
            typeof(object), 
            typeof(ContentDialog),
            new PropertyMetadata(null));

    // IsPrimaryButtonEnabled

    public bool IsPrimaryButtonEnabled
    {
        get => (bool)GetValue(IsPrimaryButtonEnabledProperty);
        set => SetValue(IsPrimaryButtonEnabledProperty, value);
    }

    public static readonly DependencyProperty IsPrimaryButtonEnabledProperty =
        DependencyProperty.Register(
            nameof(IsPrimaryButtonEnabled), 
            typeof(bool),
            typeof(ContentDialog),
            new PropertyMetadata(true));

    // CloseButtonText

    public string? CloseButtonText
    {
        get => (string)GetValue(CloseButtonTextProperty);
        set => SetValue(CloseButtonTextProperty, value);
    }

    public static readonly DependencyProperty CloseButtonTextProperty =
        DependencyProperty.Register(
            nameof(CloseButtonText),
            typeof(string),
            typeof(ContentDialog),
            new PropertyMetadata("", null, CoerceButtonText));

    // CloseButtonStyle

    public Style CloseButtonStyle
    {
        get => (Style)GetValue(CloseButtonStyleProperty);
        set => SetValue(CloseButtonStyleProperty, value);
    }

    public static readonly DependencyProperty CloseButtonStyleProperty =
        DependencyProperty.Register(
            nameof(CloseButtonStyle),
            typeof(Style),
            typeof(ContentDialog),
            new PropertyMetadata(null));

    // CloseButtonCommand

    public ICommand CloseButtonCommand
    {
        get => (ICommand)GetValue(CloseButtonCommandProperty);
        set => SetValue(CloseButtonCommandProperty, value);
    }

    public static readonly DependencyProperty CloseButtonCommandProperty =
        DependencyProperty.Register(
            nameof(CloseButtonCommand),
            typeof(ICommand),
            typeof(ContentDialog),
            new PropertyMetadata(null));

    // CloseButtonCommandParameter

    public object? CloseButtonCommandParameter
    {
        get => GetValue(CloseButtonCommandParameterProperty);
        set => SetValue(CloseButtonCommandParameterProperty, value);
    }

    public static readonly DependencyProperty CloseButtonCommandParameterProperty =
        DependencyProperty.Register(nameof(CloseButtonCommandParameter),
            typeof(object),
            typeof(ContentDialog),
            new PropertyMetadata(null));

    // IsLightDismissEnabled

    public bool IsLightDismissEnabled
    {
        get { return (bool)GetValue(IsLightDismissEnabledProperty); }
        set { SetValue(IsLightDismissEnabledProperty, value); }
    }

    public static readonly DependencyProperty IsLightDismissEnabledProperty =
        DependencyProperty.Register(
            nameof(IsLightDismissEnabled),
            typeof(bool),
            typeof(ContentDialog),
            new PropertyMetadata(true));

    // BackgroundContent

    public object? BackgroundContent
    {
        get => GetValue(BackgroundContentProperty);
        private set => SetValue(BackgroundContentKey, value);
    }

    private static readonly DependencyPropertyKey BackgroundContentKey =
        DependencyProperty.RegisterReadOnly(
            nameof(BackgroundContent),
            typeof(object),
            typeof(ContentDialog),
            new PropertyMetadata(null));

    public static readonly DependencyProperty BackgroundContentProperty =
        BackgroundContentKey!.DependencyProperty;

    private ContentControl? _backgroundContentControl;
    [SuppressMessage("CodeQuality", "IDE0052:Remove unread private members")]
    private ContentControl? BackgroundContentControl
    {
        get => _backgroundContentControl;
        set
        {
            if (_backgroundContentControl != null)
            {
                _backgroundContentControl.PreviewGotKeyboardFocus -=
                    BackgroundContentControl_PreviewGotKeyboardFocus;
            }
            _backgroundContentControl = value;
            if (_backgroundContentControl != null)
            {
                _backgroundContentControl.PreviewGotKeyboardFocus +=
                    BackgroundContentControl_PreviewGotKeyboardFocus;
            }
        }
    }

    private Grid? _rootGrid;
    private Grid? RootGrid
    {
        get => _rootGrid;
        set
        {
            if (_rootGrid != null)
            {
                _rootGrid.Loaded -= RootGrid_Loaded;
            }

            _rootGrid = value;

            if (_rootGrid != null)
            {
                _rootGrid.Loaded += RootGrid_Loaded;
            }
        }
    }

    private Button? _primaryButton;
    private Button? PrimaryButton
    {
        get => _primaryButton;
        set
        {
            if (_primaryButton != null)
            {
                _primaryButton.Click -= PrimaryButton_Click;
            }
            _primaryButton = value;
            if (_primaryButton != null)
            {
                _primaryButton.Click += PrimaryButton_Click;
            }
        }
    }

    private Button? _closeButton;
    private Button? CloseButton
    {
        get => _closeButton;
        set
        {
            if (_closeButton != null)
            {
                _closeButton.Click -= CloseButton_Click;
            }
            _closeButton = value;
            if (_closeButton != null)
            {
                _closeButton.Click += CloseButton_Click;
            }
        }
    }

    private Button? _overlay;
    [SuppressMessage("CodeQuality", "IDE0052:Remove unread private members")]
    private Button? Overlay
    {
        get => _overlay;
        set
        {
            if (_overlay != null)
            {
                _overlay.Click -= Overlay_Click;
            }
            _overlay = value;
            if (_overlay != null)
            {
                _overlay.Click += Overlay_Click;
            }
        }
    }

    private ContentControl? _root;
    private ContentControl? Root
    {
        get => _root;
        set
        {
            UpdateCurrentStateChangedHandler(_root, value);
            _root = value;
        }
    }

    private Border? _dialogBorder;
    private Border? DialogBorder
    {
        get => _dialogBorder;
        set => _dialogBorder = value;
    }

    public override void OnApplyTemplate()
    {
        Root = GetTemplateChild("Root") as ContentControl;
        BackgroundContentControl = GetTemplateChild("BackgroundContentControl") as ContentControl;
        RootGrid = GetTemplateChild("RootGrid") as Grid;
        DialogBorder = GetTemplateChild("DialogBorder") as Border;
        CloseButton = GetTemplateChild("CloseButton") as Button;
        PrimaryButton = GetTemplateChild("PrimaryButton") as Button;
        Overlay = GetTemplateChild("Overlay") as Button;
    }

    public Task<ContentDialogResult> ShowAsync()
    {
        if (_tcs != null) { throw new InvalidOperationException(); }

        ApplyTemplate();

        _mainWindowContent = Application.Current.MainWindow.Content;
        _rootGridContent = RootGrid;

        if (Root != null)
        {
            Root.Content = null;
        }

        Application.Current.MainWindow.Content = _rootGridContent;
        BackgroundContent = _mainWindowContent;

        VisualStateManager.GoToState(this, "Show", true);

        _tcs = new TaskCompletionSource<ContentDialogResult>();
        return _tcs.Task;
    }

    public void Hide(ContentDialogResult result)
    {
        _result = result;
        VisualStateManager.GoToState(this, "Hide", true);
    }

    private static object CoerceButtonText(DependencyObject d, object value)
    {
        return value ?? "";
    }

    private void BackgroundContentControl_PreviewGotKeyboardFocus(
        object sender,
        KeyboardFocusChangedEventArgs e)
    {
        e.Handled = true;
    }

    private void UpdateCurrentStateChangedHandler(FrameworkElement? oldElement, FrameworkElement? newElement)
    {
        if (oldElement != null)
        {
            var vsg = VisualStateManagerUtils.GetVisualStateGroup(oldElement, "CommonStates");

            if (vsg != null)
            {
                vsg.CurrentStateChanged -= CurrentStateChanged;
            }
        }

        if (newElement != null)
        {
            var vsg = VisualStateManagerUtils.GetVisualStateGroup(newElement, "CommonStates");

            if (vsg != null)
            {
                vsg.CurrentStateChanged += CurrentStateChanged;
            }
        }
    }

    private void RootGrid_Loaded(object sender, RoutedEventArgs e)
    {
        DialogBorder?.Focus();
        CloseButton?.Focus();
        PrimaryButton?.Focus();
    }

    private void CurrentStateChanged(object? sender, VisualStateChangedEventArgs e)
    {
        if (_tcs == null) { return; }

        if (!e.IsChanging("Show", "Hide")) { return; }

        if (Root != null)
        {
            Root.Content = _rootGridContent;
        }

        Application.Current.MainWindow.Content = _mainWindowContent;
        BackgroundContent = null;

        _tcs.SetResult(_result);
        _tcs = null;

        Application.Current.MainWindow.ContentRendered += MainWindow_ContentRendered;
    }

    private void MainWindow_ContentRendered(object? sender, EventArgs e)
    {
        FocusManager.GetFocusedElement(Application.Current.MainWindow)?.Focus();
        Application.Current.MainWindow.ContentRendered -= MainWindow_ContentRendered;
    }

    private void Overlay_Click(object sender, RoutedEventArgs e)
    {
        if (!IsLightDismissEnabled) return;
        Hide(ContentDialogResult.None);
    }

    private void PrimaryButton_Click(object sender, RoutedEventArgs e)
    {
        Hide(ContentDialogResult.Primary);
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Hide(ContentDialogResult.Close);
    }
}