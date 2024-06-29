using CommunityToolkit.Mvvm.ComponentModel;
using MNEWSAPP.MVVM.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace MNEWSAPP.Service
{
    public partial class GetNews : ObservableObject
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://newsapi.org/v2/everything?q=";

        public GetNews()
        {
            _httpClient = new HttpClient();
        }

        public async Task<ObservableCollection<ArticleModel>?> GetNewsAsync(string keyword)
        {
            var apiKey = await SecureStorage.GetAsync("apiKey");


            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("API key is not set in the configuration.");
            }

            string url = $"{_baseUrl}{keyword}&apiKey={apiKey}";
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("MN News/1.0");

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
                        foreach (var article in filteredData.Articles.Take(6))
                        {
                            article.UrlToImage = await ImageCache.GetImageFromCacheAsync(article.UrlToImage);
                            articles.Add(article);
                        }
                    }
                    return articles;
                });

                return new ObservableCollection<ArticleModel>(processedArticles);
            }

            return new ObservableCollection<ArticleModel>();
        }

        private async Task SetApiKeyAsync(string apiKeyValue)
        {
            await SecureStorage.SetAsync("apiKey", apiKeyValue);
        }
    }
}
