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
}