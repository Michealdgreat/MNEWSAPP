using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using MNEWSAPP.MVVM.Models;
using System.Threading.Tasks;
using MNEWSAPP.Service;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MNEWSAPP.MVVM.ViewModels
{
    public partial class HomeViewModel : ObservableObject, INotifyPropertyChanged
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://newsapi.org/v2/top-headlines?country=us&apiKey=";

        [ObservableProperty]
        private ObservableCollection<ArticleModel>? news;

        public HomeViewModel()
        {
            _httpClient = new HttpClient();
            News = new ObservableCollection<ArticleModel>();
            News.CollectionChanged += (s, e) => OnPropertyChanged(nameof(TopThreeNews));
            News.CollectionChanged += (s, e) => OnPropertyChanged(nameof(SelectFive));
        }

        public async Task GetNews()
        {
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


                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    News?.Clear();
                    if (filteredData?.Articles != null)
                    {
                        foreach (var article in filteredData.Articles.Take(12))
                        {


                            article.UrlToImage = await ImageCache.GetImageFromCacheAsync(article.UrlToImage);
                            News?.Add(article);

                        }
                    }
                });
            }
        }

        public ObservableCollection<ArticleModel> TopThreeNews
        {
            get
            {
                return new ObservableCollection<ArticleModel>(News?.Take(1) ?? Enumerable.Empty<ArticleModel>());
            }
        }

        public ObservableCollection<ArticleModel> SelectFive
        {
            get
            {
                return new ObservableCollection<ArticleModel>(News?.Skip(2).Take(10) ?? Enumerable.Empty<ArticleModel>());
            }
            set
            {

                SelectFive = value;
                OnPropertyChanged();

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private async Task SetApiKeyAsync(string apiKeyValue)
        {
            await SecureStorage.SetAsync("apiKey", apiKeyValue);
        }
    }
}
