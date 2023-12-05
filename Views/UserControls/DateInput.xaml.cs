namespace Projekt.Views.UserControls;

public partial class DateInput : ContentView
{
    public static readonly BindableProperty IconSourceProperty =
          BindableProperty.Create(nameof(IconSource), typeof(string), typeof(Input), string.Empty);

    public static readonly BindableProperty DateProperty =
         BindableProperty.Create(nameof(Date), typeof(DateTime?), typeof(Input), null);

    public static readonly BindableProperty ErrorMessageProperty =
        BindableProperty.Create(nameof(ErrorMessage), typeof(List<string>), typeof(Input), new List<string> { });

    public static readonly BindableProperty EnabledProperty =
      BindableProperty.Create(nameof(Enabled), typeof(bool), typeof(Input), true);

    public string IconSource
    {
        get { return (string)GetValue(IconSourceProperty); }
        set { SetValue(IconSourceProperty, value); }
    }
    public DateTime? Date
    {
        get { return (DateTime?)GetValue(DateProperty); }
        set { SetValue(DateProperty, value); }
    }
    public List<string> ErrorMessage
    {
        get
        {
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
    public DateInput()
	{
		InitializeComponent();
        this.BindingContext = this;
	}
}