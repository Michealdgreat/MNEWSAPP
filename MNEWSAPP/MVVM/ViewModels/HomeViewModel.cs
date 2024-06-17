using System.Collections.ObjectModel;
using Microsoft.Extensions.Configuration;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using MNEWSAPP.MVVM.Models;
using System.Net.Http;

namespace MNEWSAPP.MVVM.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl = "https://newsapi.org/v2/top-headlines?sources=techcrunch&apiKey=";

    [ObservableProperty]
    private ObservableCollection<ArticleModel>? news;

    private readonly IConfiguration _config;

    public HomeViewModel(IConfiguration config)
    {
        _httpClient = new HttpClient();
        _config = config;
        News = new ObservableCollection<ArticleModel>();
    }

    public async Task GetNews()
    {
        string? apiKey = _config["apiKey"];
        if (string.IsNullOrEmpty(apiKey))
        {
            throw new InvalidOperationException("API key is not set in the configuration.");
        }

        string url = $"{_baseUrl}{apiKey}";
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "MN News/1.0");

        var response = await _httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<ResponseModel>(responseString);
            News?.Clear();
            if (data?.Articles != null)
            {
                foreach (var article in data.Articles)
                {
                    News?.Add(article);
                }
            }
        }
    }
}
