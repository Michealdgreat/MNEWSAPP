using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using MNEWSAPP.MVVM.Models;
using System.Threading.Tasks;
using MNEWSAPP.Service;
using System.Linq;
using System;

namespace MNEWSAPP.MVVM.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://newsapi.org/v2/top-headlines?country=us&apiKey=";

        [ObservableProperty]
        private ObservableCollection<ArticleModel> news;

        [ObservableProperty]
        private ObservableCollection<ArticleModel> topThreeNews;

        [ObservableProperty]
        private ObservableCollection<ArticleModel> selectFive;

        [ObservableProperty]
        private bool isDataLoading;

        [ObservableProperty]
        private bool isDataLoaded;

        public HomeViewModel()
        {
            _httpClient = new HttpClient();
            News = new ObservableCollection<ArticleModel>();
            TopThreeNews = new ObservableCollection<ArticleModel>();
            SelectFive = new ObservableCollection<ArticleModel>();
        }

        public async Task GetNews()
        {
            IsDataLoading = true;
            IsDataLoaded = false;

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

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    News.Clear();
                    foreach (var article in processedArticles)
                    {
                        News.Add(article);
                    }

                    UpdateTopThreeAndSelectFive();

                    IsDataLoading = false;
                    IsDataLoaded = true;
                });
            }
        }

        private void UpdateTopThreeAndSelectFive()
        {
            TopThreeNews.Clear();
            SelectFive.Clear();

            foreach (var article in News.Take(1))
            {
                TopThreeNews.Add(article);
            }

            foreach (var article in News.Skip(1).Take(5))
            {
                SelectFive.Add(article);
            }
        }

        private async Task SetApiKeyAsync(string apiKeyValue)
        {
            await SecureStorage.SetAsync("apiKey", apiKeyValue);
        }

        public string SaveState()
        {
            var state = new
            {
                News,
                TopThreeNews,
                SelectFive,
                IsDataLoaded
            };
            return JsonConvert.SerializeObject(state);
        }

        public void RestoreState(string stateJson)
        {
            var state = JsonConvert.DeserializeObject<dynamic>(stateJson);
            if (state != null)
            {
                News = JsonConvert.DeserializeObject<ObservableCollection<ArticleModel>>(Convert.ToString(state.News));
                TopThreeNews = JsonConvert.DeserializeObject<ObservableCollection<ArticleModel>>(Convert.ToString(state.TopThreeNews));
                SelectFive = JsonConvert.DeserializeObject<ObservableCollection<ArticleModel>>(Convert.ToString(state.SelectFive));
                IsDataLoaded = state.IsDataLoaded;
            }
        }
    }
}
