namespace Projekt.Views.UserControls;

public partial class Input : ContentView
{
    public static readonly BindableProperty PlaceholderProperty =
           BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(Input), string.Empty);

    public static readonly BindableProperty IconSourceProperty =
          BindableProperty.Create(nameof(IconSource), typeof(string), typeof(Input), string.Empty);

    public static readonly BindableProperty TextProperty =
         BindableProperty.Create(nameof(Text), typeof(string), typeof(Input), string.Empty);

    public static readonly BindableProperty ErrorMessageProperty =
        BindableProperty.Create(nameof(ErrorMessage), typeof(List<string>), typeof(Input), new List<string> { });

    public static readonly BindableProperty EnabledProperty =
      BindableProperty.Create(nameof(Enabled), typeof(bool), typeof(Input), true);

    public string Placeholder
    {
        get { return (string)GetValue(PlaceholderProperty); }
        set { SetValue(PlaceholderProperty, value); }
    }
    public string IconSource
    {
        get { return (string)GetValue(IconSourceProperty); }
        set { SetValue(IconSourceProperty, value); }
    }
    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }
    public List<string> ErrorMessage
    {
        get {
            var result = (List<string>)GetValue(ErrorMessageProperty);
            return result.Count() == 0 ? null : result;
        }
        set { SetValue(ErrorMessageProperty, value); }
    }

    public bool Enabled
    {
        get { return (bool)GetValue(EnabledProperty); }
        set { SetValue(EnabledProperty, value); }
    }

    public Input()
	{
		InitializeComponent();
        this.BindingContext = this;
	}
}