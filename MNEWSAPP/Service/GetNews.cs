using System.Net.Http;
using CommunityToolkit.Mvvm.ComponentModel;
using MNEWSAPP.MVVM.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MNEWSAPP.MVVM.Views;

namespace MNEWSAPP.Service
{
    public partial class GetNews : ObservableObject
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://newsapi.org/v2/everything?q=";
        private readonly SetApiKeyView _apiKeyView;

        public GetNews()
        {
            _apiKeyView = new SetApiKeyView(new ApiKeyService());
            _httpClient = new HttpClient();
            //SecureStorage.Remove("apiKey");

        }


        public async Task<ObservableCollection<ArticleModel>?> GetNewsAsync(string keyword)
        {
            var apiKey = await SecureStorage.GetAsync("apiKey");
            if (string.IsNullOrEmpty(apiKey))
            {
                await Shell.Current.GoToAsync($"{nameof(SetApiKeyView)}");
            }

            string url = $"{_baseUrl}{keyword}&apiKey={apiKey}";
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("MN News/1.0");

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<ResponseModel>(responseString);

                    var filteredArticles = data?.Articles?.Where(article => !string.IsNullOrEmpty(article.UrlToImage)).ToList();
                    var filteredData = new ResponseModel { Articles = filteredArticles };

                    var processedArticles = await Task.Run(async () =>
                    {
                        var articles = new List<ArticleModel>();
                        if (filteredData?.Articles != null)
                        {
                            foreach (var article in filteredData.Articles.Take(7))
                            {
                                article.UrlToImage = await ImageCache.GetImageFromCacheAsync(article.UrlToImage);
                                articles.Add(article);
                            }
                        }
                        return articles;
                    });

                    return new ObservableCollection<ArticleModel>(processedArticles);
                }
                else
                {

                    await HandleApiError(response);
                    return null;
                }
            }
            catch (Exception ex)
            {

                var message = ex.Message;
                var navigationParameters = new Dictionary<string, object>
                {
                    {"errorMessage", message }
                };
                await Shell.Current.GoToAsync($"{nameof(ConnectionErrorView)}", navigationParameters);
                return null;
            }
        }

        private async Task HandleApiError(HttpResponseMessage response)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();

            var navigationParameters = new Dictionary<string, object>
                {
                    {"errorMessage", errorMessage }
                };
            await Shell.Current.GoToAsync($"{nameof(ConnectionErrorView)}", navigationParameters);
        }

        private async Task SetApiKeyAsync(string apiKeyValue)
        {
            await SecureStorage.SetAsync("apiKey", apiKeyValue);
        }
    }
}
