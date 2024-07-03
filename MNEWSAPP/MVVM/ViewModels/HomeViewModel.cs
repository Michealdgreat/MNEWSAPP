using System.Net.Http;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using MNEWSAPP.MVVM.Models;
using MNEWSAPP.Service;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MNEWSAPP.MVVM.Views;

namespace MNEWSAPP.MVVM.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://newsapi.org/v2/top-headlines?country=us&apiKey=";
        private readonly GetNews _getNewsService;

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

        [ObservableProperty]
        private string? errorMessage;

        public HomeViewModel(GetNews getNewsService)
        {
            _httpClient = new HttpClient();
            _getNewsService = getNewsService;
            News = new ObservableCollection<ArticleModel>();
            TopThreeNews = new ObservableCollection<ArticleModel>();
            SelectFive = new ObservableCollection<ArticleModel>();
        }


        public async Task GetNews()
        {
            if (IsDataLoaded) return;

            IsDataLoading = true;
            IsDataLoaded = false;
            ErrorMessage = string.Empty;

            var apiKey = await SecureStorage.GetAsync("apiKey");
            if (string.IsNullOrEmpty(apiKey))
            {
                await Shell.Current.GoToAsync($"{nameof(SetApiKeyView)}");
            }

            //if (string.IsNullOrEmpty(apiKey))
            //{
            //    ErrorMessage = "API key is not set in the configuration.";
            //    IsDataLoading = false;
            //    return;
            //}

            string url = $"{_baseUrl}{apiKey}";
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
                else
                {
                    var message = response.ReasonPhrase;

                    var navigationParameters = new Dictionary<string, object>
                {
                    {"errorMessage", message }
                };

                    await Shell.Current.GoToAsync($"{nameof(ConnectionErrorView)}", navigationParameters);
                }
            }
            catch (HttpRequestException ex)
            {
                var message = ex.Message;

                var navigationParameters = new Dictionary<string, object>
                {
                    {"errorMessage", message }
                };

                await Shell.Current.GoToAsync($"{nameof(ConnectionErrorView)}", navigationParameters);
            }
            catch (Exception ex)
            {
                var message = ex.Message;

                var navigationParameters = new Dictionary<string, object>
                {
                    {"errorMessage", message }
                };

                await Shell.Current.GoToAsync($"{nameof(ConnectionErrorView)}", navigationParameters);
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
    }
}
