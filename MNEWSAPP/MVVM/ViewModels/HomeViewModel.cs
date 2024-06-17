using System.Collections.ObjectModel;
using Microsoft.Extensions.Configuration;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MNEWSAPP.MVVM.Models;
using MNEWSAPP.Service;
using System.Windows.Input;
using Newtonsoft.Json;
using System.Net.Http;

namespace MNEWSAPP.MVVM.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl = "https://newsapi.org/v2/top-headlines?sources=techcrunch&apiKey=";
    public List<ArticleModel>? News { get; private set; }

    private readonly IConfiguration _config;

    public HomeViewModel(IConfiguration config)
    {
        _httpClient = new HttpClient();
        _config = config;
    }

    public ICommand GetAllUsersCommand => new Command(async () =>
    {
        string? apiKey = _config["apiKey"];
        if (string.IsNullOrEmpty(apiKey))
        {
            throw new InvalidOperationException("API key is not set in the configuration.");
        }

        string url = $"{_baseUrl}{apiKey}";
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "MN News/1.0");
        HttpRequestMessage request = new(HttpMethod.Get, url);
        request.Headers.Add("Authorization", "Bearer " + apiKey);

        var response = await _httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<ResponseModel>(responseString);
            News = data.Articles;
        }
    });
}
