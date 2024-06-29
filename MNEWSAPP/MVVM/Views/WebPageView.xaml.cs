namespace MNEWSAPP.MVVM.Views;

[QueryProperty(nameof(Url), "url")]
public partial class WebPageView : ContentPage
{
    private string? _url;

    public string? Url
    {
        get => _url;
        set
        {
            _url = value;
            OnPropertyChanged();
        }
    }

    public WebPageView()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        mnewsWebView.Source = Url;
    }


}
