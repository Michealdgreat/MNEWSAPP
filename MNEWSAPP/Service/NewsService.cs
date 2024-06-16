using MNEWSAPP.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MNEWSAPP.Service
{
    public class NewsService
    {
        private readonly string _requestUrl;
        private readonly JsonSerializerOptions _options = new()
        {
            WriteIndented = true
        };

        public NewsService(string requestUrl)
        {
            _requestUrl = requestUrl;
        }

        public async Task<List<ArticleModel>?> GetNewsAsync(string apiKey)
        {
            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("User-Agent", "MN News/1.0");
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiKey);

            HttpResponseMessage response;

            try
            {
                response = await httpClient.GetAsync(_requestUrl);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Error fetching data from the News API", e);
            }

            var apiResponse = await response.Content.ReadAsStreamAsync();
            var newsApiResponse = await JsonSerializer.DeserializeAsync<ApiResponseModel>(apiResponse, _options);

            if (newsApiResponse?.Articles == null)
            {
                throw new Exception("Failed to fetch data from News API.");
            }

            // Return only articles with a featured image
            var articlesWithImgUrl = newsApiResponse.Articles
                .Where(article => !string.IsNullOrEmpty(article.UrlToImage))
                .ToList();

            return articlesWithImgUrl;
        }
    }
}
