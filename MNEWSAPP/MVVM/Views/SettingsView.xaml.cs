namespace MNEWSAPP.MVVM.Views;

public partial class SettingsView : ContentPage
{
    public SettingsView()
    {
        InitializeComponent();
    }

    private async void ApiKeySettingsTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(SetApiKeyView)}");
    }

    private async void TextSizeTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(ChangeFontSizeView)}");
    }

    private async void LinkedInTapped(object sender, TappedEventArgs e)
    {
        string? url = "https://www.linkedin.com/in/micheal-shodamola-4400b528b/";
        
            var navigationParameter = new Dictionary<string, object> { { "url", url } };
            await Shell.Current.GoToAsync($"{nameof(WebPageView)}", navigationParameter);
        
    }
}