using Microsoft.Extensions.Configuration;
using MNEWSAPP.MVVM.Models;
using System.Text.Json;

namespace MNEWSAPP.Service
{
    public class GetNews
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://newsapi.org/v2/top-headlines?sources=techcrunch&apiKey=";
        private readonly IConfiguration _config;
        private readonly JsonSerializerOptions _serializerOptions;




        public GetNews(IConfiguration config)
        {
            _httpClient = new HttpClient();
            _config = config;
            _serializerOptions = new JsonSerializerOptions { WriteIndented = true };
        }




        public async Task<List<ArticleModel>?> GetNewsAsync()
        {
            string apiKey = _config["apiKey"];
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("API key is not set in the configuration.");
            }

            string url = $"{_baseUrl}{apiKey}";
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "MN News/1.0");
            HttpRequestMessage request = new(HttpMethod.Get, url);
            request.Headers.Add("Authorization", "Bearer " + apiKey);

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            await using var responseStream = await response.Content.ReadAsStreamAsync();
            var articles = await JsonSerializer.DeserializeAsync<List<ArticleModel>>(responseStream, _serializerOptions);

            return articles;
        }
    }
}
