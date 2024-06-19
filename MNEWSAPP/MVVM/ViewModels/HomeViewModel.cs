using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using MNEWSAPP.MVVM.Models;
using System.Net.Http.Headers;

namespace MNEWSAPP.MVVM.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://newsapi.org/v2/top-headlines?sources=techcrunch&apiKey=";

        [ObservableProperty]
        private ObservableCollection<ArticleModel>? news;

        public HomeViewModel()
        {
            _httpClient = new HttpClient();
            News = [];
        }

        public static async Task SetApiKeyAsync(string apiKeyValue)
        {
            await SecureStorage.SetAsync("apiKey", apiKeyValue);
        }

        public async Task GetNews()
        {
            // Ensure the API key is set only once
            var apiKey = await SecureStorage.GetAsync("apiKey");
            if (string.IsNullOrEmpty(apiKey))
            {
                await SetApiKeyAsync("d9ab75985be34739a45d52acfc196efa");
                apiKey = await SecureStorage.GetAsync("apiKey");
            }

            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("API key is not set in the configuration.");
            }

            string url = $"{_baseUrl}{apiKey}";
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("MN News/1.0");

            // Execute the network request asynchronously
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<ResponseModel>(responseString);

                // Update the ObservableCollection on the main thread
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    News?.Clear();
                    if (data?.Articles != null)
                    {
                        foreach (var article in data.Articles)
                        {
                            News?.Add(article);
                        }
                    }
                });
            }
        }
    }
}
