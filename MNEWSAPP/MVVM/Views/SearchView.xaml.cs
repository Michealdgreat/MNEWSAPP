using Microsoft.Maui;
using MNEWSAPP.MVVM.Models;
using MNEWSAPP.Service;

namespace MNEWSAPP.MVVM.Views;

public partial class SearchView : ContentPage
{
    private readonly GetNews _getNews;


    public SearchView(GetNews getNews)
    {
        InitializeComponent();
        this._getNews = getNews;
    }

    private async void SearchButtonClicked(object sender, EventArgs e)
    {
        List<ArticleModel>? articleModels = new();
        var keyword = SearchKeyword.Text;
        var news = await _getNews.GetNewsAsync(keyword);

        if (news != null)
        {
            foreach (var item in news)
            {
                articleModels.Add(item);
            }

        }

        var navigationParameters = new Dictionary<string, object>()
        {
           { "data", articleModels }
        };

        await Shell.Current.GoToAsync($"{nameof(CategoryView)}", navigationParameters);
    }

}