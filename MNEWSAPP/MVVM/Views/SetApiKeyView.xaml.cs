using MNEWSAPP.Service;

namespace MNEWSAPP.MVVM.Views
{
    public partial class SetApiKeyView : ContentPage
    {
        private readonly ApiKeyService _apiKeyService;
        public SetApiKeyView(ApiKeyService apiKeyService)
        {
            _apiKeyService = apiKeyService;
            InitializeComponent();
        }

        private async void SetApiKeyButton_Clicked(object sender, EventArgs e)
        {

            string apiKey = ApiKeyEntry.Text;


            if (string.IsNullOrEmpty(apiKey))
            {
                await DisplayAlert("Error", "Please enter a valid API key.", "OK");
                return;
            }
            await _apiKeyService.SetApiKey(apiKey);
            await DisplayAlert("Success", "API key has been saved.", "OK");


            await Shell.Current.GoToAsync("//IndexPage");
        }

        public async void CheckApiKey()
        {
            await DisplayAlert("Error", "API key Not Set!", "OK");
            return;
        }
    }
}