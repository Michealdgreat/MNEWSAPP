using MNEWSAPP.MVVM.Models;
using MNEWSAPP.Service;

namespace MNEWSAPP.MVVM.Views;

public partial class ExploreView : ContentPage
{
    private readonly GetNews _getNews;

    public ExploreView(GetNews getNews)
	{
		InitializeComponent();
        _getNews = getNews;
    }

    private async Task CategoryTapped(object sender, TappedEventArgs e, string category)
    {
        List<ArticleModel> articleModel = new();

        var data = await _getNews.GetNewsAsync(category);
        if (data != null)
        {
            foreach (var item in data)
            {
                articleModel.Add(item);
            }
        }

        var navigationParameters = new Dictionary<string, object>
            {
                { "data", articleModel }
            };

        await Shell.Current.GoToAsync("CategoryView", navigationParameters);
    }

    private async void MIcrosoftTapped(object sender, TappedEventArgs e) => await CategoryTapped(sender, e, "Microsoft");
    private async void AppleTapped(object sender, TappedEventArgs e) => await CategoryTapped(sender, e, "Apple");
    private async void MetaTapped(object sender, TappedEventArgs e) => await CategoryTapped(sender, e, "Meta");
    private async void AITapped(object sender, TappedEventArgs e) => await CategoryTapped(sender, e, "AI");
    private async void VideosTapped(object sender, TappedEventArgs e) => await CategoryTapped(sender, e, "Videos");
    private async void ENtertainmentTapped(object sender, TappedEventArgs e) => await CategoryTapped(sender, e, "Entertainment");
    private async void TiktokTapped(object sender, TappedEventArgs e) => await CategoryTapped(sender, e, "Tiktok");
    private async void SPortTapped(object sender, TappedEventArgs e) => await CategoryTapped(sender, e, "Sport");
    private async void MondayTapped(object sender, TappedEventArgs e) => await CategoryTapped(sender, e, "Monday");
    private async void BusinessTapped(object sender, TappedEventArgs e) => await CategoryTapped(sender, e, "Business");
}